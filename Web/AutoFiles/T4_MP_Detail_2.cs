using System;

namespace Web.AutoFiles
{
    public class T4_MP_Detail_2
    {
        public T4_MP_Detail_2()
        {
        }

		public string Month { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T4_MP_Detail_2.Month "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T4_MP_Detail_2( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Month))
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
                + " update [HLAQSC].dbo.T4_MP_Detail_2 "
                + " set "
				+ " T4_MP_Detail_2.Month = '" + Month + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T4_MP_Detail_2 "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T4_MP_Detail_2 "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}