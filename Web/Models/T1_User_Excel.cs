using MyTool.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web.MyLib;

namespace Web.Models
{
    public class T1_User_Excel : AutoFiles.T1_User_Excel
    {
        public PageList pageList = new PageList();

        #region 用户列表
        public int GeUserList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T1_User_Excel "
                + " where 1=1 "
                    + " and Name like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T_User.ID "
                        + ",T_User.Name "
                        + ",T_User.LoginName "
                        + ",T_User.OrgCode "
                        + ",T_User.PRoleID "
                        + ",T_User.RRoleCode "
                        + ",T_User.DRoleType "
                        + ",T_User.JobCode "
                        + ",T_User.UserKey "
                        + ",T_Dict_Job.DircTitle JobName "
                        + ",T_Org.Title OrgName "
                        + ",T_Dict_DRole.DircTitle DRoleName "
                        + ",T_RRole.Title RRoleName "
                        + ",(case T_User.Del when '0' then '' else '无效' end) Status "
                    + " from T1_User_Excel T_User "
                    + " LEFT JOIN T1_DataDirc T_Dict_Job ON (T_User.JobCode=T_Dict_Job.DircKey AND T_Dict_Job.Type='JobType') "
                    + " LEFT JOIN T1_DataDirc T_Dict_DRole ON (T_User.DRoleType=T_Dict_DRole.DircKey AND T_Dict_DRole.Type='DRoleType') "
                    + " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) "
                    + " LEFT JOIN T2_RRole T_RRole ON (T_User.RRoleCode=T_RRole.Code ) "
                    + " where 1=1 "
                        + " and T_User.Name like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion

        #region 新增用户
        public bool UpdateOne()
        {
            string sql = "";
            bool is_add = false;

            if (String.IsNullOrEmpty(ID))
            {
                // ID为空，新增
                is_add = true;
            }

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                sql += " select @ID = 'URE' + dbo.FP_Tool_IDAddOne((select max(ID) from T1_User_Excel), 10) ";
                sql += " insert into T1_User_Excel(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += " update T1_User_Excel ";
            sql += " set ";
            sql += " Name = '" + Name + "' ";
            sql += ", LoginName = '" + LoginName + "' ";

            if (JobCode != "")
            {
                sql += ", JobCode = '" + JobCode + "' ";
            }
            else
            {
                sql += ", JobCode = NULL";
            }

            if (OrgCode != "")
            {
                sql += ", OrgCode='" + OrgCode + "'";
            }
            else
            {
                sql += ", OrgCode=NULL";
            }

            if (PRoleID != "")
            {
                sql += ", PRoleID='" + PRoleID + "'";
            }
            else
            {
                sql += ", PRoleID=NULL";
            }

            if (DRoleType != "")
            {
                sql += ", DRoleType='" + DRoleType + "'";
            }
            else
            {
                sql += ", DRoleType=NULL";
            }

            if (RRoleCode != "")
            {
                sql += ", RRoleCode='" + RRoleCode + "'";
            }
            else
            {
                sql += ", RRoleCode=NULL";
            }
            if (UserKey != "")
            {
                sql += ", UserKey='" + UserKey + "'";
            }
            else
            {
                sql += ", UserKey=NULL";
            }
            if (is_add)
            {
                sql += ", Del = 0 ";
            }

            sql += " where ID = @ID ";

            return DataTool.Update(sql);
        }
        #endregion

        #region 根据用户ID，返回用户信息
        public int User_GetOne(ref DataTable dt)
        {
            string lSql = "";
            lSql = "";
            lSql += " select ";
            lSql += " T_User.ID ";
            lSql += ",T_User.Name ";
            lSql += ",T_User.LoginName ";
            lSql += ",T_User.Password ";
            lSql += ",T_User.OrgCode ";
            lSql += ",T_User.PRoleID ";
            lSql += ",T_User.RRoleCode ";
            lSql += ",T_User.DRoleType ";
            lSql += ",T_User.JobCode ";
            lSql += ",T_User.UserKey ";
            lSql += ",T_User.Del ";
            lSql += ",T_Org.Title OrgName ";
            lSql += ",T_RRole.Title RRoleName ";
            lSql += " FROM T1_User_Excel T_User";
            lSql += " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) ";
            lSql += " LEFT JOIN T2_RRole T_RRole ON (T_User.RRoleCode=T_RRole.Code ) ";
            lSql += " WHERE T_User.ID='" + ID + "'";
            return DataTool.Get_DataTable_From_DataSet_2(lSql, ref dt);
        }
        #endregion

        #region 获取重复的【登录名】
        public int User_GetDupLoginName(ref DataTable dt)
        {
            string lSQL = "";
            lSQL = "SELECT ";
            lSQL += " LoginName";
            lSQL += " FROM ";
            lSQL += " T1_User_Excel";
            lSQL += " GROUP BY LoginName";
            lSQL += " HAVING COUNT(LoginName)>1";
            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref dt);
        }
        #endregion

        #region 获取重复的【用户唯一标识】
        internal int User_GetDupUserKey(ref DataTable dt)
        {
            string lSQL = "";
            lSQL = "SELECT ";
            lSQL += " UserKey";
            lSQL += " FROM ";
            lSQL += " T1_User_Excel";
            lSQL += " GROUP BY UserKey";
            lSQL += " HAVING COUNT(UserKey)>1";
            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref dt);
        }

        #endregion

        #region 检查登录名是否可用
        public Boolean CheckLoginName()
        {
            Boolean lRet = false;
            String lSQL = "";
            DataTable lDT = null;

            lSQL = "SELECT 1";
            lSQL += " FROM T1_User_Excel";
            lSQL += "  WHERE ID <> '" + ID + "' ";
            lSQL += " AND LoginName='" + LoginName + "'";

            if (DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT) == (int)MyTool.MyEnum.MyEnum.Enum_Ret.NoData)
            {
                lRet = true;
            }

            return lRet;
        }
        #endregion

        #region 检查员工唯一标识

        public Boolean CheckUserKey()
        {
            Boolean lRet = false;
            String lSQL = "";
            DataTable lDT = null;

            lSQL = "SELECT 1";
            lSQL += " FROM T1_User_Excel";
            lSQL += " WHERE ID <> '" + ID + "' ";
            lSQL += " AND UserKey='" + UserKey + "'";

            if (DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT) == (int)MyTool.MyEnum.MyEnum.Enum_Ret.NoData)
            {
                lRet = true;
            }

            return lRet;
        }
        #endregion

        #region 将T1_User_Excel表中的数据更新到T1_User
        public bool UserDataImport()
        {
            bool lRet = false;
            String lSQL = "";
            DataTable lDTExcel = null;
            ArrayList lSQLList = new ArrayList();
            T1_User lUser = new T1_User();

            lSQL += "SELECT";
            lSQL += " Name";
            lSQL += ",LoginName";
            lSQL += ",OrgCode";
            lSQL += ",PRoleID";
            lSQL += ",RRoleCode";
            lSQL += ",DRoleType";
            lSQL += ",JobCode";
            lSQL += ",UserKey";
            lSQL += " FROM T1_User_Excel";

            if (DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDTExcel) == 1)
            {
                for (int i = 0; i < lDTExcel.Rows.Count; i++)
                {
                    lUser.ID = "";
                    lUser.LoginName = lDTExcel.Rows[i]["LoginName"].ToString();
                    if (!lUser.CheckLoginName())
                    {

                    }
                    else
                    {
                        lSQL = "";
                        lSQL += "INSERT INTO T1_User(";
                        lSQL += "ID";
                        lSQL += ", Name";
                        lSQL += ", LoginName";
                        lSQL += ", OrgCode";
                        lSQL += ", PRoleID";
                        lSQL += ", DRoleType";
                        lSQL += ", RRoleCode";
                        lSQL += ", JobCode";
                        lSQL += ", UserKey";
                        lSQL += ", Password";
                        lSQL += ", Del";
                        lSQL += ")VALUES(";
                        lSQL += "'UR' + dbo.FP_Tool_IDAddOne((select max(ID) from T1_User), 10)";
                        lSQL += ",'" + lDTExcel.Rows[i]["Name"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["LoginName"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["OrgCode"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["PRoleID"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["DRoleType"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["RRoleCode"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["JobCode"].ToString() + "'";
                        lSQL += ",'" + lDTExcel.Rows[i]["UserKey"].ToString() + "'";
                        lSQL += ",'" + MD5.Encode("123456") + "'";
                        lSQL += ",0";
                        lSQL += ")";
                        lSQLList.Add(lSQL);
                    }
                }
                lSQL = "DELETE FROM T1_User_Excel";
                lSQLList.Add(lSQL);
                lRet = SqlOption.ExecuteSqlTran(lSQLList);
            }
            return lRet;
        }
        #endregion

        #region 获取导入Insert语句
        internal string GetInsertSQL()
        {
            String lSQL = "";
            lSQL += "INSERT INTO T1_User_Excel(";
            lSQL += "ID";
            lSQL += ", Name";
            lSQL += ", LoginName";
            lSQL += ", OrgCode";
            lSQL += ", PRoleID";
            lSQL += ", DRoleType";
            lSQL += ", RRoleCode";
            lSQL += ", JobCode";
            lSQL += ", UserKey";
            lSQL += ")VALUES(";
            lSQL += "'URE' + dbo.FP_Tool_IDAddOne((select max(ID) from T1_User_Excel), 10)";
            lSQL += ",'" + Name + "'";
            lSQL += ",'" + LoginName + "'";
            lSQL += ",'" + OrgCode + "'";
            lSQL += ",'" + PRoleID + "'";
            lSQL += ",'" + DRoleType + "'";
            lSQL += ",'" + RRoleCode + "'";
            lSQL += ",'" + JobCode + "'";
            lSQL += ",'" + UserKey + "'";
            lSQL += ")";
            return lSQL;
        }
        #endregion
    }
}