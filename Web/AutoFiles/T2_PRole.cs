using System;

namespace Web.AutoFiles
{
    public class T2_PRole
    {
        public T2_PRole()
        {
        }

		public string ID { get; set; }		public string Title { get; set; }		public string Remark { get; set; }		public string Del { get; set; }		public string Lock { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_PRole.ID "				+ ",T2_PRole.Title "				+ ",T2_PRole.Remark "				+ ",T2_PRole.Del "				+ ",T2_PRole.Lock "                + " from [HLAQSC].dbo.T2_PRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_PRole( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Lock + "' ";			}
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
                + " update [HLAQSC].dbo.T2_PRole "
                + " set "
				+ " T2_PRole.ID = '" + ID + "' "				+ ",T2_PRole.Title = '" + Title + "' "				+ ",T2_PRole.Remark = '" + Remark + "' "				+ ",T2_PRole.Del = '" + Del + "' "				+ ",T2_PRole.Lock = '" + Lock + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_PRole "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark = '" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock = '" + Lock + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_PRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}