using System;

namespace Web.AutoFiles
{
    public class T2_DRole
    {
        public T2_DRole()
        {
        }

		public string ID { get; set; }		public string Title { get; set; }		public string Type { get; set; }		public string Del { get; set; }		public string Lock { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_DRole.ID "				+ ",T2_DRole.Title "				+ ",T2_DRole.Type "				+ ",T2_DRole.Del "				+ ",T2_DRole.Lock "                + " from [HLAQSC].dbo.T2_DRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_DRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_DRole( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Lock + "' ";			}
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
                + " update [HLAQSC].dbo.T2_DRole "
                + " set "
				+ " T2_DRole.ID = '" + ID + "' "				+ ",T2_DRole.Title = '" + Title + "' "				+ ",T2_DRole.Type = '" + Type + "' "				+ ",T2_DRole.Del = '" + Del + "' "				+ ",T2_DRole.Lock = '" + Lock + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_DRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_DRole "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock = '" + Lock + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_DRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_DRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_DRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}