using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;


namespace Web.Models
{
    public class T3_Equipment : AutoFiles.T3_Equipment
    {
        #region 设备
        public int Equipment_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T3_Equipment "
                + " where 1=1 "
                    + " and ('" + pageList.Para1 + "' = '' or Title like '%" + pageList.Para1 + "%') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T3_Equipment.* "
                        + ",T3_EquipmentType.Title TypeName "
                        + ",(case T3_Equipment.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T3_Equipment "
                        + " left join T3_EquipmentType on T3_Equipment.Type = T3_EquipmentType.Type "
                    + " where 1=1 "
                        + " and ('" + pageList.Para1 + "' = '' or T3_Equipment.Title like '%" + pageList.Para1 + "%') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Equipment_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool Equipment_UpdateOne()
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
                sql += " select @ID = 'TE' + dbo.FP_Tool_IDAddOne((select max(ID) from T3_Equipment), 10) ";
                sql += " insert into T3_Equipment(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T3_Equipment "
                + " set "
                    + " Title = '" + Title + "' "
                    + ",Type = '" + Type + "' "
                    + ",Remark = '" + Remark + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool EP_UpdateOne()
        {
            string sql = "";

            sql += ""
                + " if exists(select 1 from T3_Equipment_Position where EquipmentID = '" + ID + "' and PositionCode = '" + PositionCode + "') "
                + " begin "
                    + " delete T3_Equipment_Position where EquipmentID = '" + ID + "' and PositionCode = '" + PositionCode + "' "
                + " end "
                + " else "
                + " begin "
                    //+ " delete T3_Equipment_Position where PositionCode = '" + PositionCode + "' "
                    + " insert into T3_Equipment_Position select '" + ID + "', '" + PositionCode + "' "
                + " end ";

            return DataTool.Update(sql);
        }

        public bool Equipment_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 设备

        #region 设备数据录入
        public int EDE_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T3_Equipment "
                + " where Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 设备数据录入

        #region 井下数据录入
        public int MDE_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T3_Equipment "
                + " where Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int MDE_GetAllList_Position(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T3_Equipment_Position ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 井下数据录入

        #region 接口
        public int SCJBatch_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T3_Equipment "
                + " where Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_GetAllList_Position(ref DataTable dt)
        {
            string sql = ""
                + " select * "
                + " from T3_Equipment_Position ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 接口

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string PositionCode { get; set; }
    }
}