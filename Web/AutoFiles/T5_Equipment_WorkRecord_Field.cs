using System;

namespace Web.AutoFiles
{
    public class T5_Equipment_WorkRecord_Field
    {
        public T5_Equipment_WorkRecord_Field()
        {
        }

		public string ID { get; set; }		public string WorkRecordID { get; set; }		public string FieldKey { get; set; }		public string FieldValue { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_Equipment_WorkRecord_Field.ID "				+ ",T5_Equipment_WorkRecord_Field.WorkRecordID "				+ ",T5_Equipment_WorkRecord_Field.FieldKey "				+ ",T5_Equipment_WorkRecord_Field.FieldValue "                + " from [HLAQSC].dbo.T5_Equipment_WorkRecord_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_Equipment_WorkRecord_Field( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FieldValue + "' ";			}
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
                + " update [HLAQSC].dbo.T5_Equipment_WorkRecord_Field "
                + " set "
				+ " T5_Equipment_WorkRecord_Field.ID = '" + ID + "' "				+ ",T5_Equipment_WorkRecord_Field.WorkRecordID = '" + WorkRecordID + "' "				+ ",T5_Equipment_WorkRecord_Field.FieldKey = '" + FieldKey + "' "				+ ",T5_Equipment_WorkRecord_Field.FieldValue = '" + FieldValue + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_Equipment_WorkRecord_Field "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID = '" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(FieldKey))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldKey = '" + FieldKey + "' ";			}			if (!String.IsNullOrEmpty(FieldValue))			{				count++;				sql += (count > 1 ? "," : " ") + "FieldValue = '" + FieldValue + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_Equipment_WorkRecord_Field "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord_Field.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}