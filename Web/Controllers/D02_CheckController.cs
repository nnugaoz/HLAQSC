using MyTool.DB;
using MyTool.Log;
using MyTool.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class D02_CheckController : BaseController
    {
        //
        // GET: /D01_Plan/
        public ActionResult CheckList()
        {
            return View();
        }

        #region 下载
        [HttpGet]
        public FileResult Download(String pFileName, String pFileNameS)
        {
            String lTempFilePath = ConfigurationManager.AppSettings["Excel_Path"].ToString();
            if (!lTempFilePath.EndsWith("\\"))
            {
                lTempFilePath += "\\";
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(lTempFilePath + pFileNameS);
            string fileName = pFileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion

        #region 导入【验收】
        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    //计划上传数据库操作-事务
                    Dictionary<string, object> lSQLDic = new Dictionary<string, object>();

                    //将上传文件保存至服务器，Dictionary<key服务器本地文件名，value客户上传文件名>
                    ExcelCommon lExcelCommon = new ExcelCommon();
                    Dictionary<String, String> lFileDic = lExcelCommon.Upload(Request);

                    //获取服务器文件保存路径，读取计划文件，开始解析，并将解析过程保存数据库。
                    String lTempFilePath = ConfigurationManager.AppSettings["Excel_Path"].ToString();
                    if (!lTempFilePath.EndsWith("\\"))
                    {
                        lTempFilePath += "\\";
                    }

                    foreach (string lKey in lFileDic.Keys)
                    {
                        //服务器计划文件全路径
                        String lFileName = lTempFilePath + lKey;

                        //如果计划年月重复，记录老计划ID
                        String lOldCheckID = "";

                        //新计划ID
                        String lNewCheckID = "";

                        //插入计划文件主表T6_Plan
                        if (AddCheck(Request.Form["txtYM"].ToString(), lFileDic[lKey], lKey, ref lOldCheckID, ref lNewCheckID, ref lSQLDic))
                        {
                            DataSet lDS = lExcelCommon.ReadFile(lFileName, new string[] { "'b1-总表$'", "'b3-采掘作业$'", "'b4-掘进$'", "'b6-采矿$'", "'b7-出矿$'" });

                            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("'b1-总表$'"))
                            {
                                DataTable tempTable = lDS.Tables["'b1-总表$'"];
                                Import_ZongBiao(tempTable, lOldCheckID, lNewCheckID, ref lSQLDic);
                            }

                            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("'b3-采掘作业$'"))
                            {
                                DataTable tempTable = lDS.Tables["'b3-采掘作业$'"];
                                Import_CaiJue(tempTable, lOldCheckID, lNewCheckID, ref lSQLDic);
                            }

                            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("'b4-掘进$'"))
                            {
                                DataTable tempTable = lDS.Tables["'b4-掘进$'"];
                                Import_JueJin(tempTable, lOldCheckID, lNewCheckID, ref lSQLDic);
                            }

                            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("'b6-采矿$'"))
                            {
                                DataTable tempTable = lDS.Tables["'b6-采矿$'"];
                                Import_CaiKuang(tempTable, lOldCheckID, lNewCheckID, ref lSQLDic);
                            }

                            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("'b7-出矿$'"))
                            {
                                DataTable tempTable = lDS.Tables["'b7-出矿$'"];
                                Import_ChuKuang(tempTable, lOldCheckID, lNewCheckID, ref lSQLDic);
                            }

                            if (lSQLDic.Count > 0)
                            {
                                SqlOption.ExecuteSqlTran_sort(lSQLDic);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyTool.MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
            }
            return View("CheckList");
        }

        //在计划主表(T6_Plan)中增加一条记录
        //1、如果计划年月重复，则删除老的计划，并带出老计划主表ID
        //2、插入新计划，并带出新计划主表ID
        private Boolean AddCheck(String pYM, String pFileName, String pFileNameS, ref string pOldCheckID, ref string pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {

            Boolean lRet = true;

            T6_Check lCheck = new T6_Check();
            lCheck.ID = "";
            lCheck.YM = pYM;
            lCheck.FileName = pFileName;
            lCheck.FileNameS = pFileNameS;
            lCheck.UploadTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!lCheck.GetOldCheckID())
            {
                return false;
            }
            pOldCheckID = lCheck.ID;

            if (!lCheck.GetNewCheckID())
            {
                return false;
            }
            pNewCheckID = lCheck.ID;

            if (pOldCheckID != "")
            {
                lCheck.ID = pOldCheckID;
                pSQLDic.Add(lCheck.GetDeleteSQL(), null);
            }

            lCheck.ID = pNewCheckID;
            pSQLDic.Add(lCheck.GetInsertSQL(), null);

            return lRet;
        }
        #endregion

        #region 导入【验收b1_总表】
        private Boolean Import_ZongBiao(DataTable pDT, String pOldCheckID, String pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {
            DataTable_ClearEmptyRow(ref pDT);
            DataTable_ClearEmptyColumn(ref pDT);

            String lZB1 = "";
            String lZB1_Last = "";
            String lDW_Last = "";
            T6_Check_B1_ZongBiao lZongBiao = new T6_Check_B1_ZongBiao();

            if (pOldCheckID != "")
            {
                //删除老计划-总表数据
                lZongBiao.CID = pOldCheckID;
                pSQLDic.Add(lZongBiao.DeleteByCID(), null);
            }

            for (int lRowIndex = 1; lRowIndex < pDT.Rows.Count; lRowIndex++)
            {
                lZongBiao.CID = pNewCheckID;

                lZB1 = pDT.Rows[lRowIndex][1].ToString();
                if (lZB1.Trim() != "" && lZB1.Trim() != "其中")
                {
                    lZB1_Last = lZB1;
                }
                else
                {
                    lZB1 = lZB1_Last;
                }

                lZongBiao.ZB1 = lZB1;
                lZongBiao.ZB2 = pDT.Rows[lRowIndex][2].ToString();

                lZongBiao.DW = pDT.Rows[lRowIndex][3].ToString();
                if (lZongBiao.DW.Trim() != "")
                {
                    lDW_Last = lZongBiao.DW;
                }
                else
                {
                    lZongBiao.DW = lDW_Last;
                }
                lZongBiao.BYYS= pDT.Rows[lRowIndex][4].ToString();
                if (!IsDigitalStr(lZongBiao.BYYS))
                {
                    if (lZongBiao.BYYS.Trim() == "")
                    {
                        for (int lLastRowIndex = lRowIndex - 1; lLastRowIndex > 0; lLastRowIndex--)
                        {
                            lZongBiao.ZB2 = pDT.Rows[lLastRowIndex][4].ToString();
                            if (lZongBiao.ZB2 != "")
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        lZongBiao.ZB2 = pDT.Rows[lRowIndex][4].ToString();
                    }

                    lZongBiao.BYYS = pDT.Rows[lRowIndex][5].ToString();
                    pSQLDic.Add(new String(' ', lRowIndex) + lZongBiao.getInsertSQL(), null);

                    if (pDT.Rows[lRowIndex][6].ToString().Trim() == "")
                    {
                        for (int lLastRowIndex = lRowIndex - 1; lLastRowIndex > 0; lLastRowIndex--)
                        {
                            lZongBiao.ZB2 = pDT.Rows[lLastRowIndex][6].ToString();
                            if (lZongBiao.ZB2 != "")
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        lZongBiao.ZB2 = pDT.Rows[lRowIndex][6].ToString();
                    }

                    lZongBiao.BYYS = pDT.Rows[lRowIndex][7].ToString();
                    pSQLDic.Add(lZongBiao.getInsertSQL() + new String(' ', lRowIndex + 1), null);
                }
                else
                {
                    pSQLDic.Add(lZongBiao.getInsertSQL() + new String(' ', lRowIndex + 1), null);
                }

            }
            return true;
        }
        #endregion

        #region 导入【验收-b3_采掘作业】
        private Boolean Import_CaiJue(DataTable pDT, String pOldCheckID, String pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {
            DataTable_ClearEmptyRow(ref pDT);
            DataTable_ClearEmptyColumn(ref pDT);

            String lZB1 = "";
            String lZB1_Last = "";
            T6_Check_B3_CaiJue lCaiJue = new T6_Check_B3_CaiJue();

            if (pOldCheckID != "")
            {
                //删除老计划-采掘作业数据
                lCaiJue.CID = pOldCheckID;
                pSQLDic.Add(lCaiJue.DeleteByCID(), null);
            }


            //从DataTable第3行开始（index=2）
            for (int lRow = 2; lRow < pDT.Rows.Count; lRow++)
            {

                //指标名称
                lZB1 = pDT.Rows[lRow][0].ToString();

                if (lZB1.Trim() != "" && lZB1.Trim() != "其中")
                {
                    //指标名称
                    lCaiJue.ZB1 = lZB1;
                    lZB1_Last = lZB1;

                }
                else
                {
                    //【指标名称】是【其中】或者【空】
                    lCaiJue.ZB1 = lZB1_Last;
                }

                lCaiJue.ZB2 = pDT.Rows[lRow][1].ToString();

                lCaiJue.DW = pDT.Rows[lRow][2].ToString();

                lCaiJue.NDYS = pDT.Rows[lRow][3].ToString();

                lCaiJue.YJWC1 = pDT.Rows[lRow][4].ToString();

                lCaiJue.WCL1 = pDT.Rows[lRow][5].ToString();

                lCaiJue.YJWC2 = pDT.Rows[lRow][18].ToString();

                lCaiJue.WCL2 = pDT.Rows[lRow][19].ToString();

                lCaiJue.CID = pNewCheckID;

                pSQLDic.Add(lCaiJue.GetInsertSQL() + new String(' ', lRow), null);

            }

            return true;
        }
        #endregion

        #region 导入【验收-b4_掘进】
        private Boolean Import_JueJin(DataTable pDT, String pOldCheckID, String pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {
            DataTable_ClearEmptyRow(ref pDT);
            DataTable_ClearEmptyColumn(ref pDT);
            T6_Check_B4_JueJin lJueJin = new T6_Check_B4_JueJin();

            String lZD = "";    //中段
            String lZD_Last = "";   //上一个中段
            String lSGSJ_Last = ""; //上一个施工时间
            String lJT_Last = "";

            if (pOldCheckID != "")
            {
                //删除老计划-掘进数据
                lJueJin.CID = pOldCheckID;
                pSQLDic.Add(lJueJin.DeleteByCID(), null);
            }


            for (int lRow = 0; lRow < pDT.Rows.Count; lRow++)
            {
                //第一列   中段
                lZD = pDT.Rows[lRow][0].ToString();
                //工程名称
                lJueJin.ZYM  = pDT.Rows[lRow][2].ToString();
                //如果中段单元格包含字符m，该行数据有效
                if (lZD.Contains("m"))
                {
                    //中段
                    lJueJin.ZD = pDT.Rows[lRow][0].ToString();
                    lZD_Last = pDT.Rows[lRow][0].ToString();
                }
                else if (lZD.Trim() == "" && lZD_Last != "" && lJueJin.ZYM != "合计")
                {
                    //中段
                    lJueJin.ZD = lZD_Last;
                }
                else
                {
                    lZD_Last = "";
                    continue;
                }
                //工程类型
                lJueJin.GCLX = pDT.Rows[lRow][3].ToString();
                //台效
                lJueJin.TX = pDT.Rows[lRow][4].ToString();
                //台班
                lJueJin.TB = pDT.Rows[lRow][5].ToString();
                //规格
                lJueJin.GG = pDT.Rows[lRow][6].ToString();
                //断面积
                lJueJin.DMJ = pDT.Rows[lRow][7].ToString();
                //长度
                lJueJin.CD = pDT.Rows[lRow][8].ToString();
                //体积
                lJueJin.TJ = pDT.Rows[lRow][9].ToString();
                //掘进量
                lJueJin.JJL = pDT.Rows[lRow][10].ToString();
                //折合标米
                lJueJin.ZHBM = pDT.Rows[lRow][11].ToString();
                //副产
                lJueJin.FC = pDT.Rows[lRow][12].ToString();
                //施工时间
                lJueJin.SGSJ = pDT.Rows[lRow][21].ToString();
                if (lJueJin.SGSJ.Trim() != "")
                {
                    lSGSJ_Last = lJueJin.SGSJ;
                }
                else
                {
                    lJueJin.SGSJ = lSGSJ_Last;
                }

                //机台
                lJueJin.JT = pDT.Rows[lRow][22].ToString();
                if (lJueJin.JT.Trim() != "")
                {
                    lJT_Last = lJueJin.JT;
                }
                else
                {
                    lJueJin.JT = lJT_Last;
                }
                lJueJin.CID = pNewCheckID;
                pSQLDic.Add(lJueJin.GetInsertSQL() + new String(' ', lRow), null);
            }
            return true;
        }
        #endregion

        #region 导入【验收-b6_采矿】
        private Boolean Import_CaiKuang(DataTable pDT, String pOldCheckID, String pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {
            DataTable_ClearEmptyRow(ref pDT);
            DataTable_ClearEmptyColumn(ref pDT);

            String lZD = "";    //中段
            String lZD_Last = "";   //上一个中段
            T6_Check_B6_CaiKuang lCaiKuang = new T6_Check_B6_CaiKuang();

            if (pOldCheckID != "")
            {
                //删除老计划-采掘数据
                lCaiKuang.CID = pOldCheckID;
                pSQLDic.Add(lCaiKuang.DeleteByCID(), null);
            }

            for (int lRow = 0; lRow < pDT.Rows.Count; lRow++)
            {

                //第一列   中段
                lZD = pDT.Rows[lRow][0].ToString();
                //采场
                lCaiKuang.CC = pDT.Rows[lRow][1].ToString();

                //如果中段单元格包含字符m，该行数据有效
                if (lZD.Contains("m"))
                {
                    //中段
                    lCaiKuang.ZD = pDT.Rows[lRow][0].ToString();
                    lZD_Last = pDT.Rows[lRow][0].ToString();
                }
                else if (lZD.Trim() == "" && lZD_Last != "" && lCaiKuang.CC != "小计")
                {
                    //中段
                    lCaiKuang.ZD = lZD_Last;
                }
                else
                {
                    lZD_Last = "";
                    continue;
                }

                //采矿类型
                lCaiKuang.CKLX = pDT.Rows[lRow][2].ToString();
                //地质品质-锌
                lCaiKuang.DZPW_X = pDT.Rows[lRow][3].ToString();
                //地质品质-铁
                lCaiKuang.DZPW_T = pDT.Rows[lRow][4].ToString();
                //地质品质-铜
                lCaiKuang.DZPW_C = pDT.Rows[lRow][5].ToString();
                //地质品质-铅
                lCaiKuang.DZPW_L = pDT.Rows[lRow][6].ToString();
                //采矿量
                lCaiKuang.CKL = pDT.Rows[lRow][7].ToString();
                //填充总量
                lCaiKuang.TCZL = pDT.Rows[lRow][12].ToString();
                //尾砂量
                lCaiKuang.WSL = pDT.Rows[lRow][13].ToString();
                //胶结量
                lCaiKuang.JJL = pDT.Rows[lRow][14].ToString();
                //开始时间
                lCaiKuang.KSSJ = pDT.Rows[lRow][15].ToString();
                //结束时间
                lCaiKuang.JSSJ = pDT.Rows[lRow][16].ToString();
                //备注
                lCaiKuang.BZ = pDT.Rows[lRow][17].ToString();

                lCaiKuang.CID = pNewCheckID;
                pSQLDic.Add(lCaiKuang.GetInsertSQL() + new string(' ', lRow), null);
            }

            return true;
        }
        #endregion

        #region 导入【验收-b7_出矿】
        private Boolean Import_ChuKuang(DataTable pDT, String pOldCheckID, String pNewCheckID, ref Dictionary<String, object> pSQLDic)
        {
            DataTable_ClearEmptyRow(ref pDT);
            DataTable_ClearEmptyColumn(ref pDT);

            String lZD = "";    //中段
            String lZD_Last = "";   //上一个中段
            T6_Check_B7_ChuKuang lChuKuang = new T6_Check_B7_ChuKuang();

            if (pOldCheckID != "")
            {
                //删除老计划-出矿数据
                lChuKuang.CID = pOldCheckID;
                pSQLDic.Add(lChuKuang.DeleteByCID(), null);
            }

            for (int lRow = 0; lRow < pDT.Rows.Count; lRow++)
            {

                //第一列   中段
                lZD = pDT.Rows[lRow][0].ToString();
                //采场
                lChuKuang.CC = pDT.Rows[lRow][1].ToString();

                //如果中段单元格包含字符m，该行数据有效
                if (lZD.Contains("m"))
                {
                    //中段
                    lChuKuang.ZD = pDT.Rows[lRow][0].ToString();
                    lZD_Last = pDT.Rows[lRow][0].ToString();
                }
                else if (lZD.Trim() == "" && lZD_Last != "" && lChuKuang.CC != "小计")
                {
                    //中段
                    lChuKuang.ZD = lZD_Last;
                }
                else
                {
                    lZD_Last = "";
                    continue;
                }

                //消耗矿量
                lChuKuang.XHKL = pDT.Rows[lRow][2].ToString();
                //地质品位-锌
                lChuKuang.DZPW_X = pDT.Rows[lRow][3].ToString();
                //地质品位-铁
                lChuKuang.DZPW_T = pDT.Rows[lRow][4].ToString();
                //地质品位-铜
                lChuKuang.DZPW_C = pDT.Rows[lRow][5].ToString();
                //地质品位-铅
                lChuKuang.DZPW_L = pDT.Rows[lRow][6].ToString();
                //贫化率
                lChuKuang.PHL = pDT.Rows[lRow][11].ToString();
                //损失率
                lChuKuang.SSL = pDT.Rows[lRow][12].ToString();
                //出矿品位-锌
                lChuKuang.CKPW_X = pDT.Rows[lRow][13].ToString();
                //出矿品位-铁
                lChuKuang.CKPW_T = pDT.Rows[lRow][14].ToString();
                //出矿量
                lChuKuang.CKL = pDT.Rows[lRow][17].ToString();

                lChuKuang.CID = pNewCheckID;

                pSQLDic.Add(lChuKuang.GetInsertSQL() + new String(' ', lRow), null);

            }
            return true;
        }
        #endregion

        #region 清除DataTable空白行、列

        private Boolean DataTable_ClearEmptyRow(ref DataTable pDT)
        {
            Boolean lRet = false;
            Boolean lClear = true;
            if (pDT != null && pDT.Rows.Count > 0)
            {
                for (int i = pDT.Rows.Count - 1; i >= 0; i--)
                {
                    lClear = true;
                    for (int j = 0; j < pDT.Columns.Count; j++)
                    {
                        if (pDT.Rows[i][j].ToString() != "")
                        {
                            lClear = false;
                        }
                    }

                    if (lClear)
                    {
                        pDT.Rows.RemoveAt(i);
                    }
                }
            }
            return lRet;
        }

        private Boolean DataTable_ClearEmptyColumn(ref DataTable pDT)
        {
            Boolean lRet = false;
            Boolean lClear = true;
            if (pDT != null && pDT.Rows.Count > 0)
            {
                for (int i = pDT.Columns.Count - 1; i >= 0; i--)
                {
                    lClear = true;
                    for (int j = 0; j < pDT.Rows.Count; j++)
                    {
                        if (pDT.Rows[j][i].ToString() != "")
                        {
                            lClear = false;
                        }
                    }

                    if (lClear)
                    {
                        pDT.Columns.RemoveAt(i);
                    }
                }
            }
            return lRet;
        }

        #endregion

        #region 判断字符串是否是数字字符串
        public Boolean IsDigitalStr(String pStr)
        {
            Regex lDigitalStrReg = new Regex("^[0-9.]+$");
            return lDigitalStrReg.IsMatch(pStr);
        }
        #endregion

        #region 验收详情
        public ActionResult Detail(String pID)
        {
            ViewBag.CID = pID;
            return View();
        }
        #endregion
    }
}
