using MyTool.Model;
using MyTool.MyClass;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class F03_ReportController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T8_Report3 obj = new T8_Report3();
            obj.pageList = pageList;
            
            _model_ret.ret_status = obj.R3_GetList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }
    }
}
