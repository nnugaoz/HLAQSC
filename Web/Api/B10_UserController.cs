using MyTool.Model;
using MyTool.MyClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class B10_UserController : ApiController
    {

        private Model_Ret _model_ret = new Model_Ret();

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetUserList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T1_User lUser = new T1_User();
            lUser.pageList = pageList;

            _model_ret.ret_status = lUser.GeUserList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }


        // 检查登录名是否可用
        [HttpGet]
        public string CheckLoginName(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User lUser = new T1_User();
            MyClass<T1_User> myClass = new MyClass<T1_User>(ref lUser, para);

            if (lUser.CheckLoginName())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }


        // 检查登录名是否可用,Excel导入用
        [HttpGet]
        public string CheckLoginName_Excel(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User_Excel lUserExcel = new T1_User_Excel();
            MyClass<T1_User_Excel> myClass = new MyClass<T1_User_Excel>(ref lUserExcel, para);

            if (lUserExcel.CheckLoginName())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        /// <summary>
        /// 检查员工唯一标识
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        public string CheckUserKey(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User lUser = new T1_User();
            MyClass<T1_User> myClass = new MyClass<T1_User>(ref lUser, para);

            if (lUser.CheckUserKey())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        /// <summary>
        /// 检查员工唯一标识,Excel导入用
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        public string CheckUserKey_Excel(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User_Excel lUserExcel = new T1_User_Excel();
            MyClass<T1_User_Excel> myClass = new MyClass<T1_User_Excel>(ref lUserExcel, para);

            if (lUserExcel.CheckUserKey())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }


        [HttpGet]
        public string DoSave(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User obj = new T1_User();
            MyClass<T1_User> myClass = new MyClass<T1_User>(ref obj, para);

            if (obj.UpdateOne())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string DoSave_Excel(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            T1_User_Excel lUserExcel = new T1_User_Excel();
            MyClass<T1_User_Excel> myClass = new MyClass<T1_User_Excel>(ref lUserExcel, para);

            if (lUserExcel.UpdateOne())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetUserList_Excel(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T1_User_Excel lUserExcel = new T1_User_Excel();
            lUserExcel.pageList = pageList;

            _model_ret.ret_status = lUserExcel.GeUserList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        /// <summary>
        /// Excel导入验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string ExcelImportValidate()
        {
            T1_User_Excel lUserExcel = new T1_User_Excel();
            _model_ret.mrd01.ret_status = lUserExcel.User_GetDupLoginName(ref _model_ret.mrd01.dt);
            //_model_ret.mrd02.ret_status = lUserExcel.User_GetDupUserKey(ref _model_ret.mrd02.dt);

            if (_model_ret.mrd01.ret_status == 0 )
            {
                //验证过程执行失败！
                _model_ret.ret_status = 0;
            }
            else
            {
                //验证过程执行成功！
                _model_ret.ret_status = 1;
                if (_model_ret.mrd01.ret_status == 2)
                {
                    //验证通过！
                    _model_ret.ret_status = 2;
                }
            }
            return _model_ret.Get_Ret();
        }


        [HttpGet]
        public string ExcelImport()
        {
            T1_User_Excel lUserExcel = new T1_User_Excel();

            if (lUserExcel.UserDataImport())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string ToggleStatus(string ID)
        {
            T1_User lUser = new T1_User();
            lUser.ID = ID;
            if (lUser.ToggleStatus())
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes;
            }
            else
            {
                _model_ret.ret_status = (int)MyTool.MyEnum.MyEnum.Enum_Ret.Error;
            }
            return _model_ret.Get_Ret();
        }

        [HttpGet]
        public string ResetPassword(string ID)
        {
            T1_User lUser = new T1_User();
            lUser.ID = ID;
            lUser.Password_MD5 = MD5.Encode("123456");

            lUser.ResetPassword_UpdateOne();

            return _model_ret.Get_Ret();
        }
    }
}
