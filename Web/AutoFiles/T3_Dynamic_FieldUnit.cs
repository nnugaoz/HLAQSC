using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_FieldUnit
    {
        public T3_Dynamic_FieldUnit()
        {
        }

		public string DFKey { get; set; }		public string Type { get; set; }		public string Title { get; set; }		public string Unit_0_Rate { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_FieldUnit.DFKey "				+ ",T3_Dynamic_FieldUnit.Type "				+ ",T3_Dynamic_FieldUnit.Title "				+ ",T3_Dynamic_FieldUnit.Unit_0_Rate "                + " from [HLAQSC].dbo.T3_Dynamic_FieldUnit "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldUnit.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_FieldUnit( ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}			if (!String.IsNullOrEmpty(Unit_0_Rate))			{				count++;				sql += (count > 1 ? "," : " ") + "Unit_0_Rate ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DFKey + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}			if (!String.IsNullOrEmpty(Unit_0_Rate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Unit_0_Rate + "' ";			}
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
				+ " T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' "				+ ",T3_Dynamic_FieldUnit.Type = '" + Type + "' "				+ ",T3_Dynamic_FieldUnit.Title = '" + Title + "' "				+ ",T3_Dynamic_FieldUnit.Unit_0_Rate = '" + Unit_0_Rate + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldUnit.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_FieldUnit "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey = '" + DFKey + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}			if (!String.IsNullOrEmpty(Unit_0_Rate))			{				count++;				sql += (count > 1 ? "," : " ") + "Unit_0_Rate = '" + Unit_0_Rate + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldUnit.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Dynamic_FieldUnit "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldUnit.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldUnit.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}