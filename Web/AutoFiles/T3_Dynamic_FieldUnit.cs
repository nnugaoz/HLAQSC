using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_FieldUnit
    {
        public T3_Dynamic_FieldUnit()
        {
        }

		public string DFKey { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_FieldUnit.DFKey "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_FieldUnit( ";

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
                + " update [HLAQSC].dbo.T3_Dynamic_FieldUnit "
                + " set "
				+ " T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_FieldUnit "
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
                + " delete [HLAQSC].dbo.T3_Dynamic_FieldUnit "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}