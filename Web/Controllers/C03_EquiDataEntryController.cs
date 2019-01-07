using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C03_EquiDataEntryController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);
            obj_select.SelectType = "WorkRecordStatus";
            obj_select.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Add()
        {
            if (ViewBag.UserID == MyPara.AdminID)
            {
                ViewBag.HaveRight = "0";
            }
            else
            {
                ViewBag.HaveRight = "1";
            }

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.EDE_GetAllList(ref _model_ret.mrd06.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.EDE_GetAllList(ref _model_ret.mrd07.dt);
            obj_df.EDE_GetDFType(ref _model_ret.mrd08.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T5_WorkRecord obj = new T5_WorkRecord();
            obj.ID = ID;
            obj.WR_GetOne_ByID(ref _model_ret.mrd01.dt);
            obj.WR_GetDetail_ByID(ref _model_ret.mrd02.dt);
            obj.WR_GetDetailDF_ByID(ref _model_ret.mrd03.dt);

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.EDE_GetAllList(ref _model_ret.mrd06.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.EDE_GetAllList(ref _model_ret.mrd07.dt);
            obj_df.EDE_GetDFType(ref _model_ret.mrd08.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult RSubmit(string ID)
        {
            T5_WorkRecord obj = new T5_WorkRecord();
            obj.ID = ID;
            obj.WR_GetOne_ByID(ref _model_ret.mrd01.dt);
            obj.WR_GetDetail_ByID(ref _model_ret.mrd02.dt);
            obj.WR_GetDetailDF_ByID(ref _model_ret.mrd03.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}