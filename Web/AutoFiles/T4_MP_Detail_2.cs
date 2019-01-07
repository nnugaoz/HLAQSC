using System;

namespace Web.AutoFiles
{
    public class T4_MP_Detail_2
    {
        public T4_MP_Detail_2()
        {
        }

		public string Month { get; set; }		public string PositionCode { get; set; }		public string ConfigCode { get; set; }		public string Val { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T4_MP_Detail_2.Month "				+ ",T4_MP_Detail_2.PositionCode "				+ ",T4_MP_Detail_2.ConfigCode "				+ ",T4_MP_Detail_2.Val "                + " from [HLAQSC].dbo.T4_MP_Detail_2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP_Detail_2.Month = '" + Month + "' ";					sql += " and T4_MP_Detail_2.PositionCode = '" + PositionCode + "' ";					sql += " and T4_MP_Detail_2.ConfigCode = '" + ConfigCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T4_MP_Detail_2( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(ConfigCode))			{				count++;				sql += (count > 1 ? "," : " ") + "ConfigCode ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "Val ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Month + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(ConfigCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ConfigCode + "' ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Val + "' ";			}
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(ref string sql, string where)
        {
            sql = ""
                + " update [HLAQSC].dbo.T4_MP_Detail_2 "
                + " set "
				+ " T4_MP_Detail_2.Month = '" + Month + "' "				+ ",T4_MP_Detail_2.PositionCode = '" + PositionCode + "' "				+ ",T4_MP_Detail_2.ConfigCode = '" + ConfigCode + "' "				+ ",T4_MP_Detail_2.Val = '" + Val + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP_Detail_2.Month = '" + Month + "' ";					sql += " and T4_MP_Detail_2.PositionCode = '" + PositionCode + "' ";					sql += " and T4_MP_Detail_2.ConfigCode = '" + ConfigCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T4_MP_Detail_2 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month = '" + Month + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(ConfigCode))			{				count++;				sql += (count > 1 ? "," : " ") + "ConfigCode = '" + ConfigCode + "' ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "Val = '" + Val + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP_Detail_2.Month = '" + Month + "' ";					sql += " and T4_MP_Detail_2.PositionCode = '" + PositionCode + "' ";					sql += " and T4_MP_Detail_2.ConfigCode = '" + ConfigCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T4_MP_Detail_2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP_Detail_2.Month = '" + Month + "' ";					sql += " and T4_MP_Detail_2.PositionCode = '" + PositionCode + "' ";					sql += " and T4_MP_Detail_2.ConfigCode = '" + ConfigCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}