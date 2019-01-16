using System;

namespace Web.AutoFiles
{
    public class T8_Report1_Config
    {
        public T8_Report1_Config()
        {
        }

		public string FCode { get; set; }		public string FName { get; set; }		public string R1 { get; set; }		public string R2 { get; set; }		public string R3 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_Report1_Config.FCode "				+ ",T8_Report1_Config.FName "				+ ",T8_Report1_Config.R1 "				+ ",T8_Report1_Config.R2 "				+ ",T8_Report1_Config.R3 "                + " from [HLAQSC].dbo.T8_Report1_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1_Config.FCode = '" + FCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_Report1_Config( ";

            int count = 0;
			if (!String.IsNullOrEmpty(FCode))			{				count++;				sql += (count > 1 ? "," : " ") + "FCode ";			}			if (!String.IsNullOrEmpty(FName))			{				count++;				sql += (count > 1 ? "," : " ") + "FName ";			}			if (!String.IsNullOrEmpty(R1))			{				count++;				sql += (count > 1 ? "," : " ") + "R1 ";			}			if (!String.IsNullOrEmpty(R2))			{				count++;				sql += (count > 1 ? "," : " ") + "R2 ";			}			if (!String.IsNullOrEmpty(R3))			{				count++;				sql += (count > 1 ? "," : " ") + "R3 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(FCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FCode + "' ";			}			if (!String.IsNullOrEmpty(FName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FName + "' ";			}			if (!String.IsNullOrEmpty(R1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + R1 + "' ";			}			if (!String.IsNullOrEmpty(R2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + R2 + "' ";			}			if (!String.IsNullOrEmpty(R3))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + R3 + "' ";			}
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
                + " update [HLAQSC].dbo.T8_Report1_Config "
                + " set "
				+ " T8_Report1_Config.FCode = '" + FCode + "' "				+ ",T8_Report1_Config.FName = '" + FName + "' "				+ ",T8_Report1_Config.R1 = '" + R1 + "' "				+ ",T8_Report1_Config.R2 = '" + R2 + "' "				+ ",T8_Report1_Config.R3 = '" + R3 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1_Config.FCode = '" + FCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_Report1_Config "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(FCode))			{				count++;				sql += (count > 1 ? "," : " ") + "FCode = '" + FCode + "' ";			}			if (!String.IsNullOrEmpty(FName))			{				count++;				sql += (count > 1 ? "," : " ") + "FName = '" + FName + "' ";			}			if (!String.IsNullOrEmpty(R1))			{				count++;				sql += (count > 1 ? "," : " ") + "R1 = '" + R1 + "' ";			}			if (!String.IsNullOrEmpty(R2))			{				count++;				sql += (count > 1 ? "," : " ") + "R2 = '" + R2 + "' ";			}			if (!String.IsNullOrEmpty(R3))			{				count++;				sql += (count > 1 ? "," : " ") + "R3 = '" + R3 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1_Config.FCode = '" + FCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_Report1_Config "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_Report1_Config.FCode = '" + FCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}