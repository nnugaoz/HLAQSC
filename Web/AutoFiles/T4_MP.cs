using System;

namespace Web.AutoFiles
{
    public class T4_MP
    {
        public T4_MP()
        {
        }

		public string Month { get; set; }		public string PositionCode { get; set; }		public string WorkDayCount { get; set; }		public string Status { get; set; }		public string StatusChangeDate { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T4_MP.Month "				+ ",T4_MP.PositionCode "				+ ",T4_MP.WorkDayCount "				+ ",T4_MP.Status "				+ ",T4_MP.StatusChangeDate "                + " from [HLAQSC].dbo.T4_MP "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP.Month = '" + Month + "' ";					sql += " and T4_MP.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T4_MP( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(WorkDayCount))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDayCount ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status ";			}			if (!String.IsNullOrEmpty(StatusChangeDate))			{				count++;				sql += (count > 1 ? "," : " ") + "StatusChangeDate ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Month + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(WorkDayCount))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkDayCount + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Status + "' ";			}			if (!String.IsNullOrEmpty(StatusChangeDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + StatusChangeDate + "' ";			}
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
                + " update [HLAQSC].dbo.T4_MP "
                + " set "
				+ " T4_MP.Month = '" + Month + "' "				+ ",T4_MP.PositionCode = '" + PositionCode + "' "				+ ",T4_MP.WorkDayCount = '" + WorkDayCount + "' "				+ ",T4_MP.Status = '" + Status + "' "				+ ",T4_MP.StatusChangeDate = '" + StatusChangeDate + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP.Month = '" + Month + "' ";					sql += " and T4_MP.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T4_MP "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Month))			{				count++;				sql += (count > 1 ? "," : " ") + "Month = '" + Month + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(WorkDayCount))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDayCount = '" + WorkDayCount + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status = '" + Status + "' ";			}			if (!String.IsNullOrEmpty(StatusChangeDate))			{				count++;				sql += (count > 1 ? "," : " ") + "StatusChangeDate = '" + StatusChangeDate + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP.Month = '" + Month + "' ";					sql += " and T4_MP.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T4_MP "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_MP.Month = '" + Month + "' ";					sql += " and T4_MP.PositionCode = '" + PositionCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}