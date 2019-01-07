using System;

namespace Web.AutoFiles
{
    public class T3_Job
    {
        public T3_Job()
        {
        }

		public string ID { get; set; }		public string Code { get; set; }		public string Title { get; set; }		public string Del { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Job.ID "				+ ",T3_Job.Code "				+ ",T3_Job.Title "				+ ",T3_Job.Del "                + " from [HLAQSC].dbo.T3_Job "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Job.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Job( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Code + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}
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
                + " update [HLAQSC].dbo.T3_Job "
                + " set "
				+ " T3_Job.ID = '" + ID + "' "				+ ",T3_Job.Code = '" + Code + "' "				+ ",T3_Job.Title = '" + Title + "' "				+ ",T3_Job.Del = '" + Del + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Job.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Job "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code = '" + Code + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Job.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Job "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Job.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}