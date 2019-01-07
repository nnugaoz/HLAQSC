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
    public class B02_DRoleController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetPageList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T2_DRole obj = new T2_DRole();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.DRole_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T2_DRole obj = new T2_DRole();
            MyClass<T2_DRole> myClass = new MyClass<T2_DRole>(ref obj, para);

            if (obj.DRole_UpdateOne())
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoUpdate(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T2_DRole obj = new T2_DRole();
            MyClass<T2_DRole> myClass = new MyClass<T2_DRole>(ref obj, para);

            if (obj.DRole_UpdateOne_S())
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
