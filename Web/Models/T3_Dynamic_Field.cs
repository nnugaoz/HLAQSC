using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T3_Dynamic_Field : AutoFiles.T3_Dynamic_Field
    {
        #region 工种
        public int DF_Job_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T3_Dynamic_Field "
                + " where 1=1 "
                    + " and T3_Dynamic_Field.Type1 = '" + pageList.Para1 + "' "
                    + " and T3_Dynamic_Field.Type2 = '" + pageList.Para2 + "' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T3_Dynamic_Field.I) i "
                        + ",T3_Dynamic_Field.ID, T3_Dynamic_Field.Type1, T3_Dynamic_Field.Type2 "
                        + ",T3_Dynamic_Field.FieldKey, T3_Dynamic_Field.FieldName, T3_Dynamic_Field.FieldUnit, T3_Dynamic_Field.FieldType, T3_Dynamic_Field.FieldMode "
                        + ",T_Job.DircTitle Job_Str "
                        + ",T_Unit.DircTitle Unit_Str "
                        + ",T_Type.DircTitle Type_Str "
                        + ",T_Mode.DircTitle Mode_Str "
                    + " from T3_Dynamic_Field "
                        + " left join T1_DataDirc T_Job on T_Job.Type = 'JobType' and T3_Dynamic_Field.Type2 = T_Job.DircKey "
                        + " left join T1_DataDirc T_Unit on T_Unit.Type = 'RRoleType' and T3_Dynamic_Field.FieldUnit = T_Unit.DircKey "
                        + " left join T1_DataDirc T_Type on T_Type.Type = 'RRoleType' and T3_Dynamic_Field.FieldType = T_Type.DircKey "
                        + " left join T1_DataDirc T_Mode on T_Mode.Type = 'RRoleType' and T3_Dynamic_Field.FieldMode = T_Mode.DircKey "
                    + " where 1=1 "
                        + " and T3_Dynamic_Field.Type1 = '" + pageList.Para1 + "' "
                        + " and T3_Dynamic_Field.Type2 = '" + pageList.Para2 + "' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int DF_Job_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool DF_Job_UpdateOne()
        {
            string sql = "";
            bool is_add = true;

            if (!String.IsNullOrEmpty(ID))
            {
                is_add = false;
            }

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                sql += " select @ID = 'DF' + dbo.FP_Tool_IDAddOne((select max(ID) from T3_Dynamic_Field), 10) ";
                sql += ""
                    + " insert into T3_Dynamic_Field(ID, Type1, Type2, I) "
                    + " select @ID, '" + Type1 + "', '" + Type2 + "', isnull(max(I), 0) + 1 from T3_Dynamic_Field where Type1 = '" + Type1 + "' and Type2 = '" + Type2 + "' ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T3_Dynamic_Field "
                + " set "
                    + " FieldKey = Type1 + '_' + Type2 + '_' + cast(I as varchar(100)) "
                    + ",FieldName = '" + FieldName + "' "
                    + ",FieldUnit = '" + FieldUnit + "' "
                    + ",FieldType = '" + FieldType + "' "
                    + ",FieldMode = '" + FieldMode + "' "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool DF_Job_DeleteOne()
        {
            string sql = "";
            Delete(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 工种

        #region 设备
        public int DF_Equipment_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T3_Dynamic_Field "
                + " where 1=1 "
                    + " and T3_Dynamic_Field.Type1 = '" + pageList.Para1 + "' "
                    + " and T3_Dynamic_Field.Type2 = '" + pageList.Para2 + "' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T3_Dynamic_Field.I) i "
                        + ",T3_Dynamic_Field.ID, T3_Dynamic_Field.Type1, T3_Dynamic_Field.Type2 "
                        + ",T3_Dynamic_Field.FieldKey, T3_Dynamic_Field.FieldName, T3_Dynamic_Field.FieldUnit, T3_Dynamic_Field.FieldType, T3_Dynamic_Field.FieldMode "
                        + ",T3_EquipmentType.Title EquipmentTypeName "
                        + ",T_Unit.DircTitle Unit_Str "
                        + ",T_Type.DircTitle Type_Str "
                        + ",T_Mode.DircTitle Mode_Str "
                    + " from T3_Dynamic_Field "
                        + " left join T3_EquipmentType on T3_EquipmentType.Type = T3_Dynamic_Field.Type2 "
                        + " left join T1_DataDirc T_Unit on T_Unit.Type = 'RRoleType' and T3_Dynamic_Field.FieldUnit = T_Unit.DircKey "
                        + " left join T1_DataDirc T_Type on T_Type.Type = 'RRoleType' and T3_Dynamic_Field.FieldType = T_Type.DircKey "
                        + " left join T1_DataDirc T_Mode on T_Mode.Type = 'RRoleType' and T3_Dynamic_Field.FieldMode = T_Mode.DircKey "
                    + " where 1=1 "
                        + " and T3_Dynamic_Field.Type1 = '" + pageList.Para1 + "' "
                        + " and T3_Dynamic_Field.Type2 = '" + pageList.Para2 + "' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int DF_Equipment_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool DF_Equipment_UpdateOne()
        {
            string sql = "";
            bool is_add = true;

            if (!String.IsNullOrEmpty(ID))
            {
                is_add = false;
            }

            sql += " declare @ID varchar(100) ";
            if (is_add)
            {
                sql += " select @ID = 'DF' + dbo.FP_Tool_IDAddOne((select max(ID) from T3_Dynamic_Field), 10) ";
                sql += ""
                    + " insert into T3_Dynamic_Field(ID, Type1, Type2, I) "
                    + " select @ID, '" + Type1 + "', '" + Type2 + "', isnull(max(I), 0) + 1 from T3_Dynamic_Field where Type1 = '" + Type1 + "' and Type2 = '" + Type2 + "' ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T3_Dynamic_Field "
                + " set "
                    + " FieldKey = Type1 + '_' + Type2 + '_' + cast(I as varchar(100)) "
                    + ",FieldName = '" + FieldName + "' "
                    + ",FieldUnit = '" + FieldUnit + "' "
                    + ",FieldType = '" + FieldType + "' "
                    + ",FieldMode = '" + FieldMode + "' "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }

        public bool DF_Equipment_DeleteOne()
        {
            string sql = "";
            Delete(ref sql, null);

            return DataTool.Update(sql);
        }
        #endregion 设备

        #region 设备数据录入
        public int EDE_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T3_Dynamic_Field.* "
                    + ",T1_DataDirc.DircTitle FieldUnitName "
                + " from T3_Dynamic_Field "
                    + " left join T1_DataDirc on T1_DataDirc.Type = 'FieldUnit' and T3_Dynamic_Field.FieldUnit = T1_DataDirc.DircKey "
                + " where 1=1 "
                    + " and T3_Dynamic_Field.Type1 = 'Equipment' "
                + " order by T3_Dynamic_Field.Type2, T3_Dynamic_Field.I ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int EDE_GetDFType(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Type) i "
                    + ",DFKey "
                    + ",Type SelectVal "
                    + ",Title SelectTitle "
                + " from T3_Dynamic_FieldType "
                + " where 1=1 "
                    + " and DFKey like 'Equipment_%' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 设备数据录入

        #region 井下数据录入
        public int MDE_GetOne(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T3_Dynamic_Field.* "
                    + ",T1_DataDirc.DircTitle FieldUnitName "
                + " from T3_Dynamic_Field "
                    + " left join T1_DataDirc on T1_DataDirc.Type = 'FieldUnit' and T3_Dynamic_Field.FieldUnit = T1_DataDirc.DircKey "
                + " where 1=1 "
                    + " and T3_Dynamic_Field.Type1 = 'Job' "
                    + " and T3_Dynamic_Field.Type2 = '" + Type2 + "' "
                + " order by T3_Dynamic_Field.Type2, T3_Dynamic_Field.I ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int MDE_GetDFType(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Type) i "
                    + ",DFKey "
                    + ",Type SelectVal "
                    + ",Title SelectTitle "
                + " from T3_Dynamic_FieldType "
                + " where 1=1 "
                    + " and DFKey like 'Job_%' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int MDE_GetDFUnit(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Type) i "
                    + ",DFKey "
                    + ",Type SelectVal "
                    + ",Title SelectTitle "
                    + ",Unit_0_Rate "
                + " from T3_Dynamic_FieldUnit "
                + " where 1=1 "
                    + " and DFKey like 'Job_%' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 井下数据录入

        #region 接口
        public int SCJBatch_GetAllList(ref DataTable dt, string LoginName)
        {
            string sql = ""
                + " select "
                    + " T3_Dynamic_Field.* "
                    + ",T1_DataDirc.DircTitle FieldUnitName "
                + " from T3_Dynamic_Field "
                    + " left join T1_DataDirc on T1_DataDirc.Type = 'FieldUnit' and T3_Dynamic_Field.FieldUnit = T1_DataDirc.DircKey "
                + " where 1=1 "
                    + " and T3_Dynamic_Field.Type1 = 'Job' "
                    + " and T3_Dynamic_Field.Type2 = (select JobCode from T1_User where LoginName = '" + LoginName + "') "
                + " order by T3_Dynamic_Field.Type2, T3_Dynamic_Field.I ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_Type_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T3_Dynamic_FieldType.* "
                + " from T3_Dynamic_FieldType "
                + " where 1=1 "
                    + " and T3_Dynamic_FieldType.DFKey like 'Job_%' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int SCJBatch_Unit_GetAllList(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " T3_Dynamic_FieldUnit.* "
                + " from T3_Dynamic_FieldUnit "
                + " where 1=1 "
                    + " and T3_Dynamic_FieldUnit.DFKey like 'Job_%' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 接口

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}