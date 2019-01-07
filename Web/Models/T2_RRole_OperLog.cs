using MyTool.DB;
using MyTool.MyEnum;
using System;
using System.Data;
using Web.MyLib;

namespace Web.Models
{
    public class T2_RRole_OperLog : AutoFiles.T2_RRole_OperLog
    {
        public int WR_GetPageList(ref DataTable dt)
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + pageList.PageList_Index + " - 1) * " + pageList.PageList_Count + " + 1 "
                + " set @ei = " + pageList.PageList_Index + " * " + pageList.PageList_Count + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from T2_RRole_OperLog "
                + " where 1=1 "
                    + " and ('" + pageList.Para1 + "' = '' or T2_RRole_OperLog.WorkRecordID = '" + pageList.Para1 + "') "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ",T2_RRole_OperLog.* "
                        + ",T1_User.Name UserName "
                        + ",(case T2_RRole_OperLog.Result when '0' then (case when T2_RRole.Type = '3' then '提交' else '通过' end) else '不通过' end) Result_Str "
                        + ",convert(varchar(100), T2_RRole_OperLog.RDate, 20) RDate1 "
                    + " from T2_RRole_OperLog "
                        + " left join T1_User on T2_RRole_OperLog.UserID = T1_User.ID "
                        + " left join T2_RRole on T2_RRole_OperLog.RRoleCode = T2_RRole.Code "
                    + " where 1=1 "
                        + " and ('" + pageList.Para1 + "' = '' or T2_RRole_OperLog.WorkRecordID = '" + pageList.Para1 + "') "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return DataTool.Get_DataTable_From_DataSet_2(sql, ref dt);
        }

        public int WR_Update()
        {
            string sql = ""
                + " declare @ret varchar(100) "
                + " exec SP_RRole_Change '" + WorkRecordID + "', '" + UserID + "', '" + Result + "', '" + Remark + "', @ret output "
                + " select @ret ";

            DataTable dt = new DataTable();
            if (DataTool.Get_DataTable_From_DataSet_2(sql, ref dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 分页相关
        /// </summary>
        public PageList pageList = new PageList();
    }
}