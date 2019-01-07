using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_Org : AutoFiles.T2_Org
    {
        #region 组织机构
        public int Org_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_Org "
                + " where 1=1 "
                    + " and Title like '%" + pageList.Para1 + "%' "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by T2_Org.Code) i "
                        + ",* "
                        + ",(case T2_Org.Del when '0' then '' else '无效' end) Status_Str1 "
                    + " from T2_Org "
                    + " where 1=1 "
                        + " and Title like '%" + pageList.Para1 + "%' "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Org_GetAll_ZTree(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Code) i "
                    + ",Code id "
                    + ",STitle name "
                    + ",left(Code, len(Code) - 3) pId "
                + " from T2_Org "
                + " where 1=1 "
                    + " and Del = '0' ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int Org_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool Org_UpdateOne()
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
                sql += " select @ID = 'ORG' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Org), 10) ";
                sql += " insert into T2_Org(ID) values(@ID) ";
                sql += " select @Code = dbo.FP_Tool_CodeAddOne('" + PCode + "', (select max(Code) from T2_Org where Code like '" + PCode + "___')) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
                sql += " select @Code = '" + Code + "' ";
            }

            sql += ""
                + " update T2_Org "
                + " set "
                    + " Code = @Code "
                    + ",Type = '" + Type + "' "
                    + ",Title = '" + Title + "' "
                    + ",STitle = '" + STitle + "' "
                    + ",Del = '" + Del + "' "
                    + ",Lock = isnull(Lock, '0') "
                + " where ID = @ID ";

            sql += " exec dbo.SP_Org_UpdateRemark @Code ";

            return DataTool.Update(sql);
        }

        public bool Org_UpdateOne_S()
        {
            string sql = "";
            Update_1(ref sql, null);

            if (Del == "0")
            {
                sql += ""
                    + " declare @code varchar(100) "
                    + " select @code = Code from T2_Org where ID = '" + ID + "' "
                    + " declare @bi int "
                    + " set @bi = 3 "
                    + " declare @ei int "
                    + " set @ei = len(@code) "

                    + " while(@bi <= @ei) "
                    + " begin "

                        + " update T2_Org "
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
                    + " select @code = Code from T2_Org where ID = '" + ID + "' "

                    + " update T2_Org "
                    + " set Del = '1' "
                    + " where 1=1 "
                        + " and Code like @code + '___%' ";
            }

            return DataTool.Update(sql);
        }

        public string Org_GetCodeByTitle()
        {
            DataTable lDT = null;
            String lOrgCode = "";

            string sql = "";
            Select(ref sql, " AND T2_Org.Title='" + Title + "'");

            DataTool.Get_DataTable_From_DataSet_2(sql, ref lDT);

            if (lDT != null && lDT.Rows.Count > 0)
            {
                lOrgCode = lDT.Rows[0]["Code"].ToString();
            }
            return lOrgCode;
        }

        #endregion 组织机构

        #region 位置
        public int Position_GetAll_ZTree(ref DataTable dt)
        {
            string sql = ""
                + " select "
                    + " row_number() over (order by Code) i "
                    + ",Code id "
                    + ",STitle name "
                    + ",left(Code, len(Code) - 3) pId "
                + " from T2_Org "
                + " where 1=1 "
                    + " and Del = '0' "
                    + " and Type in ('1', '2') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 位置

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();

        public string PCode { get; set; }
    }
}