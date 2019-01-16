using System;

namespace Web.AutoFiles
{
    public class T8_WR_Position
    {
        public T8_WR_Position()
        {
        }

		public string ID { get; set; }		public string WRID { get; set; }		public string PositionCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR_Position.ID "				+ ",T8_WR_Position.WRID "				+ ",T8_WR_Position.PositionCode "                + " from [HLAQSC].dbo.T8_WR_Position "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR_Position( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}
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
                + " update [HLAQSC].dbo.T8_WR_Position "
                + " set "
				+ " T8_WR_Position.ID = '" + ID + "' "				+ ",T8_WR_Position.WRID = '" + WRID + "' "				+ ",T8_WR_Position.PositionCode = '" + PositionCode + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR_Position "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WRID))			{				count++;				sql += (count > 1 ? "," : " ") + "WRID = '" + WRID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR_Position "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR_Position.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}