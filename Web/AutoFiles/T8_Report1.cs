using System;

namespace Web.AutoFiles
{
    public class T8_Report1
    {
        public T8_Report1()
        {
        }

		public string ID { get; set; }		public string Year { get; set; }		public string Month { get; set; }		public string Day { get; set; }		public string OrderBy { get; set; }		public string Type1 { get; set; }		public string Type2 { get; set; }		public string Val { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_Report1.ID "				+ ",T8_Report1.Year "				+ ",T8_Report1.Month "				+ ",T8_Report1.Day "				+ ",T8_Report1.OrderBy "				+ ",T8_Report1.Type1 "				+ ",T8_Report1.Type2 "				+ ",T8_Report1.Val "                + " from [HLAQSC].dbo.T8_Report1 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_Report1( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "Val ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Day + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Val + "' ";			}
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
                + " update [HLAQSC].dbo.T8_Report1 "
                + " set "
				+ " T8_Report1.ID = '" + ID + "' "				+ ",T8_Report1.Year = '" + Year + "' "				+ ",T8_Report1.Month = '" + Month + "' "				+ ",T8_Report1.Day = '" + Day + "' "				+ ",T8_Report1.OrderBy = '" + OrderBy + "' "				+ ",T8_Report1.Type1 = '" + Type1 + "' "				+ ",T8_Report1.Type2 = '" + Type2 + "' "				+ ",T8_Report1.Val = '" + Val + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_Report1 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year = '" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month = '" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day = '" + Day + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy = '" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 = '" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 = '" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(Val))			{				count++;				sql += (count > 1 ? "," : " ") + "Val = '" + Val + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_Report1 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}