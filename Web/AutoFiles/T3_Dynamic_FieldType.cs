using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_FieldType
    {
        public T3_Dynamic_FieldType()
        {
        }

		public string DFKey { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_FieldType.DFKey "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_FieldType( ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(DFKey))
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
                + " update [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " set "
				+ " T3_Dynamic_FieldType.DFKey = '" + DFKey + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}