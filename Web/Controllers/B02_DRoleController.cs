using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B02_DRoleController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "DRoleType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_DRole obj = new T2_DRole();
            obj.ID = ID;
            obj.DRole_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "DRoleType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}