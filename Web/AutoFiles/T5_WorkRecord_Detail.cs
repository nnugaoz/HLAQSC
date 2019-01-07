using System;

namespace Web.AutoFiles
{
    public class T5_WorkRecord_Detail
    {
        public T5_WorkRecord_Detail()
        {
        }

		public string ID { get; set; }		public string WorkRecordID { get; set; }		public string EquipmentID { get; set; }		public string PositionCode { get; set; }		public string WorkHour { get; set; }		public string WhereAbout { get; set; }		public string DF1 { get; set; }		public string DF2 { get; set; }		public string DF3 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_WorkRecord_Detail.ID "				+ ",T5_WorkRecord_Detail.WorkRecordID "				+ ",T5_WorkRecord_Detail.EquipmentID "				+ ",T5_WorkRecord_Detail.PositionCode "				+ ",T5_WorkRecord_Detail.WorkHour "				+ ",T5_WorkRecord_Detail.WhereAbout "				+ ",T5_WorkRecord_Detail.DF1 "				+ ",T5_WorkRecord_Detail.DF2 "				+ ",T5_WorkRecord_Detail.DF3 "                + " from [HLAQSC].dbo.T5_WorkRecord_Detail "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_WorkRecord_Detail( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkHour ";			}			if (!String.IsNullOrEmpty(WhereAbout))			{				count++;				sql += (count > 1 ? "," : " ") + "WhereAbout ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkHour + "' ";			}			if (!String.IsNullOrEmpty(WhereAbout))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WhereAbout + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF3 + "' ";			}
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
                + " update [HLAQSC].dbo.T5_WorkRecord_Detail "
                + " set "
				+ " T5_WorkRecord_Detail.ID = '" + ID + "' "				+ ",T5_WorkRecord_Detail.WorkRecordID = '" + WorkRecordID + "' "				+ ",T5_WorkRecord_Detail.EquipmentID = '" + EquipmentID + "' "				+ ",T5_WorkRecord_Detail.PositionCode = '" + PositionCode + "' "				+ ",T5_WorkRecord_Detail.WorkHour = '" + WorkHour + "' "				+ ",T5_WorkRecord_Detail.WhereAbout = '" + WhereAbout + "' "				+ ",T5_WorkRecord_Detail.DF1 = '" + DF1 + "' "				+ ",T5_WorkRecord_Detail.DF2 = '" + DF2 + "' "				+ ",T5_WorkRecord_Detail.DF3 = '" + DF3 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_WorkRecord_Detail "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID = '" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkHour = '" + WorkHour + "' ";			}			if (!String.IsNullOrEmpty(WhereAbout))			{				count++;				sql += (count > 1 ? "," : " ") + "WhereAbout = '" + WhereAbout + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 = '" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 = '" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 = '" + DF3 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_WorkRecord_Detail "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord_Detail.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}