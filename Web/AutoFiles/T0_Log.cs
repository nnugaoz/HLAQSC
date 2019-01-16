using System;

namespace Web.AutoFiles
{
    public class T0_Log
    {
        public T0_Log()
        {
        }

		public string ID { get; set; }		public string Txt { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T0_Log.ID "				+ ",T0_Log.Txt "                + " from [HLAQSC].dbo.T0_Log "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T0_Log.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T0_Log( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Txt))			{				count++;				sql += (count > 1 ? "," : " ") + "Txt ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Txt))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Txt + "' ";			}
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
                + " update [HLAQSC].dbo.T0_Log "
                + " set "
				+ " T0_Log.ID = '" + ID + "' "				+ ",T0_Log.Txt = '" + Txt + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T0_Log.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T0_Log "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Txt))			{				count++;				sql += (count > 1 ? "," : " ") + "Txt = '" + Txt + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T0_Log.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T0_Log "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T0_Log.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}