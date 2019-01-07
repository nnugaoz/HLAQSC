using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T5_WorkRecord : AutoFiles.T5_WorkRecord
    {
        #region 通用
        public int WR_GetOne_ByID(ref DataTable dt)
        {
            string sql = ""
                + " select T5_WorkRecord.*, '{0}' Equipments, '{0}' Positions "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + "and ID = '" + ID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int WR_GetDetail_ByID(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T5_WorkRecord_Detail.* "
                    + ",T3_Equipment.Title EquipmentName "
                    + ",T2_Position.Title PositionName "
                    + ",T3_Equipment1.Title EquipmentName1 "
                + " from T5_WorkRecord_Detail "
                    + " left join T3_Equipment on T5_WorkRecord_Detail.EquipmentID = T3_Equipment.ID "
                    + " left join T2_Position on T5_WorkRecord_Detail.PositionCode = T2_Position.Code "
                    + " left join T3_Equipment T3_Equipment1 on T5_WorkRecord_Detail.WhereAbout = T3_Equipment1.ID "
                + " where 1=1 "
                    + "and T5_WorkRecord_Detail.WorkRecordID = '" + ID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int WR_GetDetailDF_ByID(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T5_WorkRecord_Detail_Field.* "
                    + ",T3_Dynamic_Field.FieldMode "
                    + ",T3_Dynamic_Field.FieldName "
                    + ",T3_Dynamic_FieldType.Title FieldTypeName "
                + " from T5_WorkRecord_Detail "
                    + " left join T5_WorkRecord_Detail_Field on T5_WorkRecord_Detail.ID = T5_WorkRecord_Detail_Field.WorkRecordDetailID "
                    + " left join T3_Dynamic_Field on T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_Field.FieldKey "
                    + " left join T3_Dynamic_FieldType on T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_FieldType.DFKey and T5_WorkRecord_Detail_Field.FieldType = T3_Dynamic_FieldType.Type "
                + " where 1=1 "
                    + "and T5_WorkRecord_Detail.WorkRecordID = '" + ID + "' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 通用

        #region 设备数据录入
        public int EDE_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and T5_WorkRecord.Del = '0' "
                    + " and T5_WorkRecord.RecordType = 'E' "
                    + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_EDE("T5_WorkRecord.WorkManID") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_WorkRecord.* "
                        + ",T5_WorkRecord.DF1 DF "
                        + ",T1_DataDirc.DircTitle Status_Str2 "
                    + " from T5_WorkRecord "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'WorkRecordStatus' and T5_WorkRecord.Status = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and T5_WorkRecord.Del = '0' "
                        + " and T5_WorkRecord.RecordType = 'E' "
                        + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_EDE("T5_WorkRecord.WorkManID") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int EDE_GetOne_ByDate(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and RecordType = 'E' "
                    + " and WorkManID = '" + WorkManID + "' "
                    + " and WorkDate = '" + WorkDate + "' "
                    + " and Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool EDE_UpdateOne()
        {
            string sql = "";
            bool is_add = false;

            if (String.IsNullOrEmpty(ID))
            {
                // ID为空，新增
                is_add = true;
            }

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                //sql += " select @ID = 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "' + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord where ID like 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "%'), 2) ";
                sql += " select @ID = 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "01' ";
                sql += " insert into T5_WorkRecord(ID, WorkManID, WorkManName, RRoleCode, RRoleCode_Cur) values(@ID, '" + WorkManID + "', '" + WorkManName + "', '" + RRoleCode + "', '" + RRoleCode + "') ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T5_WorkRecord "
                + " set "
                    + " RecordType = '" + RecordType + "' "
                    + ",WorkDate = '" + WorkDate + "' "
                    + ",WorkClassCode = '" + WorkClassCode + "' "
                    + ",WorkClassName = '" + WorkClassName + "' "
                    + ",WorkHour = '" + WorkHour + "' "
                    + ",Status = '" + Status + "' "
                    + ",Del = '" + Del + "' "
                + " where ID = @ID ";

            sql += ""
                + " delete T5_WorkRecord_Detail_Field where WorkRecordDetailID in (select ID from T5_WorkRecord_Detail where WorkRecordID = @ID) "
                + " delete T5_WorkRecord_Detail where WorkRecordID = @ID ";

            if (!String.IsNullOrEmpty(Equipments))
            {
                sql += " declare @DetailID varchar(100) ";

                // EquipmentID;WorkHour;FieldKey,FieldValue*FieldKey,FieldValue*FieldKey,FieldValue,FieldType**
                Equipments = Equipments.Replace("**", "%");
                string[] equis = Equipments.Split('%');
                if (equis != null && equis.Length > 0)
                {
                    for (int i = 0; i < equis.Length; i++)
                    {
                        string equi = equis[i];
                        string[] fs = equi.Split(';');
                        if (fs != null && fs.Length > 1)
                        {
                            sql += ""
                                + " select @DetailID = 'WRD' + replace(@ID, 'WR', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail where ID like 'WRD' + replace(@ID, 'WR', '') + '%'), 3) "

                                + " insert into T5_WorkRecord_Detail( "
                                    + " ID, WorkRecordID, EquipmentID, WorkHour "
                                + " ) values ( "
                                    + " @DetailID, @ID, '" + fs[0] + "', " + fs[1] + " "
                                + " ) ";

                            string[] dfs = fs[2].Split('*');
                            if (dfs != null && dfs.Length > 1)
                            {
                                for (int j = 0; j < dfs.Length; j++)
                                {
                                    string[] df = dfs[j].Split(',');

                                    if (df != null && df.Length > 1)
                                    {
                                        sql += ""
                                            + " insert into T5_WorkRecord_Detail_Field ( "
                                                + " ID, "
                                                + " WorkRecordDetailID, FieldKey, FieldValue, FieldType "
                                            + " ) values ( "
                                                + " 'WRDF' + replace(@DetailID, 'WRD', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail_Field where ID like 'WRDF' + replace(@DetailID, 'WRD', '') + '%'), 3), "
                                                + " @DetailID, '" + df[0] + "', '" + df[1] + "', '" + df[2] + "' "
                                            + " ) ";
                                    }
                                }
                            }

                            sql += ""
                                + " exec SP_WorkRecord_UpdateDF @ID, @DetailID ";
                        }
                    }
                }
            }

            sql += ""
                + " declare @ret varchar(100) "
                + " exec SP_RRole_Change @ID, '" + WorkManID + "', '0', '', @ret output "
                + " select @ret ";

            return DataTool.Update(sql);
        }
        #endregion 设备数据录入

        #region 设备数据
        public int ED_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and T5_WorkRecord.Del = '0' "
                    + " and T5_WorkRecord.RecordType = 'E' "
                    + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_ED("T5_WorkRecord.WorkManID", "T5_WorkRecord.RRoleCode") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_WorkRecord.* "
                        + ",T5_WorkRecord.DF1 DF "
                        + ",T1_DataDirc.DircTitle Status_Str2 "
                    + " from T5_WorkRecord "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'WorkRecordStatus' and T5_WorkRecord.Status = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and T5_WorkRecord.Del = '0' "
                        + " and T5_WorkRecord.RecordType = 'E' "
                        + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_ED("T5_WorkRecord.WorkManID", "T5_WorkRecord.RRoleCode") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 设备数据

        #region 井下数据录入
        public int MDE_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and T5_WorkRecord.Del = '0' "
                    + " and T5_WorkRecord.RecordType = 'M' "
                    + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_EDE("T5_WorkRecord.WorkManID") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_WorkRecord.* "
                        + ",T5_WorkRecord.DF1 DF "
                        + ",T1_DataDirc.DircTitle Status_Str2 "
                    + " from T5_WorkRecord "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'WorkRecordStatus' and T5_WorkRecord.Status = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and T5_WorkRecord.Del = '0' "
                        + " and T5_WorkRecord.RecordType = 'M' "
                        + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_EDE("T5_WorkRecord.WorkManID") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int MDE_GetOne_ByDate(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and RecordType = 'M' "
                    + " and WorkManID = '" + WorkManID + "' "
                    + " and WorkDate = '" + WorkDate + "' "
                    + " and Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool MDE_UpdateOne()
        {
            string sql = "";
            bool is_add = false;

            if (String.IsNullOrEmpty(ID))
            {
                // ID为空，新增
                is_add = true;
            }

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                //sql += " select @ID = 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "' + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord where ID like 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "%'), 2) ";
                sql += " select @ID = 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "02' ";
                sql += " insert into T5_WorkRecord(ID, WorkManID, WorkManName, RRoleCode, RRoleCode_Cur) values(@ID, '" + WorkManID + "', '" + WorkManName + "', '" + RRoleCode + "', '" + RRoleCode + "') ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T5_WorkRecord "
                + " set "
                    + " RecordType = '" + RecordType + "' "
                    + ",WorkDate = '" + WorkDate + "' "
                    + ",WorkClassCode = '" + WorkClassCode + "' "
                    + ",WorkClassName = '" + WorkClassName + "' "
                    + ",WorkHour = '" + WorkHour + "' "
                    + ",Status = '" + Status + "' "
                    + ",Del = '" + Del + "' "
                + " where ID = @ID ";

            sql += ""
                + " delete T5_WorkRecord_Detail_Field where WorkRecordDetailID in (select ID from T5_WorkRecord_Detail where WorkRecordID = @ID) "
                + " delete T5_WorkRecord_Detail where WorkRecordID = @ID ";

            if (!String.IsNullOrEmpty(Positions))
            {
                sql += " declare @DetailID varchar(100) ";

                // PositionCode;WorkHour;FieldKey,FieldValue*FieldKey,FieldValue*FieldKey,FieldValue**
                Positions = Positions.Replace("**", "%");
                string[] equis = Positions.Split('%');
                if (equis != null && equis.Length > 0)
                {
                    for (int i = 0; i < equis.Length; i++)
                    {
                        string equi = equis[i];
                        string[] fs = equi.Split(';');
                        if (fs != null && fs.Length > 1)
                        {
                            sql += ""
                                + " select @DetailID = 'WRD' + replace(@ID, 'WR', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail where ID like 'WRD' + replace(@ID, 'WR', '') + '%'), 3) "

                                + " insert into T5_WorkRecord_Detail( "
                                    + " ID, WorkRecordID, PositionCode, WhereAbout, WorkHour "
                                + " ) values ( "
                                    + " @DetailID, @ID, '" + fs[0] + "', '" + fs[1] + "', " + fs[2] + " "
                                + " ) ";

                            string[] dfs = fs[3].Split('*');
                            if (dfs != null && dfs.Length > 1)
                            {
                                for (int j = 0; j < dfs.Length; j++)
                                {
                                    string[] df = dfs[j].Split(',');

                                    if (df != null && df.Length > 1)
                                    {
                                        sql += ""
                                            + " insert into T5_WorkRecord_Detail_Field ( "
                                                + " ID, "
                                                + " WorkRecordDetailID, FieldKey, FieldValue, FieldType, FieldUnit "
                                            + " ) values ( "
                                                + " 'WRDF' + replace(@DetailID, 'WRD', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail_Field where ID like 'WRDF' + replace(@DetailID, 'WRD', '') + '%'), 3), "
                                                + " @DetailID, '" + df[0] + "', '" + df[1] + "', '" + df[2] + "', '" + df[3] + "' "
                                            + " ) ";
                                    }
                                }
                            }

                            sql += ""
                                + " exec SP_WorkRecord_UpdateDF @ID, @DetailID ";
                        }
                    }
                }
            }

            sql += ""
                + " declare @ret varchar(100) "
                + " exec SP_RRole_Change @ID, '" + WorkManID + "', '0', '', @ret output "
                + " select @ret ";

            return DataTool.Update(sql);
        }
        #endregion 井下数据录入

        #region 井下数据
        public int MD_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and T5_WorkRecord.Del = '0' "
                    + " and T5_WorkRecord.RecordType = 'M' "
                    + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_ED("T5_WorkRecord.WorkManID", "T5_WorkRecord.RRoleCode") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_WorkRecord.* "
                        + ",T5_WorkRecord.DF1 DF "
                        + ",T1_DataDirc.DircTitle Status_Str2 "
                    + " from T5_WorkRecord "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'WorkRecordStatus' and T5_WorkRecord.Status = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and T5_WorkRecord.Del = '0' "
                        + " and T5_WorkRecord.RecordType = 'M' "
                        + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_ED("T5_WorkRecord.WorkManID", "T5_WorkRecord.RRoleCode") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 井下数据

        #region 数据审核
        public int RM_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and T5_WorkRecord.Del = '0' "
                    + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_RM("T5_WorkRecord.RRoleCode_Cur") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_WorkRecord.* "
                        + ",T5_WorkRecord.DF1 DF "
                        + ",T1_DataDirc.DircTitle Status_Str2 "
                    + " from T5_WorkRecord "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'WorkRecordStatus' and T5_WorkRecord.Status = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and T5_WorkRecord.Del = '0' "
                        + " and ('" + pageList.Para1 + "' = '' or T5_WorkRecord.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T5_WorkRecord.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T5_WorkRecord.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T5_WorkRecord.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_RM("T5_WorkRecord.RRoleCode_Cur") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion

        #region 接口
        public bool SCJ_UpdateOne()
        {
            WorkDate = Convert.ToDateTime(WorkDate).ToString("yyyy-MM-dd");

            string sql = "";
            sql += ""
                + " declare @ID varchar(100) "
                + " select @ID = ID "
                + " from T5_WorkRecord "
                + " where 1=1 "
                    + " and WorkManID = '" + WorkManID + "' "
                    + " and WorkDate = '" + WorkDate + "' ";

            sql += ""
                + " if(@ID is null) "
                + " begin ";

            sql += ""
                    + " select @ID = 'WR" + WorkManID.Replace("UR", "") + WorkDate.Replace("-", "") + "02' "
                    + " insert into T5_WorkRecord(ID, WorkManID, WorkManName, RRoleCode, RRoleCode_Cur) "
                    + " select @ID"
                        + ",T1_User.ID, T1_User.Name, T1_User.RRoleCode, T1_User.RRoleCode "
                    + " from T1_User "
                    + " where 1=1 "
                        + " and ID = '" + WorkManID + "' ";

            sql += ""
                + " end "
                + " else "
                + " begin "
                    + " delete T5_WorkRecord_Detail_Field where WorkRecordDetailID in (select ID from T5_WorkRecord_Detail where WorkRecordID = @ID) "
                    + " delete T5_WorkRecord_Detail where WorkRecordID = @ID "
                + " end ";

            sql += ""
                + " update T5_WorkRecord "
                + " set "
                    + " RecordType = '" + RecordType + "' "
                    + ",WorkDate = '" + WorkDate + "' "
                    + ",WorkClassCode = '" + WorkClassCode + "' "
                    + ",WorkClassName = (select DircTitle from T1_DataDirc where Type = 'ClassType' and DircKey = '" + WorkClassCode + "') "
                    + ",Status = '" + Status + "' "
                    + ",Del = '" + Del + "' "
                + " where ID = @ID ";

            if (!String.IsNullOrEmpty(Positions))
            {
                sql += " declare @DetailID varchar(100) ";

                // PositionCode;WorkHour;FieldKey,FieldValue*FieldKey,FieldValue*FieldKey,FieldValue**
                Positions = Positions.Replace("**", "%");
                string[] equis = Positions.Split('%');
                if (equis != null && equis.Length > 0)
                {
                    for (int i = 0; i < equis.Length; i++)
                    {
                        string equi = equis[i];
                        string[] fs = equi.Split(';');
                        if (fs != null && fs.Length > 1)
                        {
                            sql += ""
                                + " select @DetailID = 'WRD' + replace(@ID, 'WR', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail where ID like 'WRD' + replace(@ID, 'WR', '') + '%'), 3) "

                                + " insert into T5_WorkRecord_Detail( "
                                    + " ID, WorkRecordID, PositionCode, WhereAbout, WorkHour "
                                + " ) values ( "
                                    + " @DetailID, @ID, '" + fs[0] + "', '" + fs[1] + "', " + fs[2] + " "
                                + " ) ";

                            string[] dfs = fs[3].Split('*');
                            if (dfs != null && dfs.Length > 1)
                            {
                                for (int j = 0; j < dfs.Length; j++)
                                {
                                    string[] df = dfs[j].Split(',');

                                    if (df != null && df.Length > 1)
                                    {
                                        sql += ""
                                            + " insert into T5_WorkRecord_Detail_Field ( "
                                                + " ID, "
                                                + " WorkRecordDetailID, FieldKey, FieldValue, FieldType, FieldUnit "
                                            + " ) values ( "
                                                + " 'WRDF' + replace(@DetailID, 'WRD', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T5_WorkRecord_Detail_Field where ID like 'WRDF' + replace(@DetailID, 'WRD', '') + '%'), 3), "
                                                + " @DetailID, '" + df[0] + "', '" + df[1] + "', '" + df[2] + "', '" + df[3] + "' "
                                            + " ) ";
                                    }
                                }
                            }

                            sql += ""
                                + " update T4_MP "
                                + " set "
                                    + " Status = '" + fs[4] + "' "
                                    + ",StatusChangeDate = getdate() "
                                + " where 1=1 "
                                    + " and Month = '" + DateTime.Now.ToString("yyyy-MM") + "' "
                                    + " and PositionCode = '" + fs[0] + "' ";

                            sql += ""
                                + " exec SP_WorkRecord_UpdateDF @ID, @DetailID "

                                + " update T5_WorkRecord "
                                + " set "
                                    + " WorkHour = (select sum(WorkHour) from T5_WorkRecord_Detail where WorkRecordID = @ID) "
                                + " where 1=1 "
                                    + " and ID = @ID ";
                        }
                    }
                }
            }

            sql += ""
                + " declare @ret varchar(100) "
                + " exec SP_RRole_Change @ID, '" + WorkManID + "', '0', '', @ret output "
                + " select @ret ";

            return DataTool.Update(sql);
        }
        #endregion 接口

        #region log
        public bool Txt_UpdateOne(string txt)
        {
            string sql = "";
            bool is_add = true;

            sql += " declare @ID varchar(100) ";
            sql += " select @ID = 'TT' + dbo.FP_Tool_IDAddOne((select max(ID) from T0_Log), 10) ";
            sql += " insert into T0_Log(ID, Txt) values(@ID, '" + txt + "') ";

            return DataTool.Update(sql);
        }
        #endregion log

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string Equipments { get; set; }

        public string Positions { get; set; }

        /// <summary>
        /// 审核结果 0 成功/1 失败
        /// </summary>
        public string RResult { get; set; }
    }
}