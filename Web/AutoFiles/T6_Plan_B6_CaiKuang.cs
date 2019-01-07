using System;

namespace Web.AutoFiles
{
    public class T6_Plan_B6_CaiKuang
    {
        public T6_Plan_B6_CaiKuang()
        {
        }

		public string ID { get; set; }		public string PID { get; set; }		public string ZD { get; set; }		public string CC { get; set; }		public string CKLX { get; set; }		public string DZPW_X { get; set; }		public string DZPW_T { get; set; }		public string DZPW_C { get; set; }		public string DZPW_L { get; set; }		public string CKL { get; set; }		public string TCZL { get; set; }		public string WSL { get; set; }		public string JJL { get; set; }		public string KSSJ { get; set; }		public string JSSJ { get; set; }		public string BZ { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Plan_B6_CaiKuang.ID "				+ ",T6_Plan_B6_CaiKuang.PID "				+ ",T6_Plan_B6_CaiKuang.ZD "				+ ",T6_Plan_B6_CaiKuang.CC "				+ ",T6_Plan_B6_CaiKuang.CKLX "				+ ",T6_Plan_B6_CaiKuang.DZPW_X "				+ ",T6_Plan_B6_CaiKuang.DZPW_T "				+ ",T6_Plan_B6_CaiKuang.DZPW_C "				+ ",T6_Plan_B6_CaiKuang.DZPW_L "				+ ",T6_Plan_B6_CaiKuang.CKL "				+ ",T6_Plan_B6_CaiKuang.TCZL "				+ ",T6_Plan_B6_CaiKuang.WSL "				+ ",T6_Plan_B6_CaiKuang.JJL "				+ ",T6_Plan_B6_CaiKuang.KSSJ "				+ ",T6_Plan_B6_CaiKuang.JSSJ "				+ ",T6_Plan_B6_CaiKuang.BZ "                + " from [HLAQSC].dbo.T6_Plan_B6_CaiKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B6_CaiKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Plan_B6_CaiKuang( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC ";			}			if (!String.IsNullOrEmpty(CKLX))			{				count++;				sql += (count > 1 ? "," : " ") + "CKLX ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_X ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_T ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_C ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_L ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "CKL ";			}			if (!String.IsNullOrEmpty(TCZL))			{				count++;				sql += (count > 1 ? "," : " ") + "TCZL ";			}			if (!String.IsNullOrEmpty(WSL))			{				count++;				sql += (count > 1 ? "," : " ") + "WSL ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "JJL ";			}			if (!String.IsNullOrEmpty(KSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "KSSJ ";			}			if (!String.IsNullOrEmpty(JSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "JSSJ ";			}			if (!String.IsNullOrEmpty(BZ))			{				count++;				sql += (count > 1 ? "," : " ") + "BZ ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CC + "' ";			}			if (!String.IsNullOrEmpty(CKLX))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CKLX + "' ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_X + "' ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_T + "' ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_C + "' ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DZPW_L + "' ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CKL + "' ";			}			if (!String.IsNullOrEmpty(TCZL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + TCZL + "' ";			}			if (!String.IsNullOrEmpty(WSL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WSL + "' ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + JJL + "' ";			}			if (!String.IsNullOrEmpty(KSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + KSSJ + "' ";			}			if (!String.IsNullOrEmpty(JSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + JSSJ + "' ";			}			if (!String.IsNullOrEmpty(BZ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + BZ + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Plan_B6_CaiKuang "
                + " set "
				+ " T6_Plan_B6_CaiKuang.ID = '" + ID + "' "				+ ",T6_Plan_B6_CaiKuang.PID = '" + PID + "' "				+ ",T6_Plan_B6_CaiKuang.ZD = '" + ZD + "' "				+ ",T6_Plan_B6_CaiKuang.CC = '" + CC + "' "				+ ",T6_Plan_B6_CaiKuang.CKLX = '" + CKLX + "' "				+ ",T6_Plan_B6_CaiKuang.DZPW_X = '" + DZPW_X + "' "				+ ",T6_Plan_B6_CaiKuang.DZPW_T = '" + DZPW_T + "' "				+ ",T6_Plan_B6_CaiKuang.DZPW_C = '" + DZPW_C + "' "				+ ",T6_Plan_B6_CaiKuang.DZPW_L = '" + DZPW_L + "' "				+ ",T6_Plan_B6_CaiKuang.CKL = '" + CKL + "' "				+ ",T6_Plan_B6_CaiKuang.TCZL = '" + TCZL + "' "				+ ",T6_Plan_B6_CaiKuang.WSL = '" + WSL + "' "				+ ",T6_Plan_B6_CaiKuang.JJL = '" + JJL + "' "				+ ",T6_Plan_B6_CaiKuang.KSSJ = '" + KSSJ + "' "				+ ",T6_Plan_B6_CaiKuang.JSSJ = '" + JSSJ + "' "				+ ",T6_Plan_B6_CaiKuang.BZ = '" + BZ + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B6_CaiKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Plan_B6_CaiKuang "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID = '" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD = '" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC = '" + CC + "' ";			}			if (!String.IsNullOrEmpty(CKLX))			{				count++;				sql += (count > 1 ? "," : " ") + "CKLX = '" + CKLX + "' ";			}			if (!String.IsNullOrEmpty(DZPW_X))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_X = '" + DZPW_X + "' ";			}			if (!String.IsNullOrEmpty(DZPW_T))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_T = '" + DZPW_T + "' ";			}			if (!String.IsNullOrEmpty(DZPW_C))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_C = '" + DZPW_C + "' ";			}			if (!String.IsNullOrEmpty(DZPW_L))			{				count++;				sql += (count > 1 ? "," : " ") + "DZPW_L = '" + DZPW_L + "' ";			}			if (!String.IsNullOrEmpty(CKL))			{				count++;				sql += (count > 1 ? "," : " ") + "CKL = '" + CKL + "' ";			}			if (!String.IsNullOrEmpty(TCZL))			{				count++;				sql += (count > 1 ? "," : " ") + "TCZL = '" + TCZL + "' ";			}			if (!String.IsNullOrEmpty(WSL))			{				count++;				sql += (count > 1 ? "," : " ") + "WSL = '" + WSL + "' ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "JJL = '" + JJL + "' ";			}			if (!String.IsNullOrEmpty(KSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "KSSJ = '" + KSSJ + "' ";			}			if (!String.IsNullOrEmpty(JSSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "JSSJ = '" + JSSJ + "' ";			}			if (!String.IsNullOrEmpty(BZ))			{				count++;				sql += (count > 1 ? "," : " ") + "BZ = '" + BZ + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B6_CaiKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Plan_B6_CaiKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B6_CaiKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}