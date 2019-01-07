using System;

namespace Web.AutoFiles
{
    public class T6_Check_B4_JueJin
    {
        public T6_Check_B4_JueJin()
        {
        }

		public string ID { get; set; }		public string CID { get; set; }		public string ZD { get; set; }		public string CC { get; set; }		public string ZYM { get; set; }		public string GCLX { get; set; }		public string TX { get; set; }		public string TB { get; set; }		public string GG { get; set; }		public string DMJ { get; set; }		public string CD { get; set; }		public string TJ { get; set; }		public string JJL { get; set; }		public string ZHBM { get; set; }		public string FC { get; set; }		public string SGSJ { get; set; }		public string JT { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Check_B4_JueJin.ID "				+ ",T6_Check_B4_JueJin.CID "				+ ",T6_Check_B4_JueJin.ZD "				+ ",T6_Check_B4_JueJin.CC "				+ ",T6_Check_B4_JueJin.ZYM "				+ ",T6_Check_B4_JueJin.GCLX "				+ ",T6_Check_B4_JueJin.TX "				+ ",T6_Check_B4_JueJin.TB "				+ ",T6_Check_B4_JueJin.GG "				+ ",T6_Check_B4_JueJin.DMJ "				+ ",T6_Check_B4_JueJin.CD "				+ ",T6_Check_B4_JueJin.TJ "				+ ",T6_Check_B4_JueJin.JJL "				+ ",T6_Check_B4_JueJin.ZHBM "				+ ",T6_Check_B4_JueJin.FC "				+ ",T6_Check_B4_JueJin.SGSJ "				+ ",T6_Check_B4_JueJin.JT "                + " from [HLAQSC].dbo.T6_Check_B4_JueJin "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B4_JueJin.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Check_B4_JueJin( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC ";			}			if (!String.IsNullOrEmpty(ZYM))			{				count++;				sql += (count > 1 ? "," : " ") + "ZYM ";			}			if (!String.IsNullOrEmpty(GCLX))			{				count++;				sql += (count > 1 ? "," : " ") + "GCLX ";			}			if (!String.IsNullOrEmpty(TX))			{				count++;				sql += (count > 1 ? "," : " ") + "TX ";			}			if (!String.IsNullOrEmpty(TB))			{				count++;				sql += (count > 1 ? "," : " ") + "TB ";			}			if (!String.IsNullOrEmpty(GG))			{				count++;				sql += (count > 1 ? "," : " ") + "GG ";			}			if (!String.IsNullOrEmpty(DMJ))			{				count++;				sql += (count > 1 ? "," : " ") + "DMJ ";			}			if (!String.IsNullOrEmpty(CD))			{				count++;				sql += (count > 1 ? "," : " ") + "CD ";			}			if (!String.IsNullOrEmpty(TJ))			{				count++;				sql += (count > 1 ? "," : " ") + "TJ ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "JJL ";			}			if (!String.IsNullOrEmpty(ZHBM))			{				count++;				sql += (count > 1 ? "," : " ") + "ZHBM ";			}			if (!String.IsNullOrEmpty(FC))			{				count++;				sql += (count > 1 ? "," : " ") + "FC ";			}			if (!String.IsNullOrEmpty(SGSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "SGSJ ";			}			if (!String.IsNullOrEmpty(JT))			{				count++;				sql += (count > 1 ? "," : " ") + "JT ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CC + "' ";			}			if (!String.IsNullOrEmpty(ZYM))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZYM + "' ";			}			if (!String.IsNullOrEmpty(GCLX))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + GCLX + "' ";			}			if (!String.IsNullOrEmpty(TX))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + TX + "' ";			}			if (!String.IsNullOrEmpty(TB))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + TB + "' ";			}			if (!String.IsNullOrEmpty(GG))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + GG + "' ";			}			if (!String.IsNullOrEmpty(DMJ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DMJ + "' ";			}			if (!String.IsNullOrEmpty(CD))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CD + "' ";			}			if (!String.IsNullOrEmpty(TJ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + TJ + "' ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + JJL + "' ";			}			if (!String.IsNullOrEmpty(ZHBM))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZHBM + "' ";			}			if (!String.IsNullOrEmpty(FC))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FC + "' ";			}			if (!String.IsNullOrEmpty(SGSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + SGSJ + "' ";			}			if (!String.IsNullOrEmpty(JT))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + JT + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Check_B4_JueJin "
                + " set "
				+ " T6_Check_B4_JueJin.ID = '" + ID + "' "				+ ",T6_Check_B4_JueJin.CID = '" + CID + "' "				+ ",T6_Check_B4_JueJin.ZD = '" + ZD + "' "				+ ",T6_Check_B4_JueJin.CC = '" + CC + "' "				+ ",T6_Check_B4_JueJin.ZYM = '" + ZYM + "' "				+ ",T6_Check_B4_JueJin.GCLX = '" + GCLX + "' "				+ ",T6_Check_B4_JueJin.TX = '" + TX + "' "				+ ",T6_Check_B4_JueJin.TB = '" + TB + "' "				+ ",T6_Check_B4_JueJin.GG = '" + GG + "' "				+ ",T6_Check_B4_JueJin.DMJ = '" + DMJ + "' "				+ ",T6_Check_B4_JueJin.CD = '" + CD + "' "				+ ",T6_Check_B4_JueJin.TJ = '" + TJ + "' "				+ ",T6_Check_B4_JueJin.JJL = '" + JJL + "' "				+ ",T6_Check_B4_JueJin.ZHBM = '" + ZHBM + "' "				+ ",T6_Check_B4_JueJin.FC = '" + FC + "' "				+ ",T6_Check_B4_JueJin.SGSJ = '" + SGSJ + "' "				+ ",T6_Check_B4_JueJin.JT = '" + JT + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B4_JueJin.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Check_B4_JueJin "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID = '" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZD))			{				count++;				sql += (count > 1 ? "," : " ") + "ZD = '" + ZD + "' ";			}			if (!String.IsNullOrEmpty(CC))			{				count++;				sql += (count > 1 ? "," : " ") + "CC = '" + CC + "' ";			}			if (!String.IsNullOrEmpty(ZYM))			{				count++;				sql += (count > 1 ? "," : " ") + "ZYM = '" + ZYM + "' ";			}			if (!String.IsNullOrEmpty(GCLX))			{				count++;				sql += (count > 1 ? "," : " ") + "GCLX = '" + GCLX + "' ";			}			if (!String.IsNullOrEmpty(TX))			{				count++;				sql += (count > 1 ? "," : " ") + "TX = '" + TX + "' ";			}			if (!String.IsNullOrEmpty(TB))			{				count++;				sql += (count > 1 ? "," : " ") + "TB = '" + TB + "' ";			}			if (!String.IsNullOrEmpty(GG))			{				count++;				sql += (count > 1 ? "," : " ") + "GG = '" + GG + "' ";			}			if (!String.IsNullOrEmpty(DMJ))			{				count++;				sql += (count > 1 ? "," : " ") + "DMJ = '" + DMJ + "' ";			}			if (!String.IsNullOrEmpty(CD))			{				count++;				sql += (count > 1 ? "," : " ") + "CD = '" + CD + "' ";			}			if (!String.IsNullOrEmpty(TJ))			{				count++;				sql += (count > 1 ? "," : " ") + "TJ = '" + TJ + "' ";			}			if (!String.IsNullOrEmpty(JJL))			{				count++;				sql += (count > 1 ? "," : " ") + "JJL = '" + JJL + "' ";			}			if (!String.IsNullOrEmpty(ZHBM))			{				count++;				sql += (count > 1 ? "," : " ") + "ZHBM = '" + ZHBM + "' ";			}			if (!String.IsNullOrEmpty(FC))			{				count++;				sql += (count > 1 ? "," : " ") + "FC = '" + FC + "' ";			}			if (!String.IsNullOrEmpty(SGSJ))			{				count++;				sql += (count > 1 ? "," : " ") + "SGSJ = '" + SGSJ + "' ";			}			if (!String.IsNullOrEmpty(JT))			{				count++;				sql += (count > 1 ? "," : " ") + "JT = '" + JT + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B4_JueJin.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Check_B4_JueJin "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B4_JueJin.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}