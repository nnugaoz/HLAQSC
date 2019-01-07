using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B04_OrgController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            T2_Org obj = new T2_Org();
            obj.Org_GetAll_ZTree(ref _model_ret.mrd01.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "OrgType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_Org obj = new T2_Org();
            obj.ID = ID;
            obj.Org_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "OrgType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}