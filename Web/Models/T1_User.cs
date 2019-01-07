using MyTool.DB;
using MyTool.Model;
using System;
using System.Data;
using Web.AutoFiles;
using Web.MyLib;

namespace Web.Models
{
    public class T1_User : AutoFiles.T1_User
    {
        /// <summary>
        /// 分页相关
        /// </summary>
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
                + " from T1_User "
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
                        + ",T_PRole.Title PRoleName "
                        + ",T_User.Del Del "
                        + ",(case T_User.Del when '0' then '已启用' else '已禁用' end) Status "
                    + " from T1_User T_User "
                    + " LEFT JOIN T1_DataDirc T_Dict_Job ON (T_User.JobCode=T_Dict_Job.DircKey AND T_Dict_Job.Type='JobType') "
                    + " LEFT JOIN T1_DataDirc T_Dict_DRole ON (T_User.DRoleType=T_Dict_DRole.DircKey AND T_Dict_DRole.Type='DRoleType') "
                    + " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) "
                    + " LEFT JOIN T2_RRole T_RRole ON (T_User.RRoleCode=T_RRole.Code ) "
                    + " LEFT JOIN T2_PRole T_PRole ON (T_User.PRoleID=T_PRole.ID ) "
                    + " where 1=1 "
                        + " and T_User.Name like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion

        #region 检查登录名是否可用
        public Boolean CheckLoginName()
        {
            Boolean lRet = false;
            String lSQL = "";
            DataTable lDT = null;

            lSQL = "SELECT 1";
            lSQL += " FROM T1_User";
            lSQL += " WHERE LoginName='" + LoginName + "'";
            lSQL += " AND ID <> '" + ID + "'";

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
            lSQL += " FROM T1_User";
            lSQL += " WHERE UserKey='" + UserKey + "'";
            lSQL += " AND ID <> '" + ID + "'";

            if (DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT) == (int)MyTool.MyEnum.MyEnum.Enum_Ret.NoData)
            {
                lRet = true;
            }

            return lRet;
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
                sql += " select @ID = 'UR' + dbo.FP_Tool_IDAddOne((select max(ID) from T1_User), 10) ";
                sql += " insert into T1_User(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += " update T1_User ";
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
                sql += ", Password = '" + MD5.Encode("123456") + "' ";
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
            lSql += " FROM T1_User T_User";
            lSql += " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) ";
            lSql += " LEFT JOIN T2_RRole T_RRole ON (T_User.RRoleCode=T_RRole.Code ) ";
            lSql += " WHERE T_User.ID='" + ID + "'";
            return DataTool.Get_DataTable_From_DataSet_2(lSql, ref dt);
        }
        #endregion

        #region 获取所有用户信息
        public int User_GetUsers(ref DataTable dt)
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
            lSql += " FROM T1_User T_User";
            lSql += " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) ";
            lSql += " LEFT JOIN T2_RRole T_RRole ON (T_User.RRoleCode=T_RRole.Code ) ";
            return DataTool.Get_DataTable_From_DataSet_2(lSql, ref dt);
        }
        #endregion

        #region 获取所有用户信息
        public int User_ExportUsers(ref DataTable dt)
        {
            string lSql = "";
            lSql = "";
            lSql += " select ";
            lSql += " T_User.Name ";
            lSql += ",T_User.LoginName ";
            lSql += ",T_Org.Title OrgName ";
            lSql += ",T_Dict_Job.DircTitle JobName ";
            lSql += ",T_User.UserKey ";
            lSql += " FROM T1_User T_User";
            lSql += " LEFT JOIN T2_Org T_Org ON (T_User.OrgCode=T_Org.Code ) ";
            lSql += " LEFT JOIN T1_DataDirc T_Dict_Job ON (T_User.JobCode=T_Dict_Job.DircKey AND T_Dict_Job.Type='JobType') ";
            return DataTool.Get_DataTable_From_DataSet_2(lSql, ref dt);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// 获取一条的数据
        /// 限制条件 登录名 密码
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Login_GetOne_Limit(ref DataTable dt)
        {
            string sql = ""
                + " select T1_User.ID "
                + " from T1_User "
                + " where 1=1 "
                    + " and Del = 0 "
                    + " and LoginName = '" + LoginName + "' "
                    + " and Password = '" + Password_MD5 + "' "

                + " union all "

                + " select T1_User_Admin.ID "
                + " from T1_User_Admin "
                + " where 1=1 "
                    + " and LoginName = '" + LoginName + "' "
                    + " and Password = '" + Password_MD5 + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        public string Password_MD5 { get; set; }
        #endregion 登录

        #region BaseController
        /// <summary>
        /// BaseController
        /// 获取一条的数据
        /// 限制条件 ID
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int BC_GetOne_Limit(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " ID, Name, OrgCode, PRoleID, RRoleCode, DRoleType, JobCode "
                + " from T1_User "
                + " where 1=1 "
                    + " and ID = '" + ID + "' "

                + " union all "

                + " select "
                    + " ID, Name, '' OrgCode, '' PRoleID, '' RRoleCode, '' DRoleType, '' JobCode "
                + " from T1_User_Admin "
                + " where 1=1 "
                    + " and ID = '" + ID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion BaseController

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// 限制条件 ID
        /// </summary>
        /// <returns></returns>
        public bool EditPassword_UpdateOne()
        {
            string sql = ""
                + " if('" + ID + "' = '" + MyPara.AdminID + "') "
                + " begin "
                    + " update T1_User_Admin "
                    + " set "
                        + " Password = '" + Password_MD5 + "' "
                    + " where 1=1 "
                        + " and ID = '" + ID + "' "
                + " end "
                + " else "
                + " begin "
                    + " update T1_User "
                    + " set "
                        + " Password = '" + Password_MD5 + "' "
                    + " where 1=1 "
                        + " and ID = '" + ID + "' "
                + " end ";

            return DataTool.Update(sql);
        }

        public bool ResetPassword_UpdateOne()
        {
            string sql = ""
                + " update T1_User "
                + " set "
                    + " Password = '" + Password_MD5 + "' "
                + " where 1=1 "
                    + " and ID = '" + ID + "' ";

            return DataTool.Update(sql);
        }
        #endregion 修改密码

        #region 审核权限 绑定人员
        public int RRole_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T1_User "
                + " where 1=1 "
                    + " and T1_User.Del = '0' "
                    + " and ('" + pageList.Para1 + "' = '' or T1_User.OrgCode = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T1_User.Name like '%" + pageList.Para2 + "%') "
                    + " and ('" + pageList.Para3 + "' = '' or T1_User.JobCode = '" + pageList.Para3 + "') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select T1_User.ID)) i "
                        + ",T1_User.* "
                        + ",isnull(T2_Org.Title, '') OrgName "
                        + ",isnull(T1_DataDirc.DircTitle, '') JobName "
                        + ",(case when T2_RRole_User.RRoleID is null then '0' else '1' end) BindStatus "
                    + " from T1_User "
                        + " left join T2_Org on T1_User.OrgCode = T2_Org.Code "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'JobType' and T1_User.JobCode = T1_DataDirc.DircKey "
                        + " left join T2_RRole on T2_RRole.Code = '" + pageList.Para9 + "' "
                        + " left join T2_RRole_User on T2_RRole_User.RRoleID = T2_RRole.ID and T1_User.ID = T2_RRole_User.UserID "
                    + " where 1=1 "
                        + " and T1_User.Del = '0' "
                        + " and ('" + pageList.Para1 + "' = '' or T1_User.OrgCode = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T1_User.Name like '%" + pageList.Para2 + "%') "
                        + " and ('" + pageList.Para3 + "' = '' or T1_User.JobCode = '" + pageList.Para3 + "') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 审核权限 绑定人员

        #region 井下收据管理
        /// <summary>
        /// C11_MineDataEntryController
        /// 获取一条的数据
        /// 限制条件 ID
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int MDE_GetOne(ref DataTable dt)
        {
            string sql = ""
                + " declare @JobCode varchar(100) "
                + " declare @JobName varchar(100) "

                + " select "
                    + " @JobCode = T1_User.JobCode "
                    + ",@JobName = T3_Job.Title "
                + " from T1_User "
                    + " left join T3_Job on T1_User.JobCode = T3_Job.Code "
                + " where 1=1 "
                    + " and T1_User.ID = '" + ID + "' "

                + " select isnull(@JobCode, '') JobCode, isnull(@JobName, '') JobName ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 井下收据管理

        #region 手持机登录
        public int SCJLogin_GetOne_Limit(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T1_User.ID "
                    + ",T1_User.Name "
                    + ",T1_User.JobCode "
                + " from T1_User "
                + " where 1=1 "
                    + " and Del = '0' "
                    + " and LoginName = '" + LoginName + "' "
                    + " and Password = '" + Password_MD5 + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 手持机登录

        #region 切换用户可用状态
        internal bool ToggleStatus()
        {
            bool lRet = false;
            string lSQL = "";
            lSQL = "UPDATE T1_User SET Del=CASE DEL WHEN 'true' THEN 'false' WHEN 'false' THEN 'true' END WHERE ID='" + ID + "'";
            lRet = DataTool.Update(lSQL);

            return lRet;
        }
        #endregion

    }
}