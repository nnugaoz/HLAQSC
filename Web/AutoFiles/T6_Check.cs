using System;

namespace Web.AutoFiles
{
    public class T6_Check
    {
        public T6_Check()
        {
        }

		public string ID { get; set; }		public string YM { get; set; }		public string FileName { get; set; }		public string FileNameS { get; set; }		public string UploadTime { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Check.ID "				+ ",T6_Check.YM "				+ ",T6_Check.FileName "				+ ",T6_Check.FileNameS "				+ ",T6_Check.UploadTime "                + " from [HLAQSC].dbo.T6_Check "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Check( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(YM))			{				count++;				sql += (count > 1 ? "," : " ") + "YM ";			}			if (!String.IsNullOrEmpty(FileName))			{				count++;				sql += (count > 1 ? "," : " ") + "FileName ";			}			if (!String.IsNullOrEmpty(FileNameS))			{				count++;				sql += (count > 1 ? "," : " ") + "FileNameS ";			}			if (!String.IsNullOrEmpty(UploadTime))			{				count++;				sql += (count > 1 ? "," : " ") + "UploadTime ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(YM))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + YM + "' ";			}			if (!String.IsNullOrEmpty(FileName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FileName + "' ";			}			if (!String.IsNullOrEmpty(FileNameS))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + FileNameS + "' ";			}			if (!String.IsNullOrEmpty(UploadTime))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + UploadTime + "' ";			}
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
                + " update [HLAQSC].dbo.T6_Check "
                + " set "
				+ " T6_Check.ID = '" + ID + "' "				+ ",T6_Check.YM = '" + YM + "' "				+ ",T6_Check.FileName = '" + FileName + "' "				+ ",T6_Check.FileNameS = '" + FileNameS + "' "				+ ",T6_Check.UploadTime = '" + UploadTime + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Check "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(YM))			{				count++;				sql += (count > 1 ? "," : " ") + "YM = '" + YM + "' ";			}			if (!String.IsNullOrEmpty(FileName))			{				count++;				sql += (count > 1 ? "," : " ") + "FileName = '" + FileName + "' ";			}			if (!String.IsNullOrEmpty(FileNameS))			{				count++;				sql += (count > 1 ? "," : " ") + "FileNameS = '" + FileNameS + "' ";			}			if (!String.IsNullOrEmpty(UploadTime))			{				count++;				sql += (count > 1 ? "," : " ") + "UploadTime = '" + UploadTime + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Check "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Check.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}