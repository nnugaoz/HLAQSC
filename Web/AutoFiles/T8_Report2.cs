using System;

namespace Web.AutoFiles
{
    public class T8_Report2
    {
        public T8_Report2()
        {
        }

		public string ID { get; set; }		public string Year { get; set; }		public string Month { get; set; }		public string Day { get; set; }		public string OrderBy { get; set; }		public string Type1 { get; set; }		public string Type2 { get; set; }		public string PW_Xin { get; set; }		public string PW_Tie { get; set; }		public string PW_Tong { get; set; }		public string PW_Qian { get; set; }		public string W { get; set; }		public string W_Xin { get; set; }		public string W_Tie { get; set; }		public string W_Tong { get; set; }		public string W_Qian { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_Report2.ID "				+ ",T8_Report2.Year "				+ ",T8_Report2.Month "				+ ",T8_Report2.Day "				+ ",T8_Report2.OrderBy "				+ ",T8_Report2.Type1 "				+ ",T8_Report2.Type2 "				+ ",T8_Report2.PW_Xin "				+ ",T8_Report2.PW_Tie "				+ ",T8_Report2.PW_Tong "				+ ",T8_Report2.PW_Qian "				+ ",T8_Report2.W "				+ ",T8_Report2.W_Xin "				+ ",T8_Report2.W_Tie "				+ ",T8_Report2.W_Tong "				+ ",T8_Report2.W_Qian "                + " from [HLAQSC].dbo.T8_Report2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_Report2( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Xin ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tie ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tong ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Qian ";			}			if (!String.IsNullOrEmpty(W))			{				count++;				sql += (count > 1 ? "," : " ") + "W ";			}			if (!String.IsNullOrEmpty(W_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Xin ";			}			if (!String.IsNullOrEmpty(W_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Tie ";			}			if (!String.IsNullOrEmpty(W_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Tong ";			}			if (!String.IsNullOrEmpty(W_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Qian ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Day + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Xin + "' ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Tie + "' ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Tong + "' ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Qian + "' ";			}			if (!String.IsNullOrEmpty(W))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W + "' ";			}			if (!String.IsNullOrEmpty(W_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W_Xin + "' ";			}			if (!String.IsNullOrEmpty(W_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W_Tie + "' ";			}			if (!String.IsNullOrEmpty(W_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W_Tong + "' ";			}			if (!String.IsNullOrEmpty(W_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W_Qian + "' ";			}
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
                + " update [HLAQSC].dbo.T8_Report2 "
                + " set "
				+ " T8_Report2.ID = '" + ID + "' "				+ ",T8_Report2.Year = '" + Year + "' "				+ ",T8_Report2.Month = '" + Month + "' "				+ ",T8_Report2.Day = '" + Day + "' "				+ ",T8_Report2.OrderBy = '" + OrderBy + "' "				+ ",T8_Report2.Type1 = '" + Type1 + "' "				+ ",T8_Report2.Type2 = '" + Type2 + "' "				+ ",T8_Report2.PW_Xin = '" + PW_Xin + "' "				+ ",T8_Report2.PW_Tie = '" + PW_Tie + "' "				+ ",T8_Report2.PW_Tong = '" + PW_Tong + "' "				+ ",T8_Report2.PW_Qian = '" + PW_Qian + "' "				+ ",T8_Report2.W = '" + W + "' "				+ ",T8_Report2.W_Xin = '" + W_Xin + "' "				+ ",T8_Report2.W_Tie = '" + W_Tie + "' "				+ ",T8_Report2.W_Tong = '" + W_Tong + "' "				+ ",T8_Report2.W_Qian = '" + W_Qian + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_Report2 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year = '" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month = '" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day = '" + Day + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy = '" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 = '" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 = '" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Xin = '" + PW_Xin + "' ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tie = '" + PW_Tie + "' ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tong = '" + PW_Tong + "' ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Qian = '" + PW_Qian + "' ";			}			if (!String.IsNullOrEmpty(W))			{				count++;				sql += (count > 1 ? "," : " ") + "W = '" + W + "' ";			}			if (!String.IsNullOrEmpty(W_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Xin = '" + W_Xin + "' ";			}			if (!String.IsNullOrEmpty(W_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Tie = '" + W_Tie + "' ";			}			if (!String.IsNullOrEmpty(W_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Tong = '" + W_Tong + "' ";			}			if (!String.IsNullOrEmpty(W_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W_Qian = '" + W_Qian + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_Report2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}