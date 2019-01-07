using System;

namespace Web.AutoFiles
{
    public class T2_Position_Org
    {
        public T2_Position_Org()
        {
        }

		public string PositionCode { get; set; }		public string OrgCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_Position_Org.PositionCode "				+ ",T2_Position_Org.OrgCode "                + " from [HLAQSC].dbo.T2_Position_Org "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_Position_Org.PositionCode = '" + PositionCode + "' ";					sql += " and T2_Position_Org.OrgCode = '" + OrgCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_Position_Org( ";

            int count = 0;
			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "OrgCode ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + OrgCode + "' ";			}
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
				+ " T2_Position_Org.PositionCode = '" + PositionCode + "' "				+ ",T2_Position_Org.OrgCode = '" + OrgCode + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_Position_Org.PositionCode = '" + PositionCode + "' ";					sql += " and T2_Position_Org.OrgCode = '" + OrgCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_Position_Org "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "OrgCode = '" + OrgCode + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_Position_Org.PositionCode = '" + PositionCode + "' ";					sql += " and T2_Position_Org.OrgCode = '" + OrgCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_Position_Org "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_Position_Org.PositionCode = '" + PositionCode + "' ";					sql += " and T2_Position_Org.OrgCode = '" + OrgCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}