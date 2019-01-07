using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web.MyLib;

namespace Web.Models
{
    public class T6_Plan : AutoFiles.T6_Plan
    {
        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public int GetPlanList(ref DataTable dt)
        {
            string sql = ""
                            + " declare @bi int "
                            + " declare @ei int "
                            + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                            + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                            + " declare @count int "
                            + " select @count = count(1) "
                            + " from T6_Plan "
                            + " where 1=1 "
                                + " and YM like '%" + pageList.Para1 + "%' "

                            + " select @count c, * "
                            + " from ( "
                                + " select "
                                    + " ROW_NUMBER() over (order by YM) i "
                                    + ",T_Plan.ID "
                                    + ",T_Plan.YM "
                                    + ",T_Plan.FileName "
                                    + ",T_Plan.FileNameS "
                                    + ",T_Plan.UploadTime "
                                + " from T6_Plan T_Plan "
                                + " where 1=1 "
                                    + " and T_Plan.YM like '%" + pageList.Para1 + "%' "
                            + " ) t "
                            + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);

        }        

        internal int CheckPlanExists()
        {
            DataTable lDT = null;
            String lSQL = "";
            lSQL += "SELECT 1";
            lSQL += " FROM T6_Plan";
            lSQL += " WHERE YM='" + YM + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT);
        }

        #region 获取老计划ID
        internal Boolean GetOldPlanID()
        {
            Boolean lRet = false;
            string lSQL = "";
            int lSQLRet = 0;
            DataTable lDT = null;

            lSQL = "SELECT ID";
            lSQL += " FROM T6_Plan";
            lSQL += " WHERE YM='" + YM + "'";

            lSQLRet = DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT);

            if (lSQLRet == (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes || lSQLRet == (int)MyTool.MyEnum.MyEnum.Enum_Ret.NoData)
            {
                lRet = true;
                if (lSQLRet == (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes)
                {
                    ID = lDT.Rows[0][0].ToString();
                }
            }

            return lRet;
        }
        #endregion

        #region 获取新计划ID
        internal bool GetNewPlanID()
        {
            Boolean lRet = false;
            string lSQL = "";
            int lSQLRet = 0;
            DataTable lDT = null;

            lSQL = "SELECT 'PL' + dbo.FP_Tool_IDAddOne((SELECT MAX(ID) FROM T6_Plan),10)";

            lSQLRet = DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT);

            if (lSQLRet == (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes)
            {
                lRet = true;
                ID = lDT.Rows[0][0].ToString();
            }

            return lRet;
        }
        #endregion

        #region 获取删除记录的SQL
        internal string GetDeleteSQL()
        {
            String lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Plan ";
            lSQL += " WHERE ID='" + ID + "'";

            return lSQL;
        }
        #endregion

        #region 获取插入记录SQL
        internal string GetInsertSQL()
        {
            String lSQL = "";

            lSQL = "INSERT INTO T6_Plan(";
            lSQL += "ID";
            lSQL += ", YM";
            lSQL += ", FileName";
            lSQL += ", FileNameS";
            lSQL += ", UploadTime";
            lSQL += ")VALUES(";
            lSQL += "'" + ID + "'";
            lSQL += ",'" + YM + "'";
            lSQL += ",'" + FileName + "'";
            lSQL += ",'" + FileNameS + "'";
            lSQL += ",'" + UploadTime + "'";
            lSQL += ")";

            return lSQL;
        }
        #endregion
    }
}