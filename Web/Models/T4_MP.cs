using MyTool.DB;
using System.Data;

namespace Web.Models
{
    public class T4_MP : AutoFiles.T4_MP
    {
        #region 接口
        public int SCJBatch_GetMonthInfo_ZD(ref DataTable dt, string LoginName, string ZDCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_MonthInfo_ZD_ByLoginName('" + LoginName + "', '" + ZDCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetMonthInfo_CC(ref DataTable dt, string LoginName, string ZDCode, string CCCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_MonthInfo_CC_ByLoginName('" + LoginName + "', '" + ZDCode + "', '" + CCCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetMonthInfo_ZYM(ref DataTable dt, string LoginName, string ZDCode, string CCCode, string ZYMCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_MonthInfo_ZYM_ByLoginName('" + LoginName + "', '" + ZDCode + "', '" + CCCode + "', '" + ZYMCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetInfo_ZD(ref DataTable dt, string LoginName, string ZDCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_Info_ZD_ByLoginName('" + LoginName + "', '" + ZDCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetInfo_CC(ref DataTable dt, string LoginName, string ZDCode, string CCCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_Info_CC_ByLoginName('" + LoginName + "', '" + ZDCode + "', '" + CCCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetInfo_ZYM(ref DataTable dt, string LoginName, string ZDCode, string CCCode, string ZYMCode)
        {
            string sql = ""
                + " select * "
                + " from dbo.FT_SCJ_Info_ZYM_ByLoginName('" + LoginName + "', '" + ZDCode + "', '" + CCCode + "', '" + ZYMCode + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 接口
    }
}