using MyTool.Model;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C91_MessageBoardController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Position_GetAll_ZTree(ref _model_ret.mrd01.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Detail(string ID)
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Position_GetAll_ZTree(ref _model_ret.mrd01.dt);

            T5_MessageBoard obj_mb = new T5_MessageBoard();
            obj_mb.ID = ID;
            obj_mb.GG_GetOne(ref _model_ret.mrd02.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Position_GetAll_ZTree(ref _model_ret.mrd01.dt);

            T5_MessageBoard obj_mb = new T5_MessageBoard();
            obj_mb.ID = ID;
            obj_mb.GG_GetOne(ref _model_ret.mrd02.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}