using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_RRole : AutoFiles.T2_RRole
    {
        #region 用户权限
        public int RRole_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_RRole "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select T2_RRole.Code)) i "
                        + ",T2_RRole.* "
                        + ",T1_DataDirc.DircTitle TypeStr "
                        + ",(case T2_RRole.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T2_RRole "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'RRoleType' and T2_RRole.Type = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int RRole_GetAll_ZTree(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Code) i "
                    + ",Code id "
                    + ",Title name "
                    + ",left(Code, len(Code) - 3) pId "
                + " from T2_RRole "
                + " where 1=1 "
                    + " and Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int RRole_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        internal string RRole_GetCodeByTitle()
        {
            string sql = "";
            DataTable lDT = null;
            string lRRoleCode = "";

            Select(ref sql, " AND T2_RRole.Title='" + Title + "'");

            DataTool.Get_DataTable_From_DataSet_2(sql, ref lDT);
            if (lDT != null && lDT.Rows.Count > 0)
            {
                lRRoleCode = lDT.Rows[0]["Code"].ToString();
            }
            return lRRoleCode;
        }

        public bool RRole_UpdateOne()
        {
            string sql = "";
            bool is_add = false;

            if (String.IsNullOrEmpty(ID))
            {
                // ID为空，新增
                is_add = true;
            }

            sql += " declare @ID varchar(100) ";
            sql += " declare @Code varchar(100) ";
            if (is_add)
            {
                sql += " select @ID = 'RR' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_RRole), 10) ";
                sql += " insert into T2_RRole(ID) values(@ID) ";
                sql += " select @Code = dbo.FP_Tool_CodeAddOne('" + PCode + "', (select max(Code) from T2_RRole where Code like '" + PCode + "___')) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
                sql += " select @Code = '" + Code + "' ";
            }

            sql += ""
                + " update T2_RRole "
                + " set "
                    + " Code = @Code "
                    + ",Type = '" + Type + "' "
                    + ",Title = '" + Title + "' "
                    + ",Remark = '" + Remark + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool RRole_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            if (Del == "0")
            {
                sql += ""
                    + " declare @code varchar(100) "
                    + " select @code = Code from T2_RRole where ID = '" + ID + "' "
                    + " declare @bi int "
                    + " set @bi = 3 "
                    + " declare @ei int "
                    + " set @ei = len(@code) "

                    + " while(@bi <= @ei) "
                    + " begin "

                        + " update T2_RRole "
                        + " set Del = '0' "
                        + " where 1=1 "
                            + " and Code = left(@code, @bi) "

                        + " set @bi = @bi + 3 "
                    + " end ";
            }
            if (Del == "1")
            {
                sql += ""
                    + " declare @code varchar(100) "
                    + " select @code = Code from T2_RRole where ID = '" + ID + "' "

                    + " update T2_RRole "
                    + " set Del = '1' "
                    + " where 1=1 "
                        + " and Code like @code + '___%' ";
            }

            return DataTool.Update(sql);
        }

        public bool RRoleUser_UpdateOne()
        {
            string sql = ""
                + " declare @id varchar(100) "
                + " select @id = ID from T2_RRole where Code = '" + Code + "' ";

            if (RRole_User_UpdateType == "1")
            {
                sql += ""
                    + " delete T2_RRole_User where UserID = '" + RRole_UserID + "' "
                    + " insert into T2_RRole_User values(@id, '" + RRole_UserID + "') ";
            }
            if (RRole_User_UpdateType == "0")
            {
                sql += ""
                    + " delete T2_RRole_User where RRoleID = @id and UserID = '" + RRole_UserID + "' ";
            }
            if (RRole_User_UpdateType == "11" || RRole_User_UpdateType == "10")
            {
                sql += ""
                    + " declare @t_user table(userid varchar(100)) "
                    + " insert into @t_user "
                    + " select ID "
                    + " from T1_User "
                    + " where 1=1 "
                        + " and Del = '0' "
                        + " and ('" + OrgCode + "' = '' or T1_User.OrgCode = '" + OrgCode + "') "
                        + " and ('" + UserName + "' = '' or T1_User.Name like '%" + UserName + "%') "
                        + " and ('" + UserJob + "' = '' or T1_User.JobCode = '" + UserJob + "') ";

                if (RRole_User_UpdateType == "11")
                {
                    sql += ""
                        + " delete T2_RRole_User "
                        + " where 1=1 "
                            + " and UserID in ( "
                                + " select userid "
                                + " from @t_user "
                            + " ) "

                        + " insert into T2_RRole_User "
                        + " select @id, [@t_user].userid "
                        + " from @t_user ";
                        //+ " where 1=1 "
                        //    + " and [@t_user].userid not in ( "
                        //        + " select UserID "
                        //        + " from T2_RRole_User "
                        //        + " where 1=1 "
                        //            + " and RRoleID = @id "
                        //    + " ) ";
                }
                if (RRole_User_UpdateType == "10")
                {
                    sql += ""
                        + " delete T2_RRole_User "
                        + " where 1=1 "
                            + " and RRoleID = @id "
                            + " and UserID in ( "
                                + " select userid "
                                + " from @t_user "
                            + " ) ";
                }
            }

            return DataTool.Update(sql);
        }
        #endregion 用户权限

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string PCode { get; set; }

        public string TypeStr { get; set; }

        public string RRole_UserID { get; set; }
        public string RRole_User_UpdateType { get; set; }
        public string OrgCode { get; set; }
        public string UserName { get; set; }
        public string UserJob { get; set; }
    }
}