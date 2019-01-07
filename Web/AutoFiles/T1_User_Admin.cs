using System;

namespace Web.AutoFiles
{
    public class T1_User_Admin
    {
        public T1_User_Admin()
        {
        }

		public string ID { get; set; }		public string Name { get; set; }		public string LoginName { get; set; }		public string Password { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T1_User_Admin.ID "				+ ",T1_User_Admin.Name "				+ ",T1_User_Admin.LoginName "				+ ",T1_User_Admin.Password "                + " from [HLAQSC].dbo.T1_User_Admin "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Admin.LoginName = '" + LoginName + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T1_User_Admin( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "Name ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "LoginName ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "Password ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Name + "' ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + LoginName + "' ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Password + "' ";			}
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
                + " update [HLAQSC].dbo.T1_User_Admin "
                + " set "
				+ " T1_User_Admin.ID = '" + ID + "' "				+ ",T1_User_Admin.Name = '" + Name + "' "				+ ",T1_User_Admin.LoginName = '" + LoginName + "' "				+ ",T1_User_Admin.Password = '" + Password + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Admin.LoginName = '" + LoginName + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T1_User_Admin "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(Name))			{				count++;				sql += (count > 1 ? "," : " ") + "Name = '" + Name + "' ";			}			if (!String.IsNullOrEmpty(LoginName))			{				count++;				sql += (count > 1 ? "," : " ") + "LoginName = '" + LoginName + "' ";			}			if (!String.IsNullOrEmpty(Password))			{				count++;				sql += (count > 1 ? "," : " ") + "Password = '" + Password + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Admin.LoginName = '" + LoginName + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T1_User_Admin "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T1_User_Admin.LoginName = '" + LoginName + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}