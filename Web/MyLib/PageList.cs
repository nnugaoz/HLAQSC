using System;
using System.Data;

namespace Web.MyLib
{
    public class PageList
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public DataTable dt = new DataTable();
        /// <summary>
        /// 当前页
        /// </summary>
        public string PageList_Index { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public string PageList_Count { get; set; }
        /// <summary>
        /// S_User表ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 数据权限
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 数据权限
        /// </summary>
        public string RRoleCode { get; set; }
        /// <summary>
        /// 数据权限
        /// </summary>
        public string DRoleType { get; set; }
        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }
        public string Para4 { get; set; }
        public string Para5 { get; set; }
        public string Para6 { get; set; }
        public string Para7 { get; set; }
        public string Para8 { get; set; }
        public string Para9 { get; set; }
        /// <summary>
        /// 获取设备数据录入的where条件的sql
        /// 数据权限控制
        /// 1、超级管理员(Admin) 查看所有数据
        /// 2、自己的数据
        /// </summary>
        /// <returns></returns>
        public string GetWhereSql_WR_EDE(string _UserID)
        {
            string sql = ""
                + "'" + this.UserID + "'='" + MyPara.AdminID + "'"
                + " or "
                + "" + _UserID + "='" + this.UserID + "'";

            return sql;
        }

        /// <summary>
        /// 获取where条件的sql
        /// 数据权限控制
        /// 1、超级管理员(Admin) 查看所有数据
        /// 2、自己 查看自己的数据
        /// 3、自己下属的数据
        /// </summary>
        /// <returns></returns>
        public string GetWhereSql_WR_ED(string _UserID, string _RRoleCode)
        {
            string sql = ""
                + "'" + this.UserID + "'='" + MyPara.AdminID + "'"
                + " or "
                + "" + _UserID + "='" + this.UserID + "'"
                + " or "
                + "" + _RRoleCode + " like '" + RRoleCode + "___%'";

            return sql;
        }

        /// <summary>
        /// 获取where条件的sql
        /// 数据权限控制
        /// 1、超级管理员(Admin) 查看所有数据
        /// 2、自己有权限的数据
        /// </summary>
        /// <returns></returns>
        public string GetWhereSql_WR_RM(string _RRoleCode_Cur)
        {
            string sql = ""
                + "'" + this.UserID + "'='" + MyPara.AdminID + "'"
                + " or "
                + "" + _RRoleCode_Cur + "='" + RRoleCode + "'";

            return sql;
        }

        public string Get_WhereSql_Para(string Table, string SearchPara1, string Type, string ParaName)
        {
            if (!String.IsNullOrEmpty(Table))
            {
                SearchPara1 = Table + "." + SearchPara1;
            }

            string Para = "";
            if (ParaName == "1")
            {
                Para = this.Para1;
            }
            if (ParaName == "2")
            {
                Para = this.Para2;
            }
            if (ParaName == "3")
            {
                Para = this.Para3;
            }
            if (ParaName == "4")
            {
                Para = this.Para4;
            }
            if (ParaName == "5")
            {
                Para = this.Para5;
            }
            if (ParaName == "6")
            {
                Para = this.Para6;
            }
            if (ParaName == "7")
            {
                Para = this.Para7;
            }
            if (ParaName == "8")
            {
                Para = this.Para8;
            }
            if (ParaName == "9")
            {
                Para = this.Para9;
            }

            if (Para == null)
            {
                return "1=1";
            }

            string sql = "";
            if (Type == "=")
            {
                sql = ""
                    + "'" + Para + "'='' or ('" + Para + "'!='' and " + SearchPara1 + "='" + Para + "')";
            }
            else if (Type == "like")
            {
                sql = ""
                    + "'" + Para + "'='' or ('" + Para + "'!='' and " + SearchPara1 + " like '%" + Para + "%')";
            }

            return sql;
        }
    }
}