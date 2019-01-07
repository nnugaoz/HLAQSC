using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MyLib;

namespace Web.Controllers
{
    public class E01_SampleController : BaseController
    {
        //
        // GET: /E01_Sample/
        public ActionResult Index()
        {
            return View();
        }
	}
}