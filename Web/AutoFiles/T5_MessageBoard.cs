using System;

namespace Web.AutoFiles
{
    public class T5_MessageBoard
    {
        public T5_MessageBoard()
        {
        }

		public string ID { get; set; }		public string PositionCode { get; set; }		public string UserID { get; set; }		public string Remark { get; set; }		public string Date { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_MessageBoard.ID "				+ ",T5_MessageBoard.PositionCode "				+ ",T5_MessageBoard.UserID "				+ ",T5_MessageBoard.Remark "				+ ",T5_MessageBoard.Date "                + " from [HLAQSC].dbo.T5_MessageBoard "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_MessageBoard.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_MessageBoard( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark ";			}			if (!String.IsNullOrEmpty(Date))			{				count++;				sql += (count > 1 ? "," : " ") + "Date ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + UserID + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Date))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Date + "' ";			}
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
                + " update [HLAQSC].dbo.T5_MessageBoard "
                + " set "
				+ " T5_MessageBoard.ID = '" + ID + "' "				+ ",T5_MessageBoard.PositionCode = '" + PositionCode + "' "				+ ",T5_MessageBoard.UserID = '" + UserID + "' "				+ ",T5_MessageBoard.Remark = '" + Remark + "' "				+ ",T5_MessageBoard.Date = '" + Date + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_MessageBoard.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_MessageBoard "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID = '" + UserID + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark = '" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Date))			{				count++;				sql += (count > 1 ? "," : " ") + "Date = '" + Date + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_MessageBoard.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_MessageBoard "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_MessageBoard.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}