using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T8_WR
    {
        public T8_WR()
        {
        }

        public string ID { get; set; }
        public string WorkDate { get; set; }
        public string WorkClassCode { get; set; }
        public string WorkClassName { get; set; }
        public string WorkManID { get; set; }
        public string WorkManName { get; set; }
        public string RRoleCode { get; set; }
        public string RRoleCode_Cur { get; set; }
        public string Status { get; set; }
        public string Del { get; set; }
        public string DF1 { get; set; }
        public string DF2 { get; set; }
        public string DF3 { get; set; }

        public List<T8_WR_Equipment_D> WR_Equipment_D_List { get; set; }

        public List<T8_WR_Position_Data1> WR_Position_Data1_List { get; set; }

        public List<T8_WR_Position_Data2_D> WR_Position_Data2_D_List { get; set; }

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
                + " T8_WR.ID "
                + ",T8_WR.WorkDate "
                + ",T8_WR.WorkClassCode "
                + ",T8_WR.WorkClassName "
                + ",T8_WR.WorkManID "
                + ",T8_WR.WorkManName "
                + ",T8_WR.RRoleCode "
                + ",T8_WR.RRoleCode_Cur "
                + ",T8_WR.Status "
                + ",T8_WR.Del "
                + ",T8_WR.DF1 "
                + ",T8_WR.DF2 "
                + ",T8_WR.DF3 "
                + " from [HLAQSC].dbo.T8_WR "
                + " where 1=1 ";
            if (String.IsNullOrEmpty(where))
            {
                sql += " and T8_WR.ID = '" + ID + "' ";
            }
            else
            {
                sql += where;
            }

            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_WR( ";

            int count = 0;
            if (!String.IsNullOrEmpty(ID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "ID ";
            }
            if (!String.IsNullOrEmpty(WorkDate))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkDate ";
            }
            if (!String.IsNullOrEmpty(WorkClassCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkClassCode ";
            }
            if (!String.IsNullOrEmpty(WorkClassName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkClassName ";
            }
            if (!String.IsNullOrEmpty(WorkManID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkManID ";
            }
            if (!String.IsNullOrEmpty(WorkManName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkManName ";
            }
            if (!String.IsNullOrEmpty(RRoleCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "RRoleCode ";
            }
            if (!String.IsNullOrEmpty(RRoleCode_Cur))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "RRoleCode_Cur ";
            }
            if (!String.IsNullOrEmpty(Status))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "Status ";
            }
            if (!String.IsNullOrEmpty(Del))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "Del ";
            }
            if (!String.IsNullOrEmpty(DF1))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF1 ";
            }
            if (!String.IsNullOrEmpty(DF2))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF2 ";
            }
            if (!String.IsNullOrEmpty(DF3))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF3 ";
            }

            sql += " ) ";
            sql += " select ";

            count = 0;
            if (!String.IsNullOrEmpty(ID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + ID + "' ";
            }
            if (!String.IsNullOrEmpty(WorkDate))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + WorkDate + "' ";
            }
            if (!String.IsNullOrEmpty(WorkClassCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + WorkClassCode + "' ";
            }
            if (!String.IsNullOrEmpty(WorkClassName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + WorkClassName + "' ";
            }
            if (!String.IsNullOrEmpty(WorkManID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + WorkManID + "' ";
            }
            if (!String.IsNullOrEmpty(WorkManName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + WorkManName + "' ";
            }
            if (!String.IsNullOrEmpty(RRoleCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";
            }
            if (!String.IsNullOrEmpty(RRoleCode_Cur))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + RRoleCode_Cur + "' ";
            }
            if (!String.IsNullOrEmpty(Status))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + Status + "' ";
            }
            if (!String.IsNullOrEmpty(Del))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + Del + "' ";
            }
            if (!String.IsNullOrEmpty(DF1))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + DF1 + "' ";
            }
            if (!String.IsNullOrEmpty(DF2))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + DF2 + "' ";
            }
            if (!String.IsNullOrEmpty(DF3))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "'" + DF3 + "' ";
            }

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
                + " update [HLAQSC].dbo.T8_WR "
                + " set "
                + " T8_WR.ID = '" + ID + "' "
                + ",T8_WR.WorkDate = '" + WorkDate + "' "
                + ",T8_WR.WorkClassCode = '" + WorkClassCode + "' "
                + ",T8_WR.WorkClassName = '" + WorkClassName + "' "
                + ",T8_WR.WorkManID = '" + WorkManID + "' "
                + ",T8_WR.WorkManName = '" + WorkManName + "' "
                + ",T8_WR.RRoleCode = '" + RRoleCode + "' "
                + ",T8_WR.RRoleCode_Cur = '" + RRoleCode_Cur + "' "
                + ",T8_WR.Status = '" + Status + "' "
                + ",T8_WR.Del = '" + Del + "' "
                + ",T8_WR.DF1 = '" + DF1 + "' "
                + ",T8_WR.DF2 = '" + DF2 + "' "
                + ",T8_WR.DF3 = '" + DF3 + "' "
                + " where 1=1 ";
            if (String.IsNullOrEmpty(where))
            {
                sql += " and T8_WR.ID = '" + ID + "' ";
            }
            else
            {
                sql += where;
            }

            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_WR "
                + " set ";

            int count = 0;
            if (!String.IsNullOrEmpty(ID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";
            }
            if (!String.IsNullOrEmpty(WorkDate))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkDate = '" + WorkDate + "' ";
            }
            if (!String.IsNullOrEmpty(WorkClassCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkClassCode = '" + WorkClassCode + "' ";
            }
            if (!String.IsNullOrEmpty(WorkClassName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkClassName = '" + WorkClassName + "' ";
            }
            if (!String.IsNullOrEmpty(WorkManID))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkManID = '" + WorkManID + "' ";
            }
            if (!String.IsNullOrEmpty(WorkManName))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "WorkManName = '" + WorkManName + "' ";
            }
            if (!String.IsNullOrEmpty(RRoleCode))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";
            }
            if (!String.IsNullOrEmpty(RRoleCode_Cur))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "RRoleCode_Cur = '" + RRoleCode_Cur + "' ";
            }
            if (!String.IsNullOrEmpty(Status))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "Status = '" + Status + "' ";
            }
            if (!String.IsNullOrEmpty(Del))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";
            }
            if (!String.IsNullOrEmpty(DF1))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF1 = '" + DF1 + "' ";
            }
            if (!String.IsNullOrEmpty(DF2))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF2 = '" + DF2 + "' ";
            }
            if (!String.IsNullOrEmpty(DF3))
            {
                count++;
                sql += (count > 1 ? "," : " ") + "DF3 = '" + DF3 + "' ";
            }

            sql += " where 1=1 ";
            if (String.IsNullOrEmpty(where))
            {
                sql += " and T8_WR.ID = '" + ID + "' ";
            }
            else
            {
                sql += where;
            }

            return true;
        }

        public bool Delete(ref string sql)
        {
            sql = "DELETE FROM T8_WR WHERE ID='" + ID + "'";
            sql += ";";
            sql += "DELETE FROM T8_WR_Equipment_D WHERE WRID='" + ID + "'";
            sql += ";";
            sql += "DELETE FROM T8_WR_Position_Data1 WHERE WRID='" + ID + "'";
            sql += ";";
            sql += "DELETE FROM T8_WR_Position_Data2_D WHERE WRID='" + ID + "'";
            sql += ";";
            return true;
        }

        #region 工作内容列表
        public int MDE_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T8_WR "
                + " where 1=1 "
                    + " and T8_WR.Del = '0' "
                    + " and ('" + pageList.Para1 + "' = '' or T8_WR.WorkDate = '" + pageList.Para1 + "') "
                    + " and ('" + pageList.Para2 + "' = '' or T8_WR.WorkClassCode = '" + pageList.Para2 + "') "
                    + " and ('" + pageList.Para3 + "' = '' or T8_WR.WorkManName like '%" + pageList.Para3 + "%') "
                    + " and ('" + pageList.Para4 + "' = '' or T8_WR.Status = '" + pageList.Para4 + "') "
                    + " and (" + pageList.GetWhereSql_WR_EDE("T8_WR.WorkManID") + ") "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T8_WR.WorkDate DESC,T8_WR.WorkClassCode DESC) i "
                        + ",T8_WR.* "
                    + " from T8_WR "
                    + " where 1=1 "
                        + " and T8_WR.Del = '0' "
                        + " and ('" + pageList.Para1 + "' = '' or T8_WR.WorkDate = '" + pageList.Para1 + "') "
                        + " and ('" + pageList.Para2 + "' = '' or T8_WR.WorkClassCode = '" + pageList.Para2 + "') "
                        + " and ('" + pageList.Para3 + "' = '' or T8_WR.WorkManName like '%" + pageList.Para3 + "%') "
                        + " and ('" + pageList.Para4 + "' = '' or T8_WR.Status = '" + pageList.Para4 + "') "
                        + " and (" + pageList.GetWhereSql_WR_EDE("T8_WR.WorkManID") + ") "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion

        #region 通用
        public int Get_ByID(ref DataTable dt)
        {
            string sql = ""
                + " select T8_WR.*"
                + " from T8_WR "
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
    }
}