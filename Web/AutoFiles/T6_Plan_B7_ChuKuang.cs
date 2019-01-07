using System;

namespace Web.AutoFiles
{
    public class T6_Plan_B7_ChuKuang
    {
        public T6_Plan_B7_ChuKuang()
        {
        }

		public string ID { get; set; }		public string PID { get; set; }		public string ZD { get; set; }		public string CC { get; set; }		public string XHKL { get; set; }		public string DZPW_X { get; set; }		public string DZPW_T { get; set; }		public string DZPW_C { get; set; }		public string DZPW_L { get; set; }		public string PHL { get; set; }		public string SSL { get; set; }		public string CKPW_X { get; set; }		public string CKPW_T { get; set; }		public string CKL { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Plan_B7_ChuKuang.ID "				+ ",T6_Plan_B7_ChuKuang.PID "				+ ",T6_Plan_B7_ChuKuang.ZD "				+ ",T6_Plan_B7_ChuKuang.CC "				+ ",T6_Plan_B7_ChuKuang.XHKL "				+ ",T6_Plan_B7_ChuKuang.DZPW_X "				+ ",T6_Plan_B7_ChuKuang.DZPW_T "				+ ",T6_Plan_B7_ChuKuang.DZPW_C "				+ ",T6_Plan_B7_ChuKuang.DZPW_L "				+ ",T6_Plan_B7_ChuKuang.PHL "				+ ",T6_Plan_B7_ChuKuang.SSL "				+ ",T6_Plan_B7_ChuKuang.CKPW_X "				+ ",T6_Plan_B7_ChuKuang.CKPW_T "				+ ",T6_Plan_B7_ChuKuang.CKL "                + " from [HLAQSC].dbo.T6_Plan_B7_ChuKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B7_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Plan_B7_ChuKuang( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC ";			}			if (!String.IsNullOrEmpty(XHKL))			{				count++;				sql += (count > 1 ? "," : " ") + "XHKL ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_X ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_T ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_C ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_L ";			}			if (!String.IsNullOrEmpty(PHL))			{				count++;				sql += (count > 1 ? "," : " ") + "PHL ";			}			if (!String.IsNullOrEmpty(SSL))			{				count++;				sql += (count > 1 ? "," : " ") + "SSL ";			}			if (!String.IsNullOrEmpty(CKPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "CKPW_X ";			}			if (!String.IsNullOrEmpty(CKPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "CKPW_T ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "CKL ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CC + "' ";			}			if (!String.IsNullOrEmpty(XHKL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + XHKL + "' ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_X + "' ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_T + "' ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_C + "' ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_L + "' ";			}			if (!String.IsNullOrEmpty(PHL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PHL + "' ";			}			if (!String.IsNullOrEmpty(SSL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + SSL + "' ";			}			if (!String.IsNullOrEmpty(CKPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CKPW_X + "' ";			}			if (!String.IsNullOrEmpty(CKPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CKPW_T + "' ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CKL + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Plan_B7_ChuKuang "
                + " set "
				+ " T6_Plan_B7_ChuKuang.ID = '" + ID + "' "				+ ",T6_Plan_B7_ChuKuang.PID = '" + PID + "' "				+ ",T6_Plan_B7_ChuKuang.ZD = '" + ZD + "' "				+ ",T6_Plan_B7_ChuKuang.CC = '" + CC + "' "				+ ",T6_Plan_B7_ChuKuang.XHKL = '" + XHKL + "' "				+ ",T6_Plan_B7_ChuKuang.DZPW_X = '" + DZPW_X + "' "				+ ",T6_Plan_B7_ChuKuang.DZPW_T = '" + DZPW_T + "' "				+ ",T6_Plan_B7_ChuKuang.DZPW_C = '" + DZPW_C + "' "				+ ",T6_Plan_B7_ChuKuang.DZPW_L = '" + DZPW_L + "' "				+ ",T6_Plan_B7_ChuKuang.PHL = '" + PHL + "' "				+ ",T6_Plan_B7_ChuKuang.SSL = '" + SSL + "' "				+ ",T6_Plan_B7_ChuKuang.CKPW_X = '" + CKPW_X + "' "				+ ",T6_Plan_B7_ChuKuang.CKPW_T = '" + CKPW_T + "' "				+ ",T6_Plan_B7_ChuKuang.CKL = '" + CKL + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B7_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Plan_B7_ChuKuang "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID = '" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD = '" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC = '" + CC + "' ";			}			if (!String.IsNullOrEmpty(XHKL))			{				count++;				sql += (count > 1 ? "," : " ") + "XHKL = '" + XHKL + "' ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_X = '" + DZPW_X + "' ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_T = '" + DZPW_T + "' ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_C = '" + DZPW_C + "' ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_L = '" + DZPW_L + "' ";			}			if (!String.IsNullOrEmpty(PHL))			{				count++;				sql += (count > 1 ? "," : " ") + "PHL = '" + PHL + "' ";			}			if (!String.IsNullOrEmpty(SSL))			{				count++;				sql += (count > 1 ? "," : " ") + "SSL = '" + SSL + "' ";			}			if (!String.IsNullOrEmpty(CKPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "CKPW_X = '" + CKPW_X + "' ";			}			if (!String.IsNullOrEmpty(CKPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "CKPW_T = '" + CKPW_T + "' ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "CKL = '" + CKL + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B7_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Plan_B7_ChuKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B7_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}