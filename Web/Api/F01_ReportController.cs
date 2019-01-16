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
    public class F01_ReportController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T8_Report1 obj = new T8_Report1();
            obj.pageList = pageList;

            _model_ret.ret_status = obj.R1_GetList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoLoad(string ReportDate)
        {
            T8_Report1 obj = new T8_Report1();
            _model_ret.ret_status = obj.R1_Load(ReportDate, ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave(string ReportDate, string Vals)
        {
            T8_Report1 obj = new T8_Report1();
            if (obj.R1_Update(ReportDate, Vals))
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
