using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_FieldType
    {
        public T3_Dynamic_FieldType()
        {
        }

		public string DFKey { get; set; }		public string Type { get; set; }		public string Title { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_FieldType.DFKey "				+ ",T3_Dynamic_FieldType.Type "				+ ",T3_Dynamic_FieldType.Title "                + " from [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldType.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldType.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_FieldType( ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DFKey + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Title + "' ";			}
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
				+ " T3_Dynamic_FieldType.DFKey = '" + DFKey + "' "				+ ",T3_Dynamic_FieldType.Type = '" + Type + "' "				+ ",T3_Dynamic_FieldType.Title = '" + Title + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldType.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldType.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey = '" + DFKey + "' ";			}			if (!String.IsNullOrEmpty(Type))			{				count++;				sql += (count > 1 ? "," : " ") + "Type = '" + Type + "' ";			}			if (!String.IsNullOrEmpty(Title))			{				count++;				sql += (count > 1 ? "," : " ") + "Title = '" + Title + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldType.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldType.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Dynamic_FieldType "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_FieldType.DFKey = '" + DFKey + "' ";					sql += " and T3_Dynamic_FieldType.Type = '" + Type + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}