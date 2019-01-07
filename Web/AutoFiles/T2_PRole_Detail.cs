using System;

namespace Web.AutoFiles
{
    public class T2_PRole_Detail
    {
        public T2_PRole_Detail()
        {
        }

		public string PRoleID { get; set; }		public string PageCode { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_PRole_Detail.PRoleID "				+ ",T2_PRole_Detail.PageCode "                + " from [HLAQSC].dbo.T2_PRole_Detail "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole_Detail.PRoleID = '" + PRoleID + "' ";					sql += " and T2_PRole_Detail.PageCode = '" + PageCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_PRole_Detail( ";

            int count = 0;
			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "PRoleID ";			}			if (!String.IsNullOrEmpty(PageCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PageCode ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PRoleID + "' ";			}			if (!String.IsNullOrEmpty(PageCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PageCode + "' ";			}
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
                + " update [HLAQSC].dbo.T2_PRole_Detail "
                + " set "
				+ " T2_PRole_Detail.PRoleID = '" + PRoleID + "' "				+ ",T2_PRole_Detail.PageCode = '" + PageCode + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole_Detail.PRoleID = '" + PRoleID + "' ";					sql += " and T2_PRole_Detail.PageCode = '" + PageCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_PRole_Detail "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "PRoleID = '" + PRoleID + "' ";			}			if (!String.IsNullOrEmpty(PageCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PageCode = '" + PageCode + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole_Detail.PRoleID = '" + PRoleID + "' ";					sql += " and T2_PRole_Detail.PageCode = '" + PageCode + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_PRole_Detail "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_PRole_Detail.PRoleID = '" + PRoleID + "' ";					sql += " and T2_PRole_Detail.PageCode = '" + PageCode + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}