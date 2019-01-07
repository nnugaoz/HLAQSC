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
    public class C91_MessageBoardController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetPageList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T5_MessageBoard obj = new T5_MessageBoard();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.GG_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T5_MessageBoard obj = new T5_MessageBoard();
            MyClass<T5_MessageBoard> myClass = new MyClass<T5_MessageBoard>(ref obj, para);

            if (obj.GG_UpdateOne())
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
