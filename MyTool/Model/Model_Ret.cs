namespace MyTool.Model
{
    public class Model_Ret
    {
        public Model_Ret()
        {
            ret_msg = "";
            ret_status = (int)MyEnum.MyEnum.Enum_Ret.Error;
            mrd01 = new Model_Ret_Detail();
            mrd02 = new Model_Ret_Detail();
            mrd03 = new Model_Ret_Detail();
            mrd04 = new Model_Ret_Detail();
            mrd05 = new Model_Ret_Detail();
            mrd06 = new Model_Ret_Detail();
            mrd07 = new Model_Ret_Detail();
            mrd08 = new Model_Ret_Detail();
            mrd09 = new Model_Ret_Detail();
            mrd10 = new Model_Ret_Detail();
            mrd11 = new Model_Ret_Detail();
            mrd12 = new Model_Ret_Detail();
            mrd13 = new Model_Ret_Detail();
            mrd14 = new Model_Ret_Detail();
            mrd15 = new Model_Ret_Detail();
            mrd16 = new Model_Ret_Detail();
            mrd17 = new Model_Ret_Detail();
            mrd18 = new Model_Ret_Detail();
            mrd19 = new Model_Ret_Detail();
            mrd20 = new Model_Ret_Detail();
        }

        /// <summary>
        /// 转json格式
        /// </summary>
        /// <returns></returns>
        public string Get_Ret()
        {
            string str = ""
                + "{"
                + "'ret_status':'" + this.ret_status + "','ret_msg':'" + this.ret_msg + "','ret_guid':'" + ret_guid + "',"
                + "'obj1':" + mrd01.get_json() + ",'obj2':" + mrd02.get_json() + ",'obj3':" + mrd03.get_json() + ",'obj4':" + mrd04.get_json() + ",'obj5':" + mrd05.get_json() + ","
                + "'obj6':" + mrd06.get_json() + ",'obj7':" + mrd07.get_json() + ",'obj8':" + mrd08.get_json() + ",'obj9':" + mrd09.get_json() + ",'obj10':" + mrd10.get_json() + ","
                + "'obj11':" + mrd11.get_json() + ",'obj12':" + mrd12.get_json() + ",'obj13':" + mrd13.get_json() + ",'obj14':" + mrd14.get_json() + ",'obj15':" + mrd15.get_json() + ","
                + "'obj16':" + mrd16.get_json() + ",'obj17':" + mrd17.get_json() + ",'obj18':" + mrd18.get_json() + ",'obj19':" + mrd19.get_json() + ",'obj20':" + mrd20.get_json() + ""
                + "}";

            ret_msg = "";
            ret_status = (int)MyEnum.MyEnum.Enum_Ret.Error;
            mrd01 = new Model_Ret_Detail();
            mrd02 = new Model_Ret_Detail();
            mrd03 = new Model_Ret_Detail();
            mrd04 = new Model_Ret_Detail();
            mrd05 = new Model_Ret_Detail();
            mrd06 = new Model_Ret_Detail();
            mrd07 = new Model_Ret_Detail();
            mrd08 = new Model_Ret_Detail();
            mrd09 = new Model_Ret_Detail();
            mrd10 = new Model_Ret_Detail();
            mrd11 = new Model_Ret_Detail();
            mrd12 = new Model_Ret_Detail();
            mrd13 = new Model_Ret_Detail();
            mrd14 = new Model_Ret_Detail();
            mrd15 = new Model_Ret_Detail();
            mrd16 = new Model_Ret_Detail();
            mrd17 = new Model_Ret_Detail();
            mrd18 = new Model_Ret_Detail();
            mrd19 = new Model_Ret_Detail();
            mrd20 = new Model_Ret_Detail();

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
        /// Guid
        /// </summary>
        public string ret_guid { get; set; }

        public Model_Ret_Detail mrd01 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd02 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd03 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd04 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd05 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd06 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd07 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd08 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd09 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd10 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd11 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd12 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd13 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd14 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd15 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd16 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd17 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd18 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd19 = new Model_Ret_Detail();
        public Model_Ret_Detail mrd20 = new Model_Ret_Detail();
    }
}