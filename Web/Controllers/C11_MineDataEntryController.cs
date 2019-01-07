using MyTool.Model;
using MyTool.MyEnum;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C11_MineDataEntryController : BaseController
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

            T1_User obj_user = new T1_User();
            obj_user.ID = ViewBag.UserID;
            if (obj_user.MDE_GetOne(ref _model_ret.mrd04.dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
                obj_df.Type2 = _model_ret.mrd04.dt.Rows[0]["JobCode"].ToString();
                obj_df.MDE_GetOne(ref _model_ret.mrd07.dt);
                obj_df.MDE_GetDFType(ref _model_ret.mrd08.dt);
                obj_df.MDE_GetDFUnit(ref _model_ret.mrd09.dt);
            }

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);

            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd06.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd10.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd11.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T5_WorkRecord obj = new T5_WorkRecord();
            obj.ID = ID;
            if (obj.WR_GetOne_ByID(ref _model_ret.mrd01.dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                T1_User obj_user = new T1_User();
                obj_user.ID = _model_ret.mrd01.dt.Rows[0]["WorkManID"].ToString();
                if (obj_user.MDE_GetOne(ref _model_ret.mrd04.dt) == (int)MyEnum.Enum_Ret.Succes)
                {
                    T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
                    obj_df.Type2 = _model_ret.mrd04.dt.Rows[0]["JobCode"].ToString();
                    obj_df.MDE_GetOne(ref _model_ret.mrd07.dt);
                    obj_df.MDE_GetDFType(ref _model_ret.mrd08.dt);
                    obj_df.MDE_GetDFUnit(ref _model_ret.mrd09.dt);
                }
            }
            obj.WR_GetDetail_ByID(ref _model_ret.mrd02.dt);
            obj.WR_GetDetailDF_ByID(ref _model_ret.mrd03.dt);

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);

            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd06.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd10.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd11.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult RSubmit(string ID)
        {
            T5_WorkRecord obj = new T5_WorkRecord();
            obj.ID = ID;
            if (obj.WR_GetOne_ByID(ref _model_ret.mrd01.dt) == (int)MyEnum.Enum_Ret.Succes)
            {
                T1_User obj_user = new T1_User();
                obj_user.ID = _model_ret.mrd01.dt.Rows[0]["WorkManID"].ToString();
                obj_user.MDE_GetOne(ref _model_ret.mrd04.dt);
            }
            obj.WR_GetDetail_ByID(ref _model_ret.mrd02.dt);
            obj.WR_GetDetailDF_ByID(ref _model_ret.mrd03.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }
    }
}