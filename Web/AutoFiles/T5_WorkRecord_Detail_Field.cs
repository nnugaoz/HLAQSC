using System;

namespace Web.AutoFiles
{
    public class T5_WorkRecord_Detail_Field
    {
        public T5_WorkRecord_Detail_Field()
        {
        }

		public string ID { get; set; }		public string WorkRecordDetailID { get; set; }		public string FieldKey { get; set; }		public string FieldType { get; set; }		public string FieldValue { get; set; }		public string FieldUnit { get; set; }		public string FieldValue0 { get; set; }		public string FieldUnit0 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_WorkRecord_Detail_Field.ID "				+ ",T5_WorkRecord_Detail_Field.WorkRecordDetailID "				+ ",T5_WorkRecord_Detail_Field.FieldKey "				+ ",T5_WorkRecord_Detail_Field.FieldType "				+ ",T5_WorkRecord_Detail_Field.FieldValue "				+ ",T5_WorkRecord_Detail_Field.FieldUnit "				+ ",T5_WorkRecord_Detail_Field.FieldValue0 "				+ ",T5_WorkRecord_Detail_Field.FieldUnit0 "                + " from [HLAQSC].dbo.T5_WorkRecord_Detail_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_WorkRecord_Detail_Field( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkRecordDetailID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordDetailID ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldType ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit ";			}			if (!String.IsNullOrEmpty(FieldValue0))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue0 ";			}			if (!String.IsNullOrEmpty(FieldUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit0 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordDetailID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkRecordDetailID + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldType + "' ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldValue + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldUnit + "' ";			}			if (!String.IsNullOrEmpty(FieldValue0))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldValue0 + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldUnit0 + "' ";			}
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
                + " update [HLAQSC].dbo.T5_WorkRecord_Detail_Field "
                + " set "
				+ " T5_WorkRecord_Detail_Field.ID = '" + ID + "' "				+ ",T5_WorkRecord_Detail_Field.WorkRecordDetailID = '" + WorkRecordDetailID + "' "				+ ",T5_WorkRecord_Detail_Field.FieldKey = '" + FieldKey + "' "				+ ",T5_WorkRecord_Detail_Field.FieldType = '" + FieldType + "' "				+ ",T5_WorkRecord_Detail_Field.FieldValue = '" + FieldValue + "' "				+ ",T5_WorkRecord_Detail_Field.FieldUnit = '" + FieldUnit + "' "				+ ",T5_WorkRecord_Detail_Field.FieldValue0 = '" + FieldValue0 + "' "				+ ",T5_WorkRecord_Detail_Field.FieldUnit0 = '" + FieldUnit0 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_WorkRecord_Detail_Field "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordDetailID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordDetailID = '" + WorkRecordDetailID + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey = '" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldType))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldType = '" + FieldType + "' ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue = '" + FieldValue + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit = '" + FieldUnit + "' ";			}			if (!String.IsNullOrEmpty(FieldValue0))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue0 = '" + FieldValue0 + "' ";			}			if (!String.IsNullOrEmpty(FieldUnit0))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldUnit0 = '" + FieldUnit0 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_WorkRecord_Detail_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}