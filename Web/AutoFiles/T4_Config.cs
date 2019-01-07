using System;

namespace Web.AutoFiles
{
    public class T4_Config
    {
        public T4_Config()
        {
        }

		public string Code { get; set; }		public string Remark1 { get; set; }		public string Unit1 { get; set; }		public string Type1 { get; set; }		public string Type2 { get; set; }		public string DFKey { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T4_Config.Code "				+ ",T4_Config.Remark1 "				+ ",T4_Config.Unit1 "				+ ",T4_Config.Type1 "				+ ",T4_Config.Type2 "				+ ",T4_Config.DFKey "                + " from [HLAQSC].dbo.T4_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_Config.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T4_Config( ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code ";			}			if (!String.IsNullOrEmpty(Remark1))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark1 ";			}			if (!String.IsNullOrEmpty(Unit1))			{				count++;				sql += (count > 1 ? "," : " ") + "Unit1 ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 ";			}			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Code + "' ";			}			if (!String.IsNullOrEmpty(Remark1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Remark1 + "' ";			}			if (!String.IsNullOrEmpty(Unit1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Unit1 + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DFKey + "' ";			}
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
                + " update [HLAQSC].dbo.T4_Config "
                + " set "
				+ " T4_Config.Code = '" + Code + "' "				+ ",T4_Config.Remark1 = '" + Remark1 + "' "				+ ",T4_Config.Unit1 = '" + Unit1 + "' "				+ ",T4_Config.Type1 = '" + Type1 + "' "				+ ",T4_Config.Type2 = '" + Type2 + "' "				+ ",T4_Config.DFKey = '" + DFKey + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_Config.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T4_Config "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(Code))			{				count++;				sql += (count > 1 ? "," : " ") + "Code = '" + Code + "' ";			}			if (!String.IsNullOrEmpty(Remark1))			{				count++;				sql += (count > 1 ? "," : " ") + "Remark1 = '" + Remark1 + "' ";			}			if (!String.IsNullOrEmpty(Unit1))			{				count++;				sql += (count > 1 ? "," : " ") + "Unit1 = '" + Unit1 + "' ";			}			if (!String.IsNullOrEmpty(Type1))			{				count++;				sql += (count > 1 ? "," : " ") + "Type1 = '" + Type1 + "' ";			}			if (!String.IsNullOrEmpty(Type2))			{				count++;				sql += (count > 1 ? "," : " ") + "Type2 = '" + Type2 + "' ";			}			if (!String.IsNullOrEmpty(DFKey))			{				count++;				sql += (count > 1 ? "," : " ") + "DFKey = '" + DFKey + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_Config.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T4_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T4_Config.Code = '" + Code + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}