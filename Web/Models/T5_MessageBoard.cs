using MyTool.DB;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T5_MessageBoard : AutoFiles.T5_MessageBoard
    {
        #region 公告
        public int GG_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T5_MessageBoard "
                    + " left join T2_Position on T5_MessageBoard.PositionCode = T2_Position.Code "
                + " where 1=1 "
                    + " and ('" + pageList.Para1 + "' = '' or T2_Position.Title like '%" + pageList.Para1 + "%') "
                    + " and ('" + pageList.Para2 + "' = '' or convert(varchar(100), T5_MessageBoard.Date, 23) = '" + pageList.Para2 + "') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T5_MessageBoard.* "
                        + ",convert(varchar(100), T5_MessageBoard.Date, 20) Date1 "
                        + ",T2_Position.Title PositionTitle "
                        + ",T1_User.Name UserName "
                    + " from T5_MessageBoard "
                        + " left join T2_Position on T5_MessageBoard.PositionCode = T2_Position.Code "
                        + " left join T1_User on T5_MessageBoard.UserID = T1_User.ID "
                    + " where 1=1 "
                        + " and ('" + pageList.Para1 + "' = '' or T2_Position.Title like '%" + pageList.Para1 + "%') "
                        + " and ('" + pageList.Para2 + "' = '' or convert(varchar(100), T5_MessageBoard.Date, 23) = '" + pageList.Para2 + "') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int GG_GetOne(ref DataTable dt)
        {
            string sql = "";
            Select(ref sql, null);

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public bool GG_UpdateOne()
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
                sql += " select @ID = 'TM' + dbo.FP_Tool_IDAddOne((select max(ID) from T5_MessageBoard), 10) ";
                sql += " insert into T5_MessageBoard(ID) values(@ID) ";
            }
            else
            {
                sql += " select @ID = '" + ID + "' ";
            }

            sql += ""
                + " update T5_MessageBoard "
                + " set "
                    + " PositionCode = '" + PositionCode + "' "
                    + ",UserID = '" + UserID + "' "
                    + ",Remark = '" + Remark + "' "
                    + ",Date = getdate() "
                + " where ID = @ID ";

            return DataTool.Update(sql);
        }
        #endregion 公告

        #region 接口
        public int SCJBatch_GetAll(ref DataTable dt, string LoginName, string Code)
        {
            string sql = ""
                + " select T5_MessageBoard.* "
                + " from "
                    + " dbo.FT_SCJ_Position_ByLoginName('" + LoginName + "', '0') t1 "
                    + " left join T5_MessageBoard on t1.Code = T5_MessageBoard.PositionCode "
                + " where 1=1 "
                    + " and T5_MessageBoard.Date > dateadd(day, -3, getdate()) "
                    + " and ('" + Code + "' = '' or t1.Code = '" + Code + "') ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }
        #endregion 接口

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}