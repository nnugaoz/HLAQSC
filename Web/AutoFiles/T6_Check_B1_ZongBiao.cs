using System;

namespace Web.AutoFiles
{
    public class T6_Check_B1_ZongBiao
    {
        public T6_Check_B1_ZongBiao()
        {
        }

		public string ID { get; set; }		public string CID { get; set; }		public string ZB1 { get; set; }		public string ZB2 { get; set; }		public string DW { get; set; }		public string BYYS { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Check_B1_ZongBiao.ID "				+ ",T6_Check_B1_ZongBiao.CID "				+ ",T6_Check_B1_ZongBiao.ZB1 "				+ ",T6_Check_B1_ZongBiao.ZB2 "				+ ",T6_Check_B1_ZongBiao.DW "				+ ",T6_Check_B1_ZongBiao.BYYS "                + " from [HLAQSC].dbo.T6_Check_B1_ZongBiao "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B1_ZongBiao.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Check_B1_ZongBiao( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "BYYS ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DW + "' ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + BYYS + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Check_B1_ZongBiao "
                + " set "
				+ " T6_Check_B1_ZongBiao.ID = '" + ID + "' "				+ ",T6_Check_B1_ZongBiao.CID = '" + CID + "' "				+ ",T6_Check_B1_ZongBiao.ZB1 = '" + ZB1 + "' "				+ ",T6_Check_B1_ZongBiao.ZB2 = '" + ZB2 + "' "				+ ",T6_Check_B1_ZongBiao.DW = '" + DW + "' "				+ ",T6_Check_B1_ZongBiao.BYYS = '" + BYYS + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B1_ZongBiao.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Check_B1_ZongBiao "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(CID))			{				count++;				sql += (count > 1 ? "," : " ") + "CID = '" + CID + "' ";			}			if (!String.IsNullOrEmpty(ZB1))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB1 = '" + ZB1 + "' ";			}			if (!String.IsNullOrEmpty(ZB2))			{				count++;				sql += (count > 1 ? "," : " ") + "ZB2 = '" + ZB2 + "' ";			}			if (!String.IsNullOrEmpty(DW))			{				count++;				sql += (count > 1 ? "," : " ") + "DW = '" + DW + "' ";			}			if (!String.IsNullOrEmpty(BYYS))			{				count++;				sql += (count > 1 ? "," : " ") + "BYYS = '" + BYYS + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B1_ZongBiao.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Check_B1_ZongBiao "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check_B1_ZongBiao.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}