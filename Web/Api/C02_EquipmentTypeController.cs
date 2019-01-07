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
    public class C02_EquipmentTypeController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string GetPageList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T3_EquipmentType obj = new T3_EquipmentType();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.ET_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T3_EquipmentType obj = new T3_EquipmentType();
            MyClass<T3_EquipmentType> myClass = new MyClass<T3_EquipmentType>(ref obj, para);

            if (obj.ET_UpdateOne())
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

            T3_EquipmentType obj = new T3_EquipmentType();
            MyClass<T3_EquipmentType> myClass = new MyClass<T3_EquipmentType>(ref obj, para);

            if (obj.ET_UpdateOne_S())
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
        public string GetPageList_DF(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T3_Dynamic_Field obj = new T3_Dynamic_Field();
            obj.pageList = pageList;

            DataTable dt = new DataTable();
            _model_ret.ret_status = obj.DF_Equipment_GetPageList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave_DF(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T3_Dynamic_Field obj = new T3_Dynamic_Field();
            MyClass<T3_Dynamic_Field> myClass = new MyClass<T3_Dynamic_Field>(ref obj, para);

            if (obj.DF_Equipment_UpdateOne())
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
        public string GetOne_DF(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T3_Dynamic_Field obj = new T3_Dynamic_Field();
            MyClass<T3_Dynamic_Field> myClass = new MyClass<T3_Dynamic_Field>(ref obj, para);

            obj.DF_Equipment_GetOne(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string Delete_DF(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T3_Dynamic_Field obj = new T3_Dynamic_Field();
            MyClass<T3_Dynamic_Field> myClass = new MyClass<T3_Dynamic_Field>(ref obj, para);

            if (obj.DF_Equipment_DeleteOne())
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
