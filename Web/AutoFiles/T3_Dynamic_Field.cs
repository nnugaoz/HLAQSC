using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_Field
    {
        public T3_Dynamic_Field()
        {
        }

		public string ID { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_Field.ID "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_Field( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))
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
                + " update [HLAQSC].dbo.T3_Dynamic_Field "
                + " set "
				+ " T3_Dynamic_Field.ID = '" + ID + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_Field "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Dynamic_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}