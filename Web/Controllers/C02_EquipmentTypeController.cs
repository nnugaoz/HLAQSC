using MyTool.Model;
using MyTool.MyEnum;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C02_EquipmentTypeController : BaseController
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
            T3_EquipmentType obj = new T3_EquipmentType();
            obj.ID = ID;
            obj.ET_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "EquipmentType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Field(string ID)
        {
            T3_EquipmentType obj = new T3_EquipmentType();
            obj.ID = ID;
            obj.ET_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "FieldTypeE";
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