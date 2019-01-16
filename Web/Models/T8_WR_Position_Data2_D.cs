using MyTool.DB;
using System;
using System.Data;

namespace Web.Models
{
    public class T8_WR_Position_Data2_D
    {
        public T8_WR_Position_Data2_D()
        {
        }

		public string ID { get; set; }
		public string WRID { get; set; }
		public string PositionCode { get; set; }
		public string EquipmentID { get; set; }
		public string FKey { get; set; }
		public string FType { get; set; }
		public string Fvalue0 { get; set; }
		public string FUnit0 { get; set; }
		public string FValue1 { get; set; }
		public string FUnit1 { get; set; }

        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR_Position_Data2_D.ID "
				+ ",T8_WR_Position_Data2_D.WRID "
				+ ",T8_WR_Position_Data2_D.PositionCode "
				+ ",T8_WR_Position_Data2_D.EquipmentID "
				+ ",T8_WR_Position_Data2_D.FKey "
				+ ",T8_WR_Position_Data2_D.FType "
				+ ",T8_WR_Position_Data2_D.Fvalue0 "
				+ ",T8_WR_Position_Data2_D.FUnit0 "
				+ ",T8_WR_Position_Data2_D.FValue1 "
				+ ",T8_WR_Position_Data2_D.FUnit1 "
                + " from [HLAQSC].dbo.T8_WR_Position_Data2_D "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
				{
					sql += " and T8_WR_Position_Data2_D.ID = '" + ID + "' ";
				}
				else
				{
					sql += where;
				}

            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR_Position_Data2_D( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "ID ";
			}
			if (!String.IsNullOrEmpty(WRID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "WRID ";
			}
			if (!String.IsNullOrEmpty(PositionCode))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "PositionCode ";
			}
			if (!String.IsNullOrEmpty(EquipmentID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "EquipmentID ";
			}
			if (!String.IsNullOrEmpty(FKey))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FKey ";
			}
			if (!String.IsNullOrEmpty(FType))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FType ";
			}
			if (!String.IsNullOrEmpty(Fvalue0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "Fvalue0 ";
			}
			if (!String.IsNullOrEmpty(FUnit0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FUnit0 ";
			}
			if (!String.IsNullOrEmpty(FValue1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FValue1 ";
			}
			if (!String.IsNullOrEmpty(FUnit1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FUnit1 ";
			}

            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";
			}
			if (!String.IsNullOrEmpty(WRID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + WRID + "' ";
			}
			if (!String.IsNullOrEmpty(PositionCode))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";
			}
			if (!String.IsNullOrEmpty(EquipmentID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";
			}
			if (!String.IsNullOrEmpty(FKey))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + FKey + "' ";
			}
			if (!String.IsNullOrEmpty(FType))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + FType + "' ";
			}
			if (!String.IsNullOrEmpty(Fvalue0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + Fvalue0 + "' ";
			}
			if (!String.IsNullOrEmpty(FUnit0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + FUnit0 + "' ";
			}
			if (!String.IsNullOrEmpty(FValue1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + FValue1 + "' ";
			}
			if (!String.IsNullOrEmpty(FUnit1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "'" + FUnit1 + "' ";
			}

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
                + " update [HLAQSC].dbo.T8_WR_Position_Data2_D "
                + " set "
				+ " T8_WR_Position_Data2_D.ID = '" + ID + "' "
				+ ",T8_WR_Position_Data2_D.WRID = '" + WRID + "' "
				+ ",T8_WR_Position_Data2_D.PositionCode = '" + PositionCode + "' "
				+ ",T8_WR_Position_Data2_D.EquipmentID = '" + EquipmentID + "' "
				+ ",T8_WR_Position_Data2_D.FKey = '" + FKey + "' "
				+ ",T8_WR_Position_Data2_D.FType = '" + FType + "' "
				+ ",T8_WR_Position_Data2_D.Fvalue0 = '" + Fvalue0 + "' "
				+ ",T8_WR_Position_Data2_D.FUnit0 = '" + FUnit0 + "' "
				+ ",T8_WR_Position_Data2_D.FValue1 = '" + FValue1 + "' "
				+ ",T8_WR_Position_Data2_D.FUnit1 = '" + FUnit1 + "' "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
				{
					sql += " and T8_WR_Position_Data2_D.ID = '" + ID + "' ";
				}
				else
				{
					sql += where;
				}

            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR_Position_Data2_D "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";
			}
			if (!String.IsNullOrEmpty(WRID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "WRID = '" + WRID + "' ";
			}
			if (!String.IsNullOrEmpty(PositionCode))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";
			}
			if (!String.IsNullOrEmpty(EquipmentID))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";
			}
			if (!String.IsNullOrEmpty(FKey))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FKey = '" + FKey + "' ";
			}
			if (!String.IsNullOrEmpty(FType))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FType = '" + FType + "' ";
			}
			if (!String.IsNullOrEmpty(Fvalue0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "Fvalue0 = '" + Fvalue0 + "' ";
			}
			if (!String.IsNullOrEmpty(FUnit0))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FUnit0 = '" + FUnit0 + "' ";
			}
			if (!String.IsNullOrEmpty(FValue1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FValue1 = '" + FValue1 + "' ";
			}
			if (!String.IsNullOrEmpty(FUnit1))
			{
				count++;
				sql += (count > 1 ? "," : " ") + "FUnit1 = '" + FUnit1 + "' ";
			}

            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
				{
					sql += " and T8_WR_Position_Data2_D.ID = '" + ID + "' ";
				}
				else
				{
					sql += where;
				}

            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR_Position_Data2_D "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
				{
					sql += " and T8_WR_Position_Data2_D.ID = '" + ID + "' ";
				}
				else
				{
					sql += where;
				}

            return true;
        }

        public int Get_ByWRID(ref DataTable dt)
        {
            String lSQL = "";
            lSQL += "SELECT ";
            lSQL += " T1.ID";
            lSQL += ", T1.WRID";
            lSQL += ", T1.PositionCode";
            lSQL += ", T1.EquipmentID";
            lSQL += ", T1.FKey";
            lSQL += ", T1.FType";
            lSQL += ", T1.Fvalue0 AS CarCnt";
            lSQL += ", T1.FUnit0 AS CarTypeName";
            lSQL += ", T1.FValue1 AS Weight";
            lSQL += ", T1.FUnit1";
            lSQL += ", T2.Title AS PositionName";
            lSQL += ", T3.Title AS EquipmentName";
            lSQL += ", T4.Type AS MineTypeCode";
            lSQL += ", T4.Title AS MineTypeName";
            lSQL += " FROM T8_WR_Position_Data2_D T1";
            lSQL += " LEFT JOIN T2_Position T2 ON T1.PositionCode=T2.Code";
            lSQL += " LEFT JOIN T3_Equipment T3 ON T1.EquipmentID=T3.ID";
            lSQL += " LEFT JOIN T3_Dynamic_FieldType T4 ON T1.FKey=T4.DFKey AND T1.FType=T4.Type";
            lSQL += " WHERE T1.WRID='" + WRID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref dt);
        }
    }
}