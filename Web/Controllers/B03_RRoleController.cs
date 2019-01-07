using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B03_RRoleController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            T2_RRole obj = new T2_RRole();
            obj.RRole_GetAll_ZTree(ref _model_ret.mrd01.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "RRoleType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_RRole obj = new T2_RRole();
            obj.ID = ID;
            obj.RRole_GetOne(ref _model_ret.mrd02.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "RRoleType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Bind(string ID)
        {
            T2_RRole obj = new T2_RRole();
            obj.ID = ID;
            obj.RRole_GetOne(ref _model_ret.mrd02.dt);
            obj.RRole_GetAll_ZTree(ref _model_ret.mrd05.dt);

            T2_Org obj_org = new T2_Org();
            obj_org.Org_GetAll_ZTree(ref _model_ret.mrd06.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "JobType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}