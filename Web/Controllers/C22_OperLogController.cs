using MyTool.Model;
using System.Web.Mvc;
using Web.MyLib;

namespace Web.Controllers
{
    public class C22_OperLogController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult Layer_PageList(string WorkRecordID)
        {
            ViewBag.WorkRecordID = WorkRecordID;
            return View();
        }
    }
}