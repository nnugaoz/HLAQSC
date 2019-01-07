using System;

namespace Web.AutoFiles
{
    public class T3_Dynamic_Field
    {
        public T3_Dynamic_Field()
        {
        }

		public string ID { get; set; }		public string Type1 { get; set; }		public string Type2 { get; set; }		public string I { get; set; }		public string FieldKey { get; set; }		public string FieldName { get; set; }		public string FieldUnit { get; set; }		public string FieldType { get; set; }		public string FieldMode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T3_Dynamic_Field.ID "				+ ",T3_Dynamic_Field.Type1 "				+ ",T3_Dynamic_Field.Type2 "				+ ",T3_Dynamic_Field.I "				+ ",T3_Dynamic_Field.FieldKey "				+ ",T3_Dynamic_Field.FieldName "				+ ",T3_Dynamic_Field.FieldUnit "				+ ",T3_Dynamic_Field.FieldType "				+ ",T3_Dynamic_Field.FieldMode "                + " from [HLAQSC].dbo.T3_Dynamic_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T3_Dynamic_Field( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 ";			}			if (!String.IsNullOrEmpty(I))			{				count++;				sql += (count > 1 ? "," : " ") + "I ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey ";			}			if (!String.IsNullOrEmpty(FieldName))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldName ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldType ";			}			if (!String.IsNullOrEmpty(FieldMode))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldMode ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(I))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + I + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldName + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldUnit + "' ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldType + "' ";			}			if (!String.IsNullOrEmpty(FieldMode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldMode + "' ";			}
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
				+ " T3_Dynamic_Field.ID = '" + ID + "' "				+ ",T3_Dynamic_Field.Type1 = '" + Type1 + "' "				+ ",T3_Dynamic_Field.Type2 = '" + Type2 + "' "				+ ",T3_Dynamic_Field.I = '" + I + "' "				+ ",T3_Dynamic_Field.FieldKey = '" + FieldKey + "' "				+ ",T3_Dynamic_Field.FieldName = '" + FieldName + "' "				+ ",T3_Dynamic_Field.FieldUnit = '" + FieldUnit + "' "				+ ",T3_Dynamic_Field.FieldType = '" + FieldType + "' "				+ ",T3_Dynamic_Field.FieldMode = '" + FieldMode + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T3_Dynamic_Field "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 = '" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 = '" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(I))			{				count++;				sql += (count > 1 ? "," : " ") + "I = '" + I + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey = '" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldName))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldName = '" + FieldName + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit = '" + FieldUnit + "' ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldType = '" + FieldType + "' ";			}			if (!String.IsNullOrEmpty(FieldMode))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldMode = '" + FieldMode + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T3_Dynamic_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T3_Dynamic_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}