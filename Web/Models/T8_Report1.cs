using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T8_Report1 : AutoFiles.T8_Report1
    {
        public int R1_GetList(ref DataTable dt)
        {
            DateTime time = Convert.ToDateTime(pageList.Para1);
            string sql = ""
                + " select "
                    + " ROW_NUMBER() over (order by ID) i, 10 c "
                    + ",* "
                + " from dbo.FT_Report1('" + time.Year.ToString("0000") + "', '" + time.Month.ToString("00") + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int R1_Load(string date, ref DataTable dt)
        {
            DateTime time = Convert.ToDateTime(date);
            string sql = ""
                + " select * "
                + " from T8_Report1 "
                + " where 1=1 "
                    + " and Year = '" + time.Year.ToString("0000") + "' "
                    + " and Month = '" + time.Month.ToString("00") + "' "
                    + " and Day = '" + time.Day.ToString("00") + "' "
                    + " and Type2_Code in ('001004', '002004', '004003', '008004', '013004') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool R1_Update(string date, string vals)
        {
            DateTime time = Convert.ToDateTime(date);
            string[] list = vals.Split(',');
            string[] code = { "001004", "002004", "004003", "008004", "013004" };

            string sql = "";

            for (int i = 0; i < list.Length; i++)
            {
                sql += ""
                    + " update T8_Report1 "
                    + " set Val = " + list[i] + " "
                    + " where 1=1 "
                        + " and Year = '" + time.Year.ToString("0000") + "' "
                        + " and Month = '" + time.Month.ToString("00") + "' "
                        + " and Day = '" + time.Day.ToString("00") + "' "
                        + " and Type2_Code = '" + code[i] + "' "
                        
                    + " exec SP_Report1 '" + time.Year.ToString("0000") + "', '" + time.Month.ToString("00") + "', '" + time.Day.ToString("00") + "' ";
            }

            return DataTool.Update(sql);
        }

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}