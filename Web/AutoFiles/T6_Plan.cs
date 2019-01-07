using System;

namespace Web.AutoFiles
{
    public class T6_Plan
    {
        public T6_Plan()
        {
        }

		public string ID { get; set; }		public string YM { get; set; }		public string FileName { get; set; }		public string FileNameS { get; set; }		public string UploadTime { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T6_Plan.ID "				+ ",T6_Plan.YM "				+ ",T6_Plan.FileName "				+ ",T6_Plan.FileNameS "				+ ",T6_Plan.UploadTime "                + " from [HLAQSC].dbo.T6_Plan "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T6_Plan( ";

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
                + " update [HLAQSC].dbo.T6_Plan "
                + " set "
				+ " T6_Plan.ID = '" + ID + "' "				+ ",T6_Plan.YM = '" + YM + "' "				+ ",T6_Plan.FileName = '" + FileName + "' "				+ ",T6_Plan.FileNameS = '" + FileNameS + "' "				+ ",T6_Plan.UploadTime = '" + UploadTime + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T6_Plan "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(YM))			{				count++;				sql += (count > 1 ? "," : " ") + "YM = '" + YM + "' ";			}			if (!String.IsNullOrEmpty(FileName))			{				count++;				sql += (count > 1 ? "," : " ") + "FileName = '" + FileName + "' ";			}			if (!String.IsNullOrEmpty(FileNameS))			{				count++;				sql += (count > 1 ? "," : " ") + "FileNameS = '" + FileNameS + "' ";			}			if (!String.IsNullOrEmpty(UploadTime))			{				count++;				sql += (count > 1 ? "," : " ") + "UploadTime = '" + UploadTime + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T6_Plan "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T6_Plan.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}