using MyTool.Model;
using MyTool.MyClass;
using MyTool.MyEnum;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class A01_LoginController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string Login(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User obj = new T1_User();
            MyClass<T1_User> myClass = new MyClass<T1_User>(ref obj, para);
            obj.Password_MD5 = MD5.Encode(obj.Password);

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.Login_GetOne_Limit(ref dt);
            if (_model_ret.ret_status == (int)MyEnum.Enum_Ret.Succes)
            {
                CookieHelper.SetCookie("UserID", dt.Rows[0]["ID"].ToString());
            }
            return _model_ret.Get_Ret();
        }
    }
}
