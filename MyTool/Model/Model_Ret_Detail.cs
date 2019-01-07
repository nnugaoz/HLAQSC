using MyTool.DB;
using System.Data;

namespace MyTool.Model
{
    public class Model_Ret_Detail
    {
        public Model_Ret_Detail()
        {
            ret_msg = "";
            ret_status = (int)MyEnum.MyEnum.Enum_Ret.Error;
            ret_json = "null";
            is_one = false;
            dt = new DataTable();
        }

        public string get_json()
        {
            string str = ret_json;
            if (str == "null")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTool.Get_Json_From_DataTable(dt, ref str, is_one);
                }
            }
            return str;
        }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 返回的状态
        /// </summary>
        public int ret_status { get; set; }
        /// <summary>
        /// 返回的json数据
        /// </summary>
        public string ret_json { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTable dt = new DataTable();
        /// <summary>
        /// 是否单条数据
        /// </summary>
        public bool is_one = false;
    }
}