using System;

namespace Web.AutoFiles
{
    public class T1_User_Excel
    {
        public T1_User_Excel()
        {
        }

		public string ID { get; set; }		public string Name { get; set; }		public string LoginName { get; set; }		public string Password { get; set; }		public string OrgCode { get; set; }		public string PRoleID { get; set; }		public string RRoleCode { get; set; }		public string DRoleType { get; set; }		public string JobCode { get; set; }		public string UserKey { get; set; }		public string Del { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T1_User_Excel.ID "				+ ",T1_User_Excel.Name "				+ ",T1_User_Excel.LoginName "				+ ",T1_User_Excel.Password "				+ ",T1_User_Excel.OrgCode "				+ ",T1_User_Excel.PRoleID "				+ ",T1_User_Excel.RRoleCode "				+ ",T1_User_Excel.DRoleType "				+ ",T1_User_Excel.JobCode "				+ ",T1_User_Excel.UserKey "				+ ",T1_User_Excel.Del "                + " from [HLAQSC].dbo.T1_User_Excel "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Excel.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T1_User_Excel( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "Name ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "LoginName ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "Password ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "OrgCode ";			}			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "PRoleID ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode ";			}			if (!String.IsNullOrEmpty(DRoleType))			{				count++;				sql += (count > 1 ? "," : " ") + "DRoleType ";			}			if (!String.IsNullOrEmpty(JobCode))			{				count++;				sql += (count > 1 ? "," : " ") + "JobCode ";			}			if (!String.IsNullOrEmpty(UserKey))			{				count++;				sql += (count > 1 ? "," : " ") + "UserKey ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Name + "' ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + LoginName + "' ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Password + "' ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + OrgCode + "' ";			}			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PRoleID + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(DRoleType))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + DRoleType + "' ";			}			if (!String.IsNullOrEmpty(JobCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + JobCode + "' ";			}			if (!String.IsNullOrEmpty(UserKey))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + UserKey + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}
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
                + " update [HLAQSC].dbo.T1_User_Excel "
                + " set "
				+ " T1_User_Excel.ID = '" + ID + "' "				+ ",T1_User_Excel.Name = '" + Name + "' "				+ ",T1_User_Excel.LoginName = '" + LoginName + "' "				+ ",T1_User_Excel.Password = '" + Password + "' "				+ ",T1_User_Excel.OrgCode = '" + OrgCode + "' "				+ ",T1_User_Excel.PRoleID = '" + PRoleID + "' "				+ ",T1_User_Excel.RRoleCode = '" + RRoleCode + "' "				+ ",T1_User_Excel.DRoleType = '" + DRoleType + "' "				+ ",T1_User_Excel.JobCode = '" + JobCode + "' "				+ ",T1_User_Excel.UserKey = '" + UserKey + "' "				+ ",T1_User_Excel.Del = '" + Del + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Excel.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T1_User_Excel "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "Name = '" + Name + "' ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "LoginName = '" + LoginName + "' ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "Password = '" + Password + "' ";			}			if (!String.IsNullOrEmpty(OrgCode))			{				count++;				sql += (count > 1 ? "," : " ") + "OrgCode = '" + OrgCode + "' ";			}			if (!String.IsNullOrEmpty(PRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "PRoleID = '" + PRoleID + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(DRoleType))			{				count++;				sql += (count > 1 ? "," : " ") + "DRoleType = '" + DRoleType + "' ";			}			if (!String.IsNullOrEmpty(JobCode))			{				count++;				sql += (count > 1 ? "," : " ") + "JobCode = '" + JobCode + "' ";			}			if (!String.IsNullOrEmpty(UserKey))			{				count++;				sql += (count > 1 ? "," : " ") + "UserKey = '" + UserKey + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Excel.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T1_User_Excel "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Excel.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}