using System;

namespace Web.AutoFiles
{
    public class T2_Position_Org
    {
        public T2_Position_Org()
        {
        }

		public string PositionCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_Position_Org.PositionCode "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_Position_Org( ";

            int count = 0;
			if (!String.IsNullOrEmpty(PositionCode))
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(PositionCode))
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
                + " update [HLAQSC].dbo.T2_Position_Org "
                + " set "
				+ " T2_Position_Org.PositionCode = '" + PositionCode + "' "
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_Position_Org "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(PositionCode))
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_Position_Org "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))
            return true;
        }
    }
}