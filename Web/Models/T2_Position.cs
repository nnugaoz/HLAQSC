using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_Position : AutoFiles.T2_Position
    {
        #region 位置
        public int Position_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_Position "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select T2_Position.Code)) i "
                        + ",T2_Position.* "
                        + ",T1_DataDirc.DircTitle TypeStr "
                        + ",(case T2_Position.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T2_Position "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'PositionType' and T2_Position.Type = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Position_GetAll_ZTree(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Code) i "
                    + ",Code id "
                    + ",Title name "
                    + ",left(Code, len(Code) - 3) pId "
                + " from T2_Position "
                + " where 1=1 "
                    + " and Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Position_GetOne(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T2_Position.* "
                    + ",T1_DataDirc.DircTitle TypeStr "
                    + ",isnull(T2_Org.STitle, '') OrgName "
                    + ",isnull(T2_Org.Code, '') OrgCode "
                + " from T2_Position "
                    + " left join T1_DataDirc on T1_DataDirc.Type = 'PositionType' and T2_Position.Type = T1_DataDirc.DircKey "
                    + " left join (select top 1 * from T2_Position_Org where T2_Position_Org.PositionCode = (select Code from T2_Position where ID = '" + ID + "')) t1 on 1 = 1 "
                    + " left join T2_Org on t1.OrgCode = T2_Org.Code "
                + " where 1=1 "
                    + " and T2_Position.ID = '" + ID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool Position_UpdateOne()
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
                sql += " select @ID = 'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10) ";
                sql += " insert into T2_Position(ID) values(@ID) ";
                sql += " select @Code = dbo.FP_Tool_CodeAddOne('" + PCode + "', (select max(Code) from T2_Position where Code like '" + PCode + "___')) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
                sql += " select @Code = '" + Code + "' ";
            }

            sql += ""
                + " update T2_Position "
                + " set "
                    + " Code = @Code "
                    + ",Type = '" + Type + "' "
                    + ",Title = '" + Title + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            sql += " exec dbo.SP_Position_UpdateRemark @Code ";

            return DataTool.Update(sql);
        }

        public bool Position_UpdateOne_Org()
        {
            string sql = "";

            sql += " declare @Code varchar(100) ";
            sql += " select @Code = '" + Code + "' ";
            sql += ""
                + " if exists(select 1 from T2_Position_Org where PositionCode = @Code) "
                + " begin "
                    + " update T2_Position_Org set OrgCode = '" + OrgCode + "' where PositionCode = @Code "
                + " end "
                + " else "
                + " begin "
                    + " insert into T2_Position_Org select @Code, '" + OrgCode + "' "
                + " end ";

            return DataTool.Update(sql);
        }

        public bool Position_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            if (Del == "0")
            {
                sql += ""
                    + " declare @code varchar(100) "
                    + " select @code = Code from T2_Position where ID = '" + ID + "' "
                    + " declare @bi int "
                    + " set @bi = 3 "
                    + " declare @ei int "
                    + " set @ei = len(@code) "

                    + " while(@bi <= @ei) "
                    + " begin "

                        + " update T2_Position "
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
                    + " select @code = Code from T2_Position where ID = '" + ID + "' "

                    + " update T2_Position "
                    + " set Del = '1' "
                    + " where 1=1 "
                        + " and Code like @code + '___%' ";
            }

            return DataTool.Update(sql);
        }
        #endregion 位置

        #region 设备-绑定采场
        public int Equipment_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_Position "
                    + " left join T2_Position T_P on left(T2_Position.Code, 3) = T_P.Code "
                        + " left join T2_Position_Org on T_P.Code = T2_Position_Org.PositionCode "
                        + " left join T2_Org on T2_Position_Org.OrgCode = T2_Org.Code "
                + " where 1=1 "
                    + " and T2_Position.Type = '2' "
                    + " and T2_Position.Del = '0' "
                    + " and ('" + pageList.Para1 + "' = '' or T2_Org.Code like '" + pageList.Para1 + "%') "
                    + " and ('" + pageList.Para2 + "' = '' or T_P.Title like '%" + pageList.Para2 + "%') "
                    + " and ('" + pageList.Para3 + "' = '' or T2_Position.Title like '%" + pageList.Para3 + "%') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select T2_Position.Code)) i "
                        + ",T2_Position.* "
                        + ",T2_Org.STitle OrgName "
                        + ",T_P.Title Title1 "
                        + ",T2_Position.Title Title2 "
                        + ",(case when T3_Equipment_Position.EquipmentID is null then '0' else '1' end) BindStatus "
                    + " from T2_Position "
                        + " left join T2_Position T_P on left(T2_Position.Code, 3) = T_P.Code "
                        + " left join T2_Position_Org on T_P.Code = T2_Position_Org.PositionCode "
                        + " left join T2_Org on T2_Position_Org.OrgCode = T2_Org.Code "
                        + " left join T3_Equipment_Position on T3_Equipment_Position.EquipmentID = '" + pageList.Para9 + "' and T2_Position.Code = T3_Equipment_Position.PositionCode "
                    + " where 1=1 "
                        + " and T2_Position.Type = '2' "
                        + " and T2_Position.Del = '0' "
                        + " and ('" + pageList.Para1 + "' = '' or T2_Org.Code like '" + pageList.Para1 + "%') "
                        + " and ('" + pageList.Para2 + "' = '' or T_P.Title like '%" + pageList.Para2 + "%') "
                        + " and ('" + pageList.Para3 + "' = '' or T2_Position.Title like '%" + pageList.Para3 + "%') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 设备-绑定采场

        #region 井下数据-绑定采场
        public int MDE_GetAll_ZTree(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " ROW_NUMBER() over (order by (select T2_Position.Code)) i "
                    + ",T2_Position.Code id "
                    + ",T2_Position.Title name "
                    + ",left(T2_Position.Code, len(T2_Position.Code) - 3) pId "
                + " from T2_Position "
                + " where 1=1 "
                    + " and T2_Position.Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 井下数据-绑定采场

        #region 接口
        public int SCJBatch_GetAll_ByType(ref DataTable dt, string LoginName)
        {
            string sql = ""
                + " select "
                    + " ROW_NUMBER() over (order by (select T2_Position.Code)) i "
                    + ",T2_Position.Code "
                    + ",T2_Position.Title "
                    + ",left(T2_Position.Code, len(T2_Position.Code) - 3) PCode "
                    + ",(case T2_Position.Type when '1' then '中段' when '2' then '采场' when '3' then '作业面' else '' end) Type"
                + " from "
                    + " dbo.FT_SCJ_Position_ByLoginName('" + LoginName + "', '" + Type + "') t1 "
                    + " left join T2_Position on t1.Code = T2_Position.Code "
                + " where 1=1 "
                    + " and T2_Position.Del = '0' "
                    + " and ('" + Code + "' = '' or T2_Position.Code like '" + Code + "___%') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJ_GetOne_ZYMManage(ref DataTable dt, string LoginName, string ZYMCode)
        {
            string sql = ""
                + " select "
                    + " T2_Position.Code "
                    + ",T2_Position.Title "
                + " from "
                    + " dbo.FT_SCJ_Position_ByLoginName('" + LoginName + "', '3') t1 "
                    + " left join T2_Position on t1.Code = T2_Position.Code "
                + " where 1=1 "
                    + " and T2_Position.Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 接口

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string PCode { get; set; }

        public string OrgName { get; set; }

        public string OrgCode { get; set; }
    }
}