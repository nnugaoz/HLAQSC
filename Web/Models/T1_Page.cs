using MyTool.DB;
using System.Data;

namespace Web.Models
{
    public class T1_Page : AutoFiles.T1_Page
    {
        #region BaseController
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int BC_GetAll_Admin(ref DataTable dt)
        {
            string sql = "";
            string where = ""
                + " and Type = 1 "
                + " and Del = 0 ";
            Select(ref sql, where);
            sql += " order by OrderBy ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        /// <summary>
        /// 获取所有菜单
        /// 限制条件 菜单权限ID
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public int BC_GetAll_Limit(ref DataTable dt, string RoleID)
        {
            string sql = ""
                + " select T1_Page.* "
                + " from T2_PRole_Detail "
                    + " left join T1_Page on T2_PRole_Detail.PageCode = T1_Page.Code "
                + " where 1=1 "
                    + " and T2_PRole_Detail.PRoleID = '" + RoleID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion BaseController

        #region PRole
        public int PRole_GetAll_ZTree_Add(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by OrderBy) i "
                    + ",Code id "
                    + ",Title name "
                    + ",left(Code, len(Code) - 3) pId "
                    + ",'false' checked "
                + " from T1_Page "
                + " where 1=1 "
                    + " and Type = '1' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int PRole_GetAll_ZTree_Edit(ref DataTable dt, string RoleID)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by T1_Page.OrderBy) i "
                    + ",T1_Page.Code id "
                    + ",T1_Page.Title name "
                    + ",left(T1_Page.Code, len(T1_Page.Code) - 3) pId "
                    + ",(case when T2_PRole_Detail.PRoleID is null then 'false' else 'true' end) checked "
                + " from T1_Page "
                    + " left join T2_PRole_Detail on 1=1 "
                        + " and T2_PRole_Detail.PRoleID = '" + RoleID + "' "
                        + " and T1_Page.Code = T2_PRole_Detail.PageCode "
                + " where 1=1 "
                    + " and T1_Page.Type = '1' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion PRole
    }
}