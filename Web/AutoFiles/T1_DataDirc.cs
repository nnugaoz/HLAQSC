using System;

namespace Web.AutoFiles
{
    public class T1_DataDirc
    {
        public T1_DataDirc()
        {
        }

		public string ID { get; set; }		public string Type { get; set; }		public string DircKey { get; set; }		public string DircTitle { get; set; }		public string Del { get; set; }		public string Lock { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T1_DataDirc.ID "				+ ",T1_DataDirc.Type "				+ ",T1_DataDirc.DircKey "				+ ",T1_DataDirc.DircTitle "				+ ",T1_DataDirc.Del "				+ ",T1_DataDirc.Lock "                + " from [HLAQSC].dbo.T1_DataDirc "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_DataDirc.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T1_DataDirc( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(DircKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DircKey ";			}			if (!String.IsNullOrEmpty(DircTitle))			{				count++;				sql += (count > 1 ? "," : " ") + "DircTitle ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(DircKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DircKey + "' ";			}			if (!String.IsNullOrEmpty(DircTitle))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DircTitle + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Lock + "' ";			}
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
                + " update [HLAQSC].dbo.T1_DataDirc "
                + " set "
				+ " T1_DataDirc.ID = '" + ID + "' "				+ ",T1_DataDirc.Type = '" + Type + "' "				+ ",T1_DataDirc.DircKey = '" + DircKey + "' "				+ ",T1_DataDirc.DircTitle = '" + DircTitle + "' "				+ ",T1_DataDirc.Del = '" + Del + "' "				+ ",T1_DataDirc.Lock = '" + Lock + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_DataDirc.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T1_DataDirc "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(DircKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DircKey = '" + DircKey + "' ";			}			if (!String.IsNullOrEmpty(DircTitle))			{				count++;				sql += (count > 1 ? "," : " ") + "DircTitle = '" + DircTitle + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(Lock))			{				count++;				sql += (count > 1 ? "," : " ") + "Lock = '" + Lock + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_DataDirc.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T1_DataDirc "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_DataDirc.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}