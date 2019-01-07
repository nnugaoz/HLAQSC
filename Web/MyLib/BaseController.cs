using Web.Models;
using MyTool.MyEnum;
using MyTool.DB;
using System;
using System.Data;
using System.Web.Mvc;

namespace Web.MyLib
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            #region 获取Url参数
            string CurrPage = Request.QueryString["CurrPage"];
            if (String.IsNullOrEmpty(CurrPage))
            {
                ViewBag.CurrPage = "";
            }
            else
            {
                ViewBag.CurrPage = CurrPage;
            }
            #endregion 获取Url参数

            #region 获取用户信息
            string userid = CookieHelper.GetCookieValue("UserID");
            if (String.IsNullOrEmpty(userid))
            {
                //重定向至登录页面
                filterContext.Result = RedirectToAction("Login", "A01_Login", new { url = Request.RawUrl });
                return;
            }

            T1_User user = new T1_User();
            user.ID = userid;

            DataTable dt = new DataTable();
            if (user.BC_GetOne_Limit(ref dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                ViewBag.UserID = dt.Rows[0]["ID"].ToString();
                ViewBag.UserName = dt.Rows[0]["Name"].ToString();

                if (ViewBag.UserID == MyPara.AdminID) // 管理员账户
                {
                    ViewBag.OrgCode = "";
                    ViewBag.RRoleCode = "";
                    ViewBag.DRoleType = "";

                    ViewBag.MyPages = CacheAndMenu("Admin", "");
                }
                else // 普通账户
                {
                    ViewBag.OrgCode = dt.Rows[0]["OrgCode"].ToString();
                    ViewBag.RRoleCode = dt.Rows[0]["RRoleCode"].ToString();
                    ViewBag.DRoleType = dt.Rows[0]["DRoleType"].ToString();

                    ViewBag.MyPages = CacheAndMenu("", dt.Rows[0]["PRoleID"].ToString());
                }
            }
            else
            {
                //重定向至登录页面
                filterContext.Result = RedirectToAction("Login", "A01_Login", new { url = Request.RawUrl });
                return;
            }
            #endregion 获取用户信息
        }


        private string CacheAndMenu(string type, string PRole_ID)
        {
            if (CacheHelper.GetCache(PRole_ID) != null && (string)CacheHelper.GetCache(PRole_ID) != "")
            {
                return (string)CacheHelper.GetCache(PRole_ID);
            }
            else
            {
                T1_Page page = new T1_Page();

                DataTable dt = new DataTable();

                int ret = 0;
                if (type == "Admin")
                {
                    ret = page.BC_GetAll_Admin(ref dt);
                }
                else
                {
                    ret = page.BC_GetAll_Limit(ref dt, PRole_ID);
                }
                if (ret == (int)MyEnum.Enum_Ret.Succes)
                {
                    string ret_str = "";
                    if (DataTool.Get_Json_From_DataTable(dt, ref ret_str, false))
                    {
                        CacheHelper.SetCache(PRole_ID, ret_str, 3600);

                        return ret_str;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
    }
}