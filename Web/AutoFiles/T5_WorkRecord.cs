using System;

namespace Web.AutoFiles
{
    public class T5_WorkRecord
    {
        public T5_WorkRecord()
        {
        }

		public string ID { get; set; }		public string RecordType { get; set; }		public string WorkDate { get; set; }		public string WorkClassCode { get; set; }		public string WorkClassName { get; set; }		public string WorkHour { get; set; }		public string WorkManID { get; set; }		public string WorkManName { get; set; }		public string RRoleCode { get; set; }		public string RRoleCode_Cur { get; set; }		public string Status { get; set; }		public string Del { get; set; }		public string DF1 { get; set; }		public string DF2 { get; set; }		public string DF3 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_WorkRecord.ID "				+ ",T5_WorkRecord.RecordType "				+ ",T5_WorkRecord.WorkDate "				+ ",T5_WorkRecord.WorkClassCode "				+ ",T5_WorkRecord.WorkClassName "				+ ",T5_WorkRecord.WorkHour "				+ ",T5_WorkRecord.WorkManID "				+ ",T5_WorkRecord.WorkManName "				+ ",T5_WorkRecord.RRoleCode "				+ ",T5_WorkRecord.RRoleCode_Cur "				+ ",T5_WorkRecord.Status "				+ ",T5_WorkRecord.Del "				+ ",T5_WorkRecord.DF1 "				+ ",T5_WorkRecord.DF2 "				+ ",T5_WorkRecord.DF3 "                + " from [HLAQSC].dbo.T5_WorkRecord "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_WorkRecord( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(RecordType))			{				count++;				sql += (count > 1 ? "," : " ") + "RecordType ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkHour ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManID ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManName ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode_Cur ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(RecordType))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RecordType + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkHour + "' ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkManID + "' ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode_Cur + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF3 + "' ";			}
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
                + " update [HLAQSC].dbo.T5_WorkRecord "
                + " set "
				+ " T5_WorkRecord.ID = '" + ID + "' "				+ ",T5_WorkRecord.RecordType = '" + RecordType + "' "				+ ",T5_WorkRecord.WorkDate = '" + WorkDate + "' "				+ ",T5_WorkRecord.WorkClassCode = '" + WorkClassCode + "' "				+ ",T5_WorkRecord.WorkClassName = '" + WorkClassName + "' "				+ ",T5_WorkRecord.WorkHour = '" + WorkHour + "' "				+ ",T5_WorkRecord.WorkManID = '" + WorkManID + "' "				+ ",T5_WorkRecord.WorkManName = '" + WorkManName + "' "				+ ",T5_WorkRecord.RRoleCode = '" + RRoleCode + "' "				+ ",T5_WorkRecord.RRoleCode_Cur = '" + RRoleCode_Cur + "' "				+ ",T5_WorkRecord.Status = '" + Status + "' "				+ ",T5_WorkRecord.Del = '" + Del + "' "				+ ",T5_WorkRecord.DF1 = '" + DF1 + "' "				+ ",T5_WorkRecord.DF2 = '" + DF2 + "' "				+ ",T5_WorkRecord.DF3 = '" + DF3 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_WorkRecord "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(RecordType))			{				count++;				sql += (count > 1 ? "," : " ") + "RecordType = '" + RecordType + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate = '" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode = '" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName = '" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(WorkHour))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkHour = '" + WorkHour + "' ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManID = '" + WorkManID + "' ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManName = '" + WorkManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode_Cur = '" + RRoleCode_Cur + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status = '" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 = '" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 = '" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 = '" + DF3 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_WorkRecord "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}