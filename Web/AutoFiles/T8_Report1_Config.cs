using System;

namespace Web.AutoFiles
{
    public class T8_Report1_Config
    {
        public T8_Report1_Config()
        {
        }

		public string FCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_Report1_Config.FCode "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_Report1_Config( ";

            int count = 0;
			if (!String.IsNullOrEmpty(FCode))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(FCode))
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
                + " update [HLAQSC].dbo.T8_Report1_Config "
                + " set "
				+ " T8_Report1_Config.FCode = '" + FCode + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_Report1_Config "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(FCode))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_Report1_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}