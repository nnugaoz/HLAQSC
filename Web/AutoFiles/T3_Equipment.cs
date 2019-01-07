using System;

namespace Web.AutoFiles
{
    public class T3_Equipment
    {
        public T3_Equipment()
        {
        }

		public string ID { get; set; }		public string Title { get; set; }		public string Type { get; set; }		public string Remark { get; set; }		public string Del { get; set; }		public string Lock { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Equipment.ID "				+ ",T3_Equipment.Title "				+ ",T3_Equipment.Type "				+ ",T3_Equipment.Remark "				+ ",T3_Equipment.Del "				+ ",T3_Equipment.Lock "                + " from [HLAQSC].dbo.T3_Equipment "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Equipment( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Lock + "' ";			}
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
                + " update [HLAQSC].dbo.T3_Equipment "
                + " set "
				+ " T3_Equipment.ID = '" + ID + "' "				+ ",T3_Equipment.Title = '" + Title + "' "				+ ",T3_Equipment.Type = '" + Type + "' "				+ ",T3_Equipment.Remark = '" + Remark + "' "				+ ",T3_Equipment.Del = '" + Del + "' "				+ ",T3_Equipment.Lock = '" + Lock + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Equipment "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark = '" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock = '" + Lock + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Equipment "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Equipment.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}