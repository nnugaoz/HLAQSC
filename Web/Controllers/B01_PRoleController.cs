using MyTool.Model;
using MyTool.MyEnum;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B01_PRoleController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            T1_Page obj = new T1_Page();
            obj.PRole_GetAll_ZTree_Add(ref _model_ret.mrd01.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_PRole obj = new T2_PRole();
            obj.ID = ID;

            if (obj.PRole_GetOne(ref _model_ret.mrd02.dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                T1_Page obj_page = new T1_Page();
                obj_page.PRole_GetAll_ZTree_Edit(ref _model_ret.mrd01.dt, _model_ret.mrd02.dt.Rows[0]["ID"].ToString());
            }
            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}