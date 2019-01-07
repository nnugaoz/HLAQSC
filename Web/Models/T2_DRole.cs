using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_DRole : AutoFiles.T2_DRole
    {
        #region 数据权限
        public int DRole_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_DRole "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T2_DRole.ID) i "
                        + ",T2_DRole.* "
                        + ",T1_DataDirc.DircTitle TypeStr "
                        + ",(case T2_DRole.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T2_DRole "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'DRoleType' and T2_DRole.Type = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int DRole_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        internal string DRole_GetTypeByTitle()
        {
            string sql = "";
            DataTable lDT = null;
            string lDRoleType = "";

            Select(ref sql, " AND T2_DRole.Title='" + Title + "'");

            DataTool.Get_DataTable_From_DataSet_2(sql, ref lDT);
            if (lDT != null && lDT.Rows.Count > 0)
            {
                lDRoleType = lDT.Rows[0]["Type"].ToString();
            }
            return lDRoleType;
        }

        public bool DRole_UpdateOne()
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
                sql += " select @ID = 'DR' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_DRole), 10) ";
                sql += " insert into T2_DRole(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T2_DRole "
                + " set "
                    + " Title = '" + Title + "' "
                    + ",Type = '" + Type + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool DRole_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 数据权限

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string TypeStr { get; set; }
    }
}