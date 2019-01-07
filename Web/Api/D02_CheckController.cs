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
    public class D02_CheckController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        [HttpGet]
        public string CheckList(string para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T6_Check lCheck = new T6_Check();
            lCheck.pageList = pageList;

            DataTable lDT = new DataTable();

            _model_ret.ret_status = lCheck.GetCheckList(ref _model_ret.mrd01.dt);
            return _model_ret.Get_Ret();
        }

        #region 判断年月计划是否存在
        [HttpGet]
        public string CheckCheckExists(String p_YM)
        {
            T6_Check lCheck = new T6_Check();
            lCheck.YM = p_YM;

            _model_ret.ret_status = lCheck.CheckCheckExists();
            return _model_ret.Get_Ret();
        }
        #endregion

        #region 根据验收ID，获取验收详情
        [HttpGet]
        public string GetCheckDetail(String para)
        {
            para = HttpUtility.UrlDecode(HttpUtility.UrlDecode(para, Encoding.UTF8), Encoding.UTF8);

            PageList pageList = new PageList();
            MyClass<PageList> myClass = new MyClass<PageList>(ref pageList, para);

            T6_Check_B1_ZongBiao lZongBiao = new T6_Check_B1_ZongBiao();
            lZongBiao.CID = pageList.Para1;
            T6_Check_B3_CaiJue lCaiJue = new T6_Check_B3_CaiJue();
            lCaiJue.CID = pageList.Para1;
            T6_Check_B4_JueJin lJueJin = new T6_Check_B4_JueJin();
            lJueJin.CID = pageList.Para1;
            T6_Check_B6_CaiKuang lCaiKuang = new T6_Check_B6_CaiKuang();
            lCaiKuang.CID = pageList.Para1;
            T6_Check_B7_ChuKuang lChuKuang = new T6_Check_B7_ChuKuang();
            lChuKuang.CID = pageList.Para1;

            _model_ret.mrd01.ret_status = lZongBiao.GetDetailByCID(ref _model_ret.mrd01.dt);
            _model_ret.mrd02.ret_status = lCaiJue.GetDetailByCID(ref _model_ret.mrd02.dt);
            _model_ret.mrd03.ret_status = lJueJin.GetDetailByCID(ref _model_ret.mrd03.dt);
            _model_ret.mrd04.ret_status = lCaiKuang.GetDetailByCID(ref _model_ret.mrd04.dt);
            _model_ret.mrd05.ret_status = lChuKuang.GetDetailByCID(ref _model_ret.mrd05.dt);

            return _model_ret.Get_Ret();
        }
        #endregion
    }
}
