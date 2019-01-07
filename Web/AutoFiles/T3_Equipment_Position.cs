using System;

namespace Web.AutoFiles
{
    public class T3_Equipment_Position
    {
        public T3_Equipment_Position()
        {
        }

		public string EquipmentID { get; set; }		public string PositionCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Equipment_Position.EquipmentID "				+ ",T3_Equipment_Position.PositionCode "                + " from [HLAQSC].dbo.T3_Equipment_Position "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment_Position.EquipmentID = '" + EquipmentID + "' ";					sql += " and T3_Equipment_Position.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Equipment_Position( ";

            int count = 0;
			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}
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
                + " update [HLAQSC].dbo.T3_Equipment_Position "
                + " set "
				+ " T3_Equipment_Position.EquipmentID = '" + EquipmentID + "' "				+ ",T3_Equipment_Position.PositionCode = '" + PositionCode + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment_Position.EquipmentID = '" + EquipmentID + "' ";					sql += " and T3_Equipment_Position.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Equipment_Position "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment_Position.EquipmentID = '" + EquipmentID + "' ";					sql += " and T3_Equipment_Position.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Equipment_Position "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment_Position.EquipmentID = '" + EquipmentID + "' ";					sql += " and T3_Equipment_Position.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}