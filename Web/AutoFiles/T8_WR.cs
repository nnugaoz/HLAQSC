using System;

namespace Web.AutoFiles
{
    public class T8_WR
    {
        public T8_WR()
        {
        }

		public string ID { get; set; }		public string WorkDate { get; set; }		public string WorkClassCode { get; set; }		public string WorkClassName { get; set; }		public string WorkManID { get; set; }		public string WorkManName { get; set; }		public string RRoleCode { get; set; }		public string RRoleCode_Cur { get; set; }		public string Status { get; set; }		public string Del { get; set; }		public string DF1 { get; set; }		public string DF2 { get; set; }		public string DF3 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_WR.ID "				+ ",T8_WR.WorkDate "				+ ",T8_WR.WorkClassCode "				+ ",T8_WR.WorkClassName "				+ ",T8_WR.WorkManID "				+ ",T8_WR.WorkManName "				+ ",T8_WR.RRoleCode "				+ ",T8_WR.RRoleCode_Cur "				+ ",T8_WR.Status "				+ ",T8_WR.Del "				+ ",T8_WR.DF1 "				+ ",T8_WR.DF2 "				+ ",T8_WR.DF3 "                + " from [HLAQSC].dbo.T8_WR "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManID ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManName ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode_Cur ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkManID + "' ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode_Cur + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DF3 + "' ";			}
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
                + " update [HLAQSC].dbo.T8_WR "
                + " set "
				+ " T8_WR.ID = '" + ID + "' "				+ ",T8_WR.WorkDate = '" + WorkDate + "' "				+ ",T8_WR.WorkClassCode = '" + WorkClassCode + "' "				+ ",T8_WR.WorkClassName = '" + WorkClassName + "' "				+ ",T8_WR.WorkManID = '" + WorkManID + "' "				+ ",T8_WR.WorkManName = '" + WorkManName + "' "				+ ",T8_WR.RRoleCode = '" + RRoleCode + "' "				+ ",T8_WR.RRoleCode_Cur = '" + RRoleCode_Cur + "' "				+ ",T8_WR.Status = '" + Status + "' "				+ ",T8_WR.Del = '" + Del + "' "				+ ",T8_WR.DF1 = '" + DF1 + "' "				+ ",T8_WR.DF2 = '" + DF2 + "' "				+ ",T8_WR.DF3 = '" + DF3 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate = '" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode = '" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName = '" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(WorkManID))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManID = '" + WorkManID + "' ";			}			if (!String.IsNullOrEmpty(WorkManName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkManName = '" + WorkManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode_Cur))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode_Cur = '" + RRoleCode_Cur + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status = '" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}			if (!String.IsNullOrEmpty(DF1))			{				count++;				sql += (count > 1 ? "," : " ") + "DF1 = '" + DF1 + "' ";			}			if (!String.IsNullOrEmpty(DF2))			{				count++;				sql += (count > 1 ? "," : " ") + "DF2 = '" + DF2 + "' ";			}			if (!String.IsNullOrEmpty(DF3))			{				count++;				sql += (count > 1 ? "," : " ") + "DF3 = '" + DF3 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_WR "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_WR.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}