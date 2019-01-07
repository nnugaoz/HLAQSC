using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_PRole : AutoFiles.T2_PRole
    {
        #region 用户权限
        public int PRole_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_PRole "
                + " where 1=1 "
                    + " and ('" + pageList.Para1 + "' = '' or Title like '%" + pageList.Para1 + "%') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T2_PRole.* "
                        + ",(case T2_PRole.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T2_PRole "
                    + " where 1=1 "
                        + " and ('" + pageList.Para1 + "' = '' or T2_PRole.Title like '%" + pageList.Para1 + "%') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int PRole_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        internal string PRole_GetIDByTitle()
        {
            string sql = "";
            DataTable lDT = null;
            string lPRoleID = "";

            Select(ref sql, " AND T2_PRole.Title='" + Title + "'");

            DataTool.Get_DataTable_From_DataSet_2(sql, ref lDT);
            if (lDT != null && lDT.Rows.Count > 0)
            {
                lPRoleID = lDT.Rows[0]["ID"].ToString();
            }
            return lPRoleID;
        }

        public bool PRole_UpdateOne()
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
                sql += " select @ID = 'PR' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_PRole), 10) ";
                sql += " insert into T2_PRole(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T2_PRole "
                + " set "
                    + " Title = '" + Title + "' "
                    + ",Remark = '" + Remark + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID "

                + " delete T2_PRole_Detail where PRoleID = @ID ";

            string[] list = RoleDetail.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                sql += " insert into T2_PRole_Detail(PRoleID, PageCode) select @ID, '" + list[i] + "' ";
            }

            return DataTool.Update(sql);
        }

        public bool PRole_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 用户权限

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string RoleDetail { get; set; }
    }
}