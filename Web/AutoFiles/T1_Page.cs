using System;

namespace Web.AutoFiles
{
    public class T1_Page
    {
        public T1_Page()
        {
        }

		public string Code { get; set; }		public string Type { get; set; }		public string OrderBy { get; set; }		public string Title { get; set; }		public string Url { get; set; }		public string Del { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T1_Page.Code "				+ ",T1_Page.Type "				+ ",T1_Page.OrderBy "				+ ",T1_Page.Title "				+ ",T1_Page.Url "				+ ",T1_Page.Del "                + " from [HLAQSC].dbo.T1_Page "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_Page.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T1_Page( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Url))			{				count++;				sql += (count > 1 ? "," : " ") + "Url ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Code + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Url))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Url + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}
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
                + " update [HLAQSC].dbo.T1_Page "
                + " set "
				+ " T1_Page.Code = '" + Code + "' "				+ ",T1_Page.Type = '" + Type + "' "				+ ",T1_Page.OrderBy = '" + OrderBy + "' "				+ ",T1_Page.Title = '" + Title + "' "				+ ",T1_Page.Url = '" + Url + "' "				+ ",T1_Page.Del = '" + Del + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_Page.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T1_Page "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code = '" + Code + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(OrderBy))			{				count++;				sql += (count > 1 ? "," : " ") + "OrderBy = '" + OrderBy + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Url))			{				count++;				sql += (count > 1 ? "," : " ") + "Url = '" + Url + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_Page.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T1_Page "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_Page.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}