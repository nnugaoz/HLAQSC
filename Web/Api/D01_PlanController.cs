using MyTool.Model;
using MyTool.MyClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;
using Web.MyLib;

namespace Web.Api
{
    public class D01_PlanController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string PlanList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T6_Plan lPlan = new T6_Plan();
            lPlan.pageList = pageList;

            DataTable lDT = new DataTable();

            _model_ret.ret_status = lPlan.GetPlanList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        #region 判断年月计划是否存在
        [HttpGet]
        public string CheckPlanExists(String p_YM)
        {
            T6_Plan lPlan = new T6_Plan();
            lPlan.YM = p_YM;

            _model_ret.ret_status = lPlan.CheckPlanExists();
            return _model_ret.Get_Ret();
        }
        #endregion

        #region 根据计划ID，获取计划详情
        [HttpGet]
        public string GetPlanDetail(String para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T6_Plan_B1_ZongBiao lZongBiao = new T6_Plan_B1_ZongBiao();
            lZongBiao.PID = pageList.Para1;
            T6_Plan_B3_CaiJue lCaiJue = new T6_Plan_B3_CaiJue();
            lCaiJue.PID = pageList.Para1;
            T6_Plan_B4_JueJin lJueJin = new T6_Plan_B4_JueJin();
            lJueJin.PID = pageList.Para1;
            T6_Plan_B6_CaiKuang lCaiKuang = new T6_Plan_B6_CaiKuang();
            lCaiKuang.PID = pageList.Para1;
            T6_Plan_B7_ChuKuang lChuKuang = new T6_Plan_B7_ChuKuang();
            lChuKuang.PID = pageList.Para1;

            _model_ret.mrd01.ret_status = lZongBiao.GetDetailByPID(ref _model_ret.mrd01.dt);
            _model_ret.mrd02.ret_status = lCaiJue.GetDetailByPID(ref _model_ret.mrd02.dt);
            _model_ret.mrd03.ret_status = lJueJin.GetDetailByPID(ref _model_ret.mrd03.dt);
            _model_ret.mrd04.ret_status = lCaiKuang.GetDetailByPID(ref _model_ret.mrd04.dt);
            _model_ret.mrd05.ret_status = lChuKuang.GetDetailByPID(ref _model_ret.mrd05.dt);

            return _model_ret.Get_Ret();
        }
        #endregion
    }
}
