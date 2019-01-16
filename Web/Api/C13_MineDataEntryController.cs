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
    public class C13_MineDataEntryController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetPageList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T8_WR obj = new T8_WR();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.MDE_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoCheckData(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T5_WorkRecord obj = new T5_WorkRecord();
            MyClass<T5_WorkRecord> myClass = new MyClass<T5_WorkRecord>(ref obj, para);

            if (obj.MDE_GetOne_ByDate(ref _model_ret.mrd01.dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.KeyError;
            }
            else
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            }
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T5_WorkRecord obj = new T5_WorkRecord();
            MyClass<T5_WorkRecord> myClass = new MyClass<T5_WorkRecord>(ref obj, para);

            if (obj.MDE_UpdateOne())
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

            T2_RRole_OperLog obj = new T2_RRole_OperLog();
            MyClass<T2_RRole_OperLog> myClass = new MyClass<T2_RRole_OperLog>(ref obj, para);

            _model_ret.ret_status = obj.WR_Update();
            return _model_ret.Get_Ret();
        }
    }
}
