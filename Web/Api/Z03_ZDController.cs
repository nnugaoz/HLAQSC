using MyTool.Model;
using MyTool.MyEnum;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Api
{
    public class Z03_ZDController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        /// <summary>
        /// 基础数据
        /// 1、中段、采场、作业面树数据
        /// 2、采场
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ListInit(string LoginName)
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Code = "";
            obj_position.Type = "1";
            obj_position.SCJBatch_GetAll_ByType(ref _model_ret.mrd01.dt, LoginName);

            T4_MP obj_mp = new T4_MP();
            obj_mp.SCJBatch_GetMonthInfo_ZD(ref _model_ret.mrd02.dt, LoginName, "");
            obj_mp.SCJBatch_GetInfo_ZD(ref _model_ret.mrd03.dt, LoginName, "");

            T5_MessageBoard obj_message = new T5_MessageBoard();
            _model_ret.mrd04.ret_json = "[]";
            obj_message.SCJBatch_GetAll(ref _model_ret.mrd04.dt, LoginName, "");

            _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
