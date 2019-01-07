using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T3_EquipmentType : AutoFiles.T3_EquipmentType
    {
        #region 设备类型
        public int ET_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T3_EquipmentType "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T3_EquipmentType.ID) i "
                        + ",T3_EquipmentType.* "
                        + ",T1_DataDirc.DircTitle TypeStr "
                        + ",(case T3_EquipmentType.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T3_EquipmentType "
                        + " left join T1_DataDirc on T1_DataDirc.Type = 'EquipmentType' and T3_EquipmentType.Type = T1_DataDirc.DircKey "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int ET_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool ET_UpdateOne()
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
                sql += " select @ID = 'ET' + dbo.FP_Tool_IDAddOne((select max(ID) from T3_EquipmentType), 10) ";
                sql += " insert into T3_EquipmentType(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T3_EquipmentType "
                + " set "
                    + " Title = '" + Title + "' "
                    + ",Type = '" + Type + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool ET_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 设备类型

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}