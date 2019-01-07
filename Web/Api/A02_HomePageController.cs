using MyTool.Model;
using MyTool.MyClass;
using MyTool.MyEnum;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class A02_HomePageController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string EditPassword(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User obj = new T1_User();
            MyClass<T1_User> myClass = new MyClass<T1_User>(ref obj, para);
            obj.Password_MD5 = MD5.Encode(obj.Password);

            if (obj.EditPassword_UpdateOne())
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }
    }
}
