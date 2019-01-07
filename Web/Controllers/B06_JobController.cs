using MyTool.Model;
using MyTool.MyEnum;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B06_JobController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "JobType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Field(string ID)
        {
            T3_Job obj = new T3_Job();
            obj.ID = ID;
            obj.Job_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "FieldTypeM";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd07.dt);
            obj_selectObj.SelectType = "FieldMode";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd08.dt);
            obj_selectObj.SelectType = "FieldUnit";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}