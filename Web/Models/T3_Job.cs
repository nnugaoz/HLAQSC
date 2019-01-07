using MyTool.DB;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T3_Job : AutoFiles.T3_Job
    {
        #region 工种
        public int Job_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T3_Job "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",* "
                        + ",(case Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T3_Job "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Job_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        internal string Job_GetCodeByTitle()
        {
            string lSQL = "";
            DataTable lDT = null;
            string lJobCode = "";

            lSQL = "SELECT ";
            lSQL += " DircKey";
            lSQL += " FROM T1_DataDirc";
            lSQL += " WHERE Type='JobType'";
            lSQL += " AND DircTitle='" + Title + "'";

            DataTool.Get_DataTable_From_DataSet_2(lSQL, ref lDT);
            if (lDT != null && lDT.Rows.Count > 0)
            {
                lJobCode = lDT.Rows[0]["DircKey"].ToString();
            }
            return lJobCode;
        }
        public bool Job_UpdateOne()
        {
            string sql = "";
            bool is_add = true;

            sql += ""
                + " declare @Title varchar(100) "
                + " select @Title = DircTitle from T1_DataDirc where Type = 'JobType' and DircKey = '" + Code + "' "

                + " if((select count(1) from T3_Job where Code = '" + Code + "') = 0) "
                + " begin ";

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                sql += " select @ID = 'TJ' + dbo.FP_Tool_IDAddOne((select max(ID) from T3_Job), 10) ";
                sql += " insert into T3_Job(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T3_Job "
                + " set "
                    + " Code = '" + Code + "' "
                    + ",Title = @Title "
                    + ",Del = '" + Del + "' "
                + " where ID = @ID ";

            sql += " end ";

            return DataTool.Update(sql);
        }

        public bool Job_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 工种

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}