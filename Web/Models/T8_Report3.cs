using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T8_Report3 : AutoFiles.T8_Report3
    {
        public int R3_GetList(ref DataTable dt)
        {
            DateTime time = Convert.ToDateTime(pageList.Para1);
            string sql = ""
                + " select "
                    + " ROW_NUMBER() over (order by T8_Report3.ID) i, 10 c "
                    + ",T8_Report3.* "
                + " from T8_Report3 "
                + " where 1=1 "
                    + " and Year = '" + time.Year.ToString("0000") + "' "
                    + " and Month = '" + time.Month.ToString("00") + "' "
                    + " and Day = '" + time.Day.ToString("00") + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}