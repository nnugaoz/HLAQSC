using System;

namespace Web.AutoFiles
{
    public class T8_WR_Position_Data1
    {
        public T8_WR_Position_Data1()
        {
        }

		public string ID { get; set; }		public string WRID { get; set; }		public string PositionCode { get; set; }		public string FKey { get; set; }		public string Fvalue { get; set; }		public string FUnit { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR_Position_Data1.ID "				+ ",T8_WR_Position_Data1.WRID "				+ ",T8_WR_Position_Data1.PositionCode "				+ ",T8_WR_Position_Data1.FKey "				+ ",T8_WR_Position_Data1.Fvalue "				+ ",T8_WR_Position_Data1.FUnit "                + " from [HLAQSC].dbo.T8_WR_Position_Data1 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR_Position_Data1( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FKey ";			}			if (!String.IsNullOrEmpty(Fvalue))			{				count++;				sql += (count > 1 ? "," : " ") + "Fvalue ";			}			if (!String.IsNullOrEmpty(FUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FKey + "' ";			}			if (!String.IsNullOrEmpty(Fvalue))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Fvalue + "' ";			}			if (!String.IsNullOrEmpty(FUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FUnit + "' ";			}
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
                + " update [HLAQSC].dbo.T8_WR_Position_Data1 "
                + " set "
				+ " T8_WR_Position_Data1.ID = '" + ID + "' "				+ ",T8_WR_Position_Data1.WRID = '" + WRID + "' "				+ ",T8_WR_Position_Data1.PositionCode = '" + PositionCode + "' "				+ ",T8_WR_Position_Data1.FKey = '" + FKey + "' "				+ ",T8_WR_Position_Data1.Fvalue = '" + Fvalue + "' "				+ ",T8_WR_Position_Data1.FUnit = '" + FUnit + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR_Position_Data1 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID = '" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(FKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FKey = '" + FKey + "' ";			}			if (!String.IsNullOrEmpty(Fvalue))			{				count++;				sql += (count > 1 ? "," : " ") + "Fvalue = '" + Fvalue + "' ";			}			if (!String.IsNullOrEmpty(FUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FUnit = '" + FUnit + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR_Position_Data1 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position_Data1.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}