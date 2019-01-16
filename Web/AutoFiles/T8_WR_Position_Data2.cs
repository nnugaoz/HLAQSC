using System;

namespace Web.AutoFiles
{
    public class T8_WR_Position_Data2
    {
        public T8_WR_Position_Data2()
        {
        }

		public string ID { get; set; }		public string WRID { get; set; }		public string PositionCode { get; set; }		public string EquipmentID { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR_Position_Data2.ID "				+ ",T8_WR_Position_Data2.WRID "				+ ",T8_WR_Position_Data2.PositionCode "				+ ",T8_WR_Position_Data2.EquipmentID "                + " from [HLAQSC].dbo.T8_WR_Position_Data2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR_Position_Data2( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}
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
                + " update [HLAQSC].dbo.T8_WR_Position_Data2 "
                + " set "
				+ " T8_WR_Position_Data2.ID = '" + ID + "' "				+ ",T8_WR_Position_Data2.WRID = '" + WRID + "' "				+ ",T8_WR_Position_Data2.PositionCode = '" + PositionCode + "' "				+ ",T8_WR_Position_Data2.EquipmentID = '" + EquipmentID + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR_Position_Data2 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID = '" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR_Position_Data2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data2.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}