using System.Web.Mvc;
using Web.MyLib;

namespace Web.Controllers
{
    public class A02_HomePageController : BaseController
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Home_Test()
        {
            return View();
        }

        public ActionResult LoginOut()
        {
            CookieHelper.ClearCookie("UserID");

            return RedirectPermanent("/A01_Login/Login");
        }

        public ActionResult EditPassword()
        {
            return View();
        }
    }
}