using System;

namespace Web.AutoFiles
{
    public class T2_RRole_OperLog
    {
        public T2_RRole_OperLog()
        {
        }

		public string ID { get; set; }		public string WorkRecordID { get; set; }		public string RRoleCode { get; set; }		public string UserID { get; set; }		public string Result { get; set; }		public string Remark { get; set; }		public string RDate { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_RRole_OperLog.ID "				+ ",T2_RRole_OperLog.WorkRecordID "				+ ",T2_RRole_OperLog.RRoleCode "				+ ",T2_RRole_OperLog.UserID "				+ ",T2_RRole_OperLog.Result "				+ ",T2_RRole_OperLog.Remark "				+ ",T2_RRole_OperLog.RDate "                + " from [HLAQSC].dbo.T2_RRole_OperLog "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_OperLog.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_RRole_OperLog( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "Result ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark ";			}			if (!String.IsNullOrEmpty(RDate))			{				count++;				sql += (count > 1 ? "," : " ") + "RDate ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + UserID + "' ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Result + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark + "' ";			}			if (!String.IsNullOrEmpty(RDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RDate + "' ";			}
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
                + " update [HLAQSC].dbo.T2_RRole_OperLog "
                + " set "
				+ " T2_RRole_OperLog.ID = '" + ID + "' "				+ ",T2_RRole_OperLog.WorkRecordID = '" + WorkRecordID + "' "				+ ",T2_RRole_OperLog.RRoleCode = '" + RRoleCode + "' "				+ ",T2_RRole_OperLog.UserID = '" + UserID + "' "				+ ",T2_RRole_OperLog.Result = '" + Result + "' "				+ ",T2_RRole_OperLog.Remark = '" + Remark + "' "				+ ",T2_RRole_OperLog.RDate = '" + RDate + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_OperLog.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_RRole_OperLog "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkRecordID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkRecordID = '" + WorkRecordID + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID = '" + UserID + "' ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "Result = '" + Result + "' ";			}			if (!String.IsNullOrEmpty(Remark))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark = '" + Remark + "' ";			}			if (!String.IsNullOrEmpty(RDate))			{				count++;				sql += (count > 1 ? "," : " ") + "RDate = '" + RDate + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_OperLog.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_RRole_OperLog "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_OperLog.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}