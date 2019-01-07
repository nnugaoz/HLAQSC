using System;

namespace Web.AutoFiles
{
    public class T6_Plan_B3_CaiJue
    {
        public T6_Plan_B3_CaiJue()
        {
        }

		public string ID { get; set; }		public string PID { get; set; }		public string ZB1 { get; set; }		public string ZB2 { get; set; }		public string DW { get; set; }		public string NDJH { get; set; }		public string YJWC1 { get; set; }		public string WCL1 { get; set; }		public string BYJH { get; set; }		public string YJWC2 { get; set; }		public string WCL2 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Plan_B3_CaiJue.ID "				+ ",T6_Plan_B3_CaiJue.PID "				+ ",T6_Plan_B3_CaiJue.ZB1 "				+ ",T6_Plan_B3_CaiJue.ZB2 "				+ ",T6_Plan_B3_CaiJue.DW "				+ ",T6_Plan_B3_CaiJue.NDJH "				+ ",T6_Plan_B3_CaiJue.YJWC1 "				+ ",T6_Plan_B3_CaiJue.WCL1 "				+ ",T6_Plan_B3_CaiJue.BYJH "				+ ",T6_Plan_B3_CaiJue.YJWC2 "				+ ",T6_Plan_B3_CaiJue.WCL2 "                + " from [HLAQSC].dbo.T6_Plan_B3_CaiJue "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Plan_B3_CaiJue( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW ";			}			if (!String.IsNullOrEmpty(NDJH))			{				count++;				sql += (count > 1 ? "," : " ") + "NDJH ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC1 ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL1 ";			}			if (!String.IsNullOrEmpty(BYJH))			{				count++;				sql += (count > 1 ? "," : " ") + "BYJH ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC2 ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL2 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DW + "' ";			}			if (!String.IsNullOrEmpty(NDJH))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + NDJH + "' ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + YJWC1 + "' ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WCL1 + "' ";			}			if (!String.IsNullOrEmpty(BYJH))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + BYJH + "' ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + YJWC2 + "' ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WCL2 + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Plan_B3_CaiJue "
                + " set "
				+ " T6_Plan_B3_CaiJue.ID = '" + ID + "' "				+ ",T6_Plan_B3_CaiJue.PID = '" + PID + "' "				+ ",T6_Plan_B3_CaiJue.ZB1 = '" + ZB1 + "' "				+ ",T6_Plan_B3_CaiJue.ZB2 = '" + ZB2 + "' "				+ ",T6_Plan_B3_CaiJue.DW = '" + DW + "' "				+ ",T6_Plan_B3_CaiJue.NDJH = '" + NDJH + "' "				+ ",T6_Plan_B3_CaiJue.YJWC1 = '" + YJWC1 + "' "				+ ",T6_Plan_B3_CaiJue.WCL1 = '" + WCL1 + "' "				+ ",T6_Plan_B3_CaiJue.BYJH = '" + BYJH + "' "				+ ",T6_Plan_B3_CaiJue.YJWC2 = '" + YJWC2 + "' "				+ ",T6_Plan_B3_CaiJue.WCL2 = '" + WCL2 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Plan_B3_CaiJue "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID = '" + PID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 = '" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 = '" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW = '" + DW + "' ";			}			if (!String.IsNullOrEmpty(NDJH))			{				count++;				sql += (count > 1 ? "," : " ") + "NDJH = '" + NDJH + "' ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC1 = '" + YJWC1 + "' ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL1 = '" + WCL1 + "' ";			}			if (!String.IsNullOrEmpty(BYJH))			{				count++;				sql += (count > 1 ? "," : " ") + "BYJH = '" + BYJH + "' ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC2 = '" + YJWC2 + "' ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL2 = '" + WCL2 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Plan_B3_CaiJue "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}