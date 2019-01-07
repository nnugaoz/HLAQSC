using System;

namespace Web.AutoFiles
{
    public class T2_RRole
    {
        public T2_RRole()
        {
        }

		public string ID { get; set; }		public string Code { get; set; }		public string Type { get; set; }		public string Title { get; set; }		public string Remark { get; set; }		public string Del { get; set; }		public string Lock { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_RRole.ID "				+ ",T2_RRole.Code "				+ ",T2_RRole.Type "				+ ",T2_RRole.Title "				+ ",T2_RRole.Remark "				+ ",T2_RRole.Del "				+ ",T2_RRole.Lock "                + " from [HLAQSC].dbo.T2_RRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_RRole( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Code + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Lock + "' ";			}
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
                + " update [HLAQSC].dbo.T2_RRole "
                + " set "
				+ " T2_RRole.ID = '" + ID + "' "				+ ",T2_RRole.Code = '" + Code + "' "				+ ",T2_RRole.Type = '" + Type + "' "				+ ",T2_RRole.Title = '" + Title + "' "				+ ",T2_RRole.Remark = '" + Remark + "' "				+ ",T2_RRole.Del = '" + Del + "' "				+ ",T2_RRole.Lock = '" + Lock + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_RRole "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code = '" + Code + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark = '" + Remark + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock = '" + Lock + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_RRole "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}