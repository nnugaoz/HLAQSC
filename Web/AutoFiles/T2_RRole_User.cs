using System;

namespace Web.AutoFiles
{
    public class T2_RRole_User
    {
        public T2_RRole_User()
        {
        }

		public string RRoleID { get; set; }		public string UserID { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T2_RRole_User.RRoleID "				+ ",T2_RRole_User.UserID "                + " from [HLAQSC].dbo.T2_RRole_User "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_User.RRoleID = '" + RRoleID + "' ";					sql += " and T2_RRole_User.UserID = '" + UserID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T2_RRole_User( ";

            int count = 0;
			if (!String.IsNullOrEmpty(RRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleID ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(RRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleID + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + UserID + "' ";			}
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
                + " update [HLAQSC].dbo.T2_RRole_User "
                + " set "
				+ " T2_RRole_User.RRoleID = '" + RRoleID + "' "				+ ",T2_RRole_User.UserID = '" + UserID + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_User.RRoleID = '" + RRoleID + "' ";					sql += " and T2_RRole_User.UserID = '" + UserID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T2_RRole_User "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(RRoleID))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleID = '" + RRoleID + "' ";			}			if (!String.IsNullOrEmpty(UserID))			{				count++;				sql += (count > 1 ? "," : " ") + "UserID = '" + UserID + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_User.RRoleID = '" + RRoleID + "' ";					sql += " and T2_RRole_User.UserID = '" + UserID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T2_RRole_User "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T2_RRole_User.RRoleID = '" + RRoleID + "' ";					sql += " and T2_RRole_User.UserID = '" + UserID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}