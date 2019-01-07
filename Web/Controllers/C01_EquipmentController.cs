using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C01_EquipmentController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "EquipmentType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T3_Equipment obj = new T3_Equipment();
            obj.ID = ID;
            obj.Equipment_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "EquipmentType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Bind(string ID)
        {
            T3_Equipment obj = new T3_Equipment();
            obj.ID = ID;
            obj.Equipment_GetOne(ref _model_ret.mrd02.dt);

            T2_Org obj_org = new T2_Org();
            obj_org.Org_GetAll_ZTree(ref _model_ret.mrd06.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}