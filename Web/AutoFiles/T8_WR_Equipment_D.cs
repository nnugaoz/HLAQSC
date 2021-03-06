using System;

namespace Web.AutoFiles
{
    public class T8_WR_Equipment_D
    {
        public T8_WR_Equipment_D()
        {
        }

		public string ID { get; set; }		public string WRID { get; set; }		public string EquipmentID { get; set; }		public string FKey { get; set; }		public string FType { get; set; }		public string Fvalue0 { get; set; }		public string FUnit0 { get; set; }		public string FValue1 { get; set; }		public string FUnit1 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR_Equipment_D.ID "				+ ",T8_WR_Equipment_D.WRID "				+ ",T8_WR_Equipment_D.EquipmentID "				+ ",T8_WR_Equipment_D.FKey "				+ ",T8_WR_Equipment_D.FType "				+ ",T8_WR_Equipment_D.Fvalue0 "				+ ",T8_WR_Equipment_D.FUnit0 "				+ ",T8_WR_Equipment_D.FValue1 "				+ ",T8_WR_Equipment_D.FUnit1 "                + " from [HLAQSC].dbo.T8_WR_Equipment_D "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Equipment_D.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR_Equipment_D( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FKey ";			}			if (!String.IsNullOrEmpty(FType))			{				count++;				sql += (count > 1 ? "," : " ") + "FType ";			}			if (!String.IsNullOrEmpty(Fvalue0))			{				count++;				sql += (count > 1 ? "," : " ") + "Fvalue0 ";			}			if (!String.IsNullOrEmpty(FUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit0 ";			}			if (!String.IsNullOrEmpty(FValue1))			{				count++;				sql += (count > 1 ? "," : " ") + "FValue1 ";			}			if (!String.IsNullOrEmpty(FUnit1))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit1 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WRID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FKey + "' ";			}			if (!String.IsNullOrEmpty(FType))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FType + "' ";			}			if (!String.IsNullOrEmpty(Fvalue0))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Fvalue0 + "' ";			}			if (!String.IsNullOrEmpty(FUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FUnit0 + "' ";			}			if (!String.IsNullOrEmpty(FValue1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FValue1 + "' ";			}			if (!String.IsNullOrEmpty(FUnit1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FUnit1 + "' ";			}
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
                + " update [HLAQSC].dbo.T8_WR_Equipment_D "
                + " set "
				+ " T8_WR_Equipment_D.ID = '" + ID + "' "				+ ",T8_WR_Equipment_D.WRID = '" + WRID + "' "				+ ",T8_WR_Equipment_D.EquipmentID = '" + EquipmentID + "' "				+ ",T8_WR_Equipment_D.FKey = '" + FKey + "' "				+ ",T8_WR_Equipment_D.FType = '" + FType + "' "				+ ",T8_WR_Equipment_D.Fvalue0 = '" + Fvalue0 + "' "				+ ",T8_WR_Equipment_D.FUnit0 = '" + FUnit0 + "' "				+ ",T8_WR_Equipment_D.FValue1 = '" + FValue1 + "' "				+ ",T8_WR_Equipment_D.FUnit1 = '" + FUnit1 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Equipment_D.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR_Equipment_D "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID = '" + WRID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FKey = '" + FKey + "' ";			}			if (!String.IsNullOrEmpty(FType))			{				count++;				sql += (count > 1 ? "," : " ") + "FType = '" + FType + "' ";			}			if (!String.IsNullOrEmpty(Fvalue0))			{				count++;				sql += (count > 1 ? "," : " ") + "Fvalue0 = '" + Fvalue0 + "' ";			}			if (!String.IsNullOrEmpty(FUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit0 = '" + FUnit0 + "' ";			}			if (!String.IsNullOrEmpty(FValue1))			{				count++;				sql += (count > 1 ? "," : " ") + "FValue1 = '" + FValue1 + "' ";			}			if (!String.IsNullOrEmpty(FUnit1))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit1 = '" + FUnit1 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Equipment_D.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR_Equipment_D "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Equipment_D.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}