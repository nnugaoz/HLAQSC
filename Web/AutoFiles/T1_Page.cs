using System;

namespace Web.AutoFiles
{
    public class T1_Page
    {
        public T1_Page()
        {
        }

		public string Code { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T1_Page.Code "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T1_Page( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Code))
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
				+ " T1_Page.Code = '" + Code + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T1_Page "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T1_Page "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}