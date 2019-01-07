using System;

namespace Web.AutoFiles
{
    public class T7_Sample
    {
        public T7_Sample()
        {
        }

		public string ID { get; set; }		public string Code { get; set; }		public string STime { get; set; }		public string PID { get; set; }		public string Sampler { get; set; }		public string Memo { get; set; }		public string Result { get; set; }		public string RTime { get; set; }		public string Analyst { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T7_Sample.ID "				+ ",T7_Sample.Code "				+ ",T7_Sample.STime "				+ ",T7_Sample.PID "				+ ",T7_Sample.Sampler "				+ ",T7_Sample.Memo "				+ ",T7_Sample.Result "				+ ",T7_Sample.RTime "				+ ",T7_Sample.Analyst "                + " from [HLAQSC].dbo.T7_Sample "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T7_Sample.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T7_Sample( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code ";			}			if (!String.IsNullOrEmpty(STime))			{				count++;				sql += (count > 1 ? "," : " ") + "STime ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID ";			}			if (!String.IsNullOrEmpty(Sampler))			{				count++;				sql += (count > 1 ? "," : " ") + "Sampler ";			}			if (!String.IsNullOrEmpty(Memo))			{				count++;				sql += (count > 1 ? "," : " ") + "Memo ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "Result ";			}			if (!String.IsNullOrEmpty(RTime))			{				count++;				sql += (count > 1 ? "," : " ") + "RTime ";			}			if (!String.IsNullOrEmpty(Analyst))			{				count++;				sql += (count > 1 ? "," : " ") + "Analyst ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Code + "' ";			}			if (!String.IsNullOrEmpty(STime))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + STime + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PID + "' ";			}			if (!String.IsNullOrEmpty(Sampler))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Sampler + "' ";			}			if (!String.IsNullOrEmpty(Memo))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Memo + "' ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Result + "' ";			}			if (!String.IsNullOrEmpty(RTime))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RTime + "' ";			}			if (!String.IsNullOrEmpty(Analyst))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Analyst + "' ";			}
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
                + " update [HLAQSC].dbo.T7_Sample "
                + " set "
				+ " T7_Sample.ID = '" + ID + "' "				+ ",T7_Sample.Code = '" + Code + "' "				+ ",T7_Sample.STime = '" + STime + "' "				+ ",T7_Sample.PID = '" + PID + "' "				+ ",T7_Sample.Sampler = '" + Sampler + "' "				+ ",T7_Sample.Memo = '" + Memo + "' "				+ ",T7_Sample.Result = '" + Result + "' "				+ ",T7_Sample.RTime = '" + RTime + "' "				+ ",T7_Sample.Analyst = '" + Analyst + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T7_Sample.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T7_Sample "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code = '" + Code + "' ";			}			if (!String.IsNullOrEmpty(STime))			{				count++;				sql += (count > 1 ? "," : " ") + "STime = '" + STime + "' ";			}			if (!String.IsNullOrEmpty(PID))			{				count++;				sql += (count > 1 ? "," : " ") + "PID = '" + PID + "' ";			}			if (!String.IsNullOrEmpty(Sampler))			{				count++;				sql += (count > 1 ? "," : " ") + "Sampler = '" + Sampler + "' ";			}			if (!String.IsNullOrEmpty(Memo))			{				count++;				sql += (count > 1 ? "," : " ") + "Memo = '" + Memo + "' ";			}			if (!String.IsNullOrEmpty(Result))			{				count++;				sql += (count > 1 ? "," : " ") + "Result = '" + Result + "' ";			}			if (!String.IsNullOrEmpty(RTime))			{				count++;				sql += (count > 1 ? "," : " ") + "RTime = '" + RTime + "' ";			}			if (!String.IsNullOrEmpty(Analyst))			{				count++;				sql += (count > 1 ? "," : " ") + "Analyst = '" + Analyst + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T7_Sample.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T7_Sample "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T7_Sample.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}