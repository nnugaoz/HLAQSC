using System;

namespace Web.AutoFiles
{
    public class T4_Config
    {
        public T4_Config()
        {
        }

		public string Code { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T4_Config.Code "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T4_Config( ";

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
                + " update [HLAQSC].dbo.T4_Config "
                + " set "
				+ " T4_Config.Code = '" + Code + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T4_Config "
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
                + " delete [HLAQSC].dbo.T4_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}