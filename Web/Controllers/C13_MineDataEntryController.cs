using MyTool.DB;
using MyTool.Model;
using MyTool.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class C13_MineDataEntryController : BaseController
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
            obj_select.Common_GetAll(ref _model_ret.mrd01.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T8_WR lWR = new T8_WR();
            T8_WR_Position_Data1 lPosition_Data1 = new T8_WR_Position_Data1();
            T8_WR_Position_Data2_D lPosition_Data2_D = new T8_WR_Position_Data2_D();
            T8_WR_Equipment_D lEquipment_D = new T8_WR_Equipment_D();

            lWR.ID = ID;
            lWR.Get_ByID(ref _model_ret.mrd01.dt);

            lPosition_Data1.WRID = ID;
            lPosition_Data1.Get_ByWRID(ref _model_ret.mrd02.dt);

            lPosition_Data2_D.WRID = ID;
            lPosition_Data2_D.Get_ByWRID(ref _model_ret.mrd03.dt);

            lEquipment_D.WRID = ID;
            lEquipment_D.Get_ByWRID(ref _model_ret.mrd04.dt);

            SelectOption obj_select = new SelectOption();
            obj_select.SelectType = "ClassType";
            obj_select.Common_GetAll(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();

            return View();
        }

        public ActionResult BBJC_Add()
        {
            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Add";
            return View();
        }

        public ActionResult BBJC_Edit(String ID, String PositionName, String PositionCode, String Length)
        {

            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Edit";
            ViewBag.ID = ID;
            ViewBag.PositionName = PositionName;
            ViewBag.PositionCode = PositionCode;
            ViewBag.Length = Length;
            return View("BBJC_Add");
        }

        public ActionResult ZYL_Add()
        {
            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Add";
            return View();
        }

        public ActionResult ZYL_Edit(String ID, String PositionName, String PositionCode, String Weight)
        {

            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);
            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Edit";
            ViewBag.ID = ID;
            ViewBag.PositionName = PositionName;
            ViewBag.PositionCode = PositionCode;
            ViewBag.Weight = Weight;
            return View("ZYL_Add");
        }

        public ActionResult GK_Add()
        {
            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd02.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd03.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.MDE_GetDFType_ByF(ref _model_ret.mrd04.dt);
            obj_df.MDE_GetDFUnit_ByF(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Add";
            return View();
        }

        public ActionResult GK_Edit(String ID, String PositionName, String PositionCode, String EquipmentID, String CarCnt, String MineTypeCode, String CarTypeName, String Weight)
        {

            T2_Position obj_position = new T2_Position();
            obj_position.MDE_GetAll_ZTree(ref _model_ret.mrd01.dt);

            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd02.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd03.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.MDE_GetDFType_ByF(ref _model_ret.mrd04.dt);
            obj_df.MDE_GetDFUnit_ByF(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Edit";
            ViewBag.ID = ID;
            ViewBag.PositionName = PositionName;
            ViewBag.PositionCode = PositionCode;
            ViewBag.EquipmentID = EquipmentID;
            ViewBag.CarCnt = CarCnt;
            ViewBag.MineTypeCode = MineTypeCode;
            ViewBag.CarTypeName = CarTypeName;
            ViewBag.Weight = Weight;
            return View("GK_Add");
        }

        public ActionResult FK_Add()
        {
            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd02.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd03.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.MDE_GetDFType_ByF(ref _model_ret.mrd04.dt);
            obj_df.MDE_GetDFUnit_ByF(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Add";
            return View();
        }

        public ActionResult FK_Edit(String ID, String EquipmentID, String CarCnt, String MineTypeCode, String CarTypeName, String Weight)
        {
            T3_Equipment obj_equi = new T3_Equipment();
            obj_equi.MDE_GetAllList(ref _model_ret.mrd02.dt);
            obj_equi.MDE_GetAllList_Position(ref _model_ret.mrd03.dt);

            T3_Dynamic_Field obj_df = new T3_Dynamic_Field();
            obj_df.MDE_GetDFType_ByF(ref _model_ret.mrd04.dt);
            obj_df.MDE_GetDFUnit_ByF(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            ViewBag.Type = "Edit";
            ViewBag.ID = ID;
            ViewBag.EquipmentID = EquipmentID;
            ViewBag.CarCnt = CarCnt;
            ViewBag.MineTypeCode = MineTypeCode;
            ViewBag.CarTypeName = CarTypeName;
            ViewBag.Weight = Weight;
            return View("FK_Add");
        }

        public String Save(T8_WR lWR)
        {
            String lResult = "";
            String lWRID = "";
            String lSQL = "";
            String lSQL_Temp = "";

            if (lWR.ID != "")
            {
                lWR.Delete(ref lSQL_Temp);
                lSQL += lSQL_Temp;
            }
            lWRID = Guid.NewGuid().ToString();
            lWR.ID = lWRID;

            if (lWR.WR_Position_Data1_List != null)
            {
                for (int i = 0; i < lWR.WR_Position_Data1_List.Count; i++)
                {
                    lWR.WR_Position_Data1_List[i].ID = Guid.NewGuid().ToString();
                    lWR.WR_Position_Data1_List[i].WRID = lWRID;
                    lSQL_Temp = "";
                    lWR.WR_Position_Data1_List[i].Insert(ref lSQL_Temp);
                    lSQL += lSQL_Temp + ";";
                }
            }

            if (lWR.WR_Position_Data2_D_List != null)
            {
                for (int i = 0; i < lWR.WR_Position_Data2_D_List.Count; i++)
                {
                    lWR.WR_Position_Data2_D_List[i].ID = Guid.NewGuid().ToString();
                    lWR.WR_Position_Data2_D_List[i].WRID = lWRID;
                    lSQL_Temp = "";
                    lWR.WR_Position_Data2_D_List[i].Insert(ref lSQL_Temp);
                    lSQL += lSQL_Temp + ";";
                }
            }

            if (lWR.WR_Equipment_D_List != null)
            {
                for (int i = 0; i < lWR.WR_Equipment_D_List.Count; i++)
                {
                    lWR.WR_Equipment_D_List[i].ID = Guid.NewGuid().ToString();
                    lWR.WR_Equipment_D_List[i].WRID = lWRID;
                    lSQL_Temp = "";
                    lWR.WR_Equipment_D_List[i].Insert(ref lSQL_Temp);
                    lSQL += lSQL_Temp + ";";
                }
            }

            lSQL_Temp = "";
            lWR.Insert(ref lSQL_Temp);
            lSQL += lSQL_Temp + ";";

            if (DataTool.Update(lSQL))
            {
                lResult = "1";
            }
            else
            {
                lResult = "0";
            }
            return lResult;
        }
    }
}