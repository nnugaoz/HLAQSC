using MyTool.Model;
using System.Net.Http;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class Z01_LoginController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public HttpResponseMessage Login(string LoginName, string Password)
        {
            T1_User obj = new T1_User();
            obj.LoginName = LoginName;
            obj.Password_MD5 = MD5.Encode(Password);

            _model_ret.ret_status = obj.SCJLogin_GetOne_Limit(ref _model_ret.mrd01.dt);
            _model_ret.mrd01.is_one = true;

            return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
