using System;

namespace Web.AutoFiles
{
    public class T8_Report3
    {
        public T8_Report3()
        {
        }

		public string ID { get; set; }		public string Year { get; set; }		public string Month { get; set; }		public string Day { get; set; }		public string ZDCode { get; set; }		public string CCCode { get; set; }		public string PW_Xin { get; set; }		public string PW_Tie { get; set; }		public string PW_Tong { get; set; }		public string PW_Qian { get; set; }		public string W1 { get; set; }		public string W1_Xin { get; set; }		public string W1_Tie { get; set; }		public string W1_Tong { get; set; }		public string W1_Qian { get; set; }		public string W2 { get; set; }		public string W2_Xin { get; set; }		public string W2_Tie { get; set; }		public string W2_Tong { get; set; }		public string W2_Qian { get; set; }		public string EquiName { get; set; }		public string CheJian { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_Report3.ID "				+ ",T8_Report3.Year "				+ ",T8_Report3.Month "				+ ",T8_Report3.Day "				+ ",T8_Report3.ZDCode "				+ ",T8_Report3.CCCode "				+ ",T8_Report3.PW_Xin "				+ ",T8_Report3.PW_Tie "				+ ",T8_Report3.PW_Tong "				+ ",T8_Report3.PW_Qian "				+ ",T8_Report3.W1 "				+ ",T8_Report3.W1_Xin "				+ ",T8_Report3.W1_Tie "				+ ",T8_Report3.W1_Tong "				+ ",T8_Report3.W1_Qian "				+ ",T8_Report3.W2 "				+ ",T8_Report3.W2_Xin "				+ ",T8_Report3.W2_Tie "				+ ",T8_Report3.W2_Tong "				+ ",T8_Report3.W2_Qian "				+ ",T8_Report3.EquiName "				+ ",T8_Report3.CheJian "                + " from [HLAQSC].dbo.T8_Report3 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report3.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_Report3( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day ";			}			if (!String.IsNullOrEmpty(ZDCode))			{				count++;				sql += (count > 1 ? "," : " ") + "ZDCode ";			}			if (!String.IsNullOrEmpty(CCCode))			{				count++;				sql += (count > 1 ? "," : " ") + "CCCode ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Xin ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tie ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tong ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Qian ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "W1 ";			}			if (!String.IsNullOrEmpty(W1_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Xin ";			}			if (!String.IsNullOrEmpty(W1_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Tie ";			}			if (!String.IsNullOrEmpty(W1_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Tong ";			}			if (!String.IsNullOrEmpty(W1_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Qian ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "W2 ";			}			if (!String.IsNullOrEmpty(W2_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Xin ";			}			if (!String.IsNullOrEmpty(W2_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Tie ";			}			if (!String.IsNullOrEmpty(W2_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Tong ";			}			if (!String.IsNullOrEmpty(W2_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Qian ";			}			if (!String.IsNullOrEmpty(EquiName))			{				count++;				sql += (count > 1 ? "," : " ") + "EquiName ";			}			if (!String.IsNullOrEmpty(CheJian))			{				count++;				sql += (count > 1 ? "," : " ") + "CheJian ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Day + "' ";			}			if (!String.IsNullOrEmpty(ZDCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZDCode + "' ";			}			if (!String.IsNullOrEmpty(CCCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CCCode + "' ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Xin + "' ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Tie + "' ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Tong + "' ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PW_Qian + "' ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1 + "' ";			}			if (!String.IsNullOrEmpty(W1_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1_Xin + "' ";			}			if (!String.IsNullOrEmpty(W1_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1_Tie + "' ";			}			if (!String.IsNullOrEmpty(W1_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1_Tong + "' ";			}			if (!String.IsNullOrEmpty(W1_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1_Qian + "' ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2 + "' ";			}			if (!String.IsNullOrEmpty(W2_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2_Xin + "' ";			}			if (!String.IsNullOrEmpty(W2_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2_Tie + "' ";			}			if (!String.IsNullOrEmpty(W2_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2_Tong + "' ";			}			if (!String.IsNullOrEmpty(W2_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2_Qian + "' ";			}			if (!String.IsNullOrEmpty(EquiName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquiName + "' ";			}			if (!String.IsNullOrEmpty(CheJian))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CheJian + "' ";			}
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
                + " update [HLAQSC].dbo.T8_Report3 "
                + " set "
				+ " T8_Report3.ID = '" + ID + "' "				+ ",T8_Report3.Year = '" + Year + "' "				+ ",T8_Report3.Month = '" + Month + "' "				+ ",T8_Report3.Day = '" + Day + "' "				+ ",T8_Report3.ZDCode = '" + ZDCode + "' "				+ ",T8_Report3.CCCode = '" + CCCode + "' "				+ ",T8_Report3.PW_Xin = '" + PW_Xin + "' "				+ ",T8_Report3.PW_Tie = '" + PW_Tie + "' "				+ ",T8_Report3.PW_Tong = '" + PW_Tong + "' "				+ ",T8_Report3.PW_Qian = '" + PW_Qian + "' "				+ ",T8_Report3.W1 = '" + W1 + "' "				+ ",T8_Report3.W1_Xin = '" + W1_Xin + "' "				+ ",T8_Report3.W1_Tie = '" + W1_Tie + "' "				+ ",T8_Report3.W1_Tong = '" + W1_Tong + "' "				+ ",T8_Report3.W1_Qian = '" + W1_Qian + "' "				+ ",T8_Report3.W2 = '" + W2 + "' "				+ ",T8_Report3.W2_Xin = '" + W2_Xin + "' "				+ ",T8_Report3.W2_Tie = '" + W2_Tie + "' "				+ ",T8_Report3.W2_Tong = '" + W2_Tong + "' "				+ ",T8_Report3.W2_Qian = '" + W2_Qian + "' "				+ ",T8_Report3.EquiName = '" + EquiName + "' "				+ ",T8_Report3.CheJian = '" + CheJian + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report3.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_Report3 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Year))			{				count++;				sql += (count > 1 ? "," : " ") + "Year = '" + Year + "' ";			}			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month = '" + Month + "' ";			}			if (!String.IsNullOrEmpty(Day))			{				count++;				sql += (count > 1 ? "," : " ") + "Day = '" + Day + "' ";			}			if (!String.IsNullOrEmpty(ZDCode))			{				count++;				sql += (count > 1 ? "," : " ") + "ZDCode = '" + ZDCode + "' ";			}			if (!String.IsNullOrEmpty(CCCode))			{				count++;				sql += (count > 1 ? "," : " ") + "CCCode = '" + CCCode + "' ";			}			if (!String.IsNullOrEmpty(PW_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Xin = '" + PW_Xin + "' ";			}			if (!String.IsNullOrEmpty(PW_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tie = '" + PW_Tie + "' ";			}			if (!String.IsNullOrEmpty(PW_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Tong = '" + PW_Tong + "' ";			}			if (!String.IsNullOrEmpty(PW_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "PW_Qian = '" + PW_Qian + "' ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "W1 = '" + W1 + "' ";			}			if (!String.IsNullOrEmpty(W1_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Xin = '" + W1_Xin + "' ";			}			if (!String.IsNullOrEmpty(W1_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Tie = '" + W1_Tie + "' ";			}			if (!String.IsNullOrEmpty(W1_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Tong = '" + W1_Tong + "' ";			}			if (!String.IsNullOrEmpty(W1_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W1_Qian = '" + W1_Qian + "' ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "W2 = '" + W2 + "' ";			}			if (!String.IsNullOrEmpty(W2_Xin))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Xin = '" + W2_Xin + "' ";			}			if (!String.IsNullOrEmpty(W2_Tie))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Tie = '" + W2_Tie + "' ";			}			if (!String.IsNullOrEmpty(W2_Tong))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Tong = '" + W2_Tong + "' ";			}			if (!String.IsNullOrEmpty(W2_Qian))			{				count++;				sql += (count > 1 ? "," : " ") + "W2_Qian = '" + W2_Qian + "' ";			}			if (!String.IsNullOrEmpty(EquiName))			{				count++;				sql += (count > 1 ? "," : " ") + "EquiName = '" + EquiName + "' ";			}			if (!String.IsNullOrEmpty(CheJian))			{				count++;				sql += (count > 1 ? "," : " ") + "CheJian = '" + CheJian + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report3.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_Report3 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report3.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}