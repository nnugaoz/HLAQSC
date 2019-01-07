using MyTool.Model;
using MyTool.MyEnum;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Api
{
    public class Z02_BatchController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        /// <summary>
        /// 基础数据
        /// 1、中段、采场、作业面树数据
        /// 2、采场
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage BatchData(string LoginName)
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Code = "";
            obj_position.Type = "1";
            obj_position.SCJBatch_GetAll_ByType(ref _model_ret.mrd01.dt, LoginName);
            obj_position.Type = "2";
            obj_position.SCJBatch_GetAll_ByType(ref _model_ret.mrd02.dt, LoginName);
            obj_position.Type = "3";
            obj_position.SCJBatch_GetAll_ByType(ref _model_ret.mrd03.dt, LoginName);

            T4_MP obj_mp = new T4_MP();
            obj_mp.SCJBatch_GetMonthInfo_ZD(ref _model_ret.mrd04.dt, LoginName, "");
            obj_mp.SCJBatch_GetMonthInfo_CC(ref _model_ret.mrd05.dt, LoginName, "", "");
            obj_mp.SCJBatch_GetMonthInfo_ZYM(ref _model_ret.mrd06.dt, LoginName, "", "", "");

            obj_mp.SCJBatch_GetInfo_ZD(ref _model_ret.mrd07.dt, LoginName, "");
            obj_mp.SCJBatch_GetInfo_CC(ref _model_ret.mrd08.dt, LoginName, "", "");
            obj_mp.SCJBatch_GetInfo_ZYM(ref _model_ret.mrd09.dt, LoginName, "", "", "");

            T5_MessageBoard obj_message = new T5_MessageBoard();
            _model_ret.mrd10.ret_json = "[]";
            obj_message.SCJBatch_GetAll(ref _model_ret.mrd10.dt, LoginName, "");

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ZYMStatus";
            obj_select.Common_GetAll(ref _model_ret.mrd11.dt);

            _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage BatchData2(string LoginName)
        {
            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd01.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.SCJBatch_GetAllList(ref _model_ret.mrd02.dt);
            obj_equi.SCJBatch_GetAllList_Position(ref _model_ret.mrd03.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.SCJBatch_GetAllList(ref _model_ret.mrd04.dt, LoginName);
            obj_df.SCJBatch_Type_GetAllList(ref _model_ret.mrd05.dt);
            obj_df.SCJBatch_Unit_GetAllList(ref _model_ret.mrd06.dt);

            _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
