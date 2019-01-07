using System;

namespace Web.AutoFiles
{
    public class T6_Check_B3_CaiJue
    {
        public T6_Check_B3_CaiJue()
        {
        }

		public string ID { get; set; }		public string CID { get; set; }		public string ZB1 { get; set; }		public string ZB2 { get; set; }		public string DW { get; set; }		public string NDYS { get; set; }		public string YJWC1 { get; set; }		public string WCL1 { get; set; }		public string BYYS { get; set; }		public string YJWC2 { get; set; }		public string WCL2 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Check_B3_CaiJue.ID "				+ ",T6_Check_B3_CaiJue.CID "				+ ",T6_Check_B3_CaiJue.ZB1 "				+ ",T6_Check_B3_CaiJue.ZB2 "				+ ",T6_Check_B3_CaiJue.DW "				+ ",T6_Check_B3_CaiJue.NDYS "				+ ",T6_Check_B3_CaiJue.YJWC1 "				+ ",T6_Check_B3_CaiJue.WCL1 "				+ ",T6_Check_B3_CaiJue.BYYS "				+ ",T6_Check_B3_CaiJue.YJWC2 "				+ ",T6_Check_B3_CaiJue.WCL2 "                + " from [HLAQSC].dbo.T6_Check_B3_CaiJue "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Check_B3_CaiJue( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW ";			}			if (!String.IsNullOrEmpty(NDYS))			{				count++;				sql += (count > 1 ? "," : " ") + "NDYS ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC1 ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL1 ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "BYYS ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC2 ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL2 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DW + "' ";			}			if (!String.IsNullOrEmpty(NDYS))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + NDYS + "' ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + YJWC1 + "' ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WCL1 + "' ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + BYYS + "' ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + YJWC2 + "' ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WCL2 + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Check_B3_CaiJue "
                + " set "
				+ " T6_Check_B3_CaiJue.ID = '" + ID + "' "				+ ",T6_Check_B3_CaiJue.CID = '" + CID + "' "				+ ",T6_Check_B3_CaiJue.ZB1 = '" + ZB1 + "' "				+ ",T6_Check_B3_CaiJue.ZB2 = '" + ZB2 + "' "				+ ",T6_Check_B3_CaiJue.DW = '" + DW + "' "				+ ",T6_Check_B3_CaiJue.NDYS = '" + NDYS + "' "				+ ",T6_Check_B3_CaiJue.YJWC1 = '" + YJWC1 + "' "				+ ",T6_Check_B3_CaiJue.WCL1 = '" + WCL1 + "' "				+ ",T6_Check_B3_CaiJue.BYYS = '" + BYYS + "' "				+ ",T6_Check_B3_CaiJue.YJWC2 = '" + YJWC2 + "' "				+ ",T6_Check_B3_CaiJue.WCL2 = '" + WCL2 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Check_B3_CaiJue "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID = '" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 = '" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 = '" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW = '" + DW + "' ";			}			if (!String.IsNullOrEmpty(NDYS))			{				count++;				sql += (count > 1 ? "," : " ") + "NDYS = '" + NDYS + "' ";			}			if (!String.IsNullOrEmpty(YJWC1))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC1 = '" + YJWC1 + "' ";			}			if (!String.IsNullOrEmpty(WCL1))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL1 = '" + WCL1 + "' ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "BYYS = '" + BYYS + "' ";			}			if (!String.IsNullOrEmpty(YJWC2))			{				count++;				sql += (count > 1 ? "," : " ") + "YJWC2 = '" + YJWC2 + "' ";			}			if (!String.IsNullOrEmpty(WCL2))			{				count++;				sql += (count > 1 ? "," : " ") + "WCL2 = '" + WCL2 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Check_B3_CaiJue "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B3_CaiJue.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}