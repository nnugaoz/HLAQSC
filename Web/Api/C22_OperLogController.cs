using MyTool.Model;
using MyTool.MyClass;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class C22_OperLogController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetPageList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T2_RRole_OperLog obj = new T2_RRole_OperLog();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.WR_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }
    }
}
