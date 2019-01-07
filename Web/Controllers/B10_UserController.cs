using MyTool.DB;
using MyTool.Log;
using MyTool.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B10_UserController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        // GET: B10_User
        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            SelectOption lSelectOption = new SelectOption();
            lSelectOption.SelectType = "JobType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd01.dt);

            T2_Org lTOrg = new T2_Org();
            lTOrg.Org_GetAll_ZTree(ref _model_ret.mrd02.dt);

            lSelectOption.SelectType = "DRoleType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd03.dt);

            T2_RRole lRRole = new T2_RRole();
            lRRole.RRole_GetAll_ZTree(ref _model_ret.mrd04.dt);

            lSelectOption.SelectType = "PRoleID";
            lSelectOption.Common_GetAll(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();

            return View();
        }

        public ActionResult Edit(String ID)
        {
            T1_User obj = new T1_User();
            obj.ID = ID;
            obj.User_GetOne(ref _model_ret.mrd01.dt);

            SelectOption lSelectOption = new SelectOption();
            lSelectOption.SelectType = "JobType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd02.dt);

            T2_Org lTOrg = new T2_Org();
            lTOrg.Org_GetAll_ZTree(ref _model_ret.mrd03.dt);

            lSelectOption.SelectType = "DRoleType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd04.dt);

            T2_RRole lRRole = new T2_RRole();
            lRRole.RRole_GetAll_ZTree(ref _model_ret.mrd05.dt);

            lSelectOption.SelectType = "PRoleID";
            lSelectOption.Common_GetAll(ref _model_ret.mrd06.dt);

            ViewBag.Ret = _model_ret.Get_Ret();

            return View();
        }

        public FileResult ExportUser(string para)
        {
            try
            {
                T1_User lUser = new T1_User();

                lUser.User_ExportUsers(ref _model_ret.mrd01.dt);

                ExcelCommon lExcelCommon = new ExcelCommon();

                String[] lColNames = new String[] { "姓名", "登录名", "组织机构名称", "工种", "用户唯一标识" };

                String lFilePath = lExcelCommon.ExportToExcel(lColNames, _model_ret.mrd01.dt);

                byte[] fileBytes = System.IO.File.ReadAllBytes(lFilePath);
                string fileName = "UserList.xlsx";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyTool.MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return null;
            }
        }

        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                String lSQL = "";
                ArrayList lSQLList = new ArrayList();

                ExcelCommon lExcelCommon = new ExcelCommon();
                Dictionary<String, String> lFiles = lExcelCommon.Upload(Request);

                String lTempFilePath = ConfigurationManager.AppSettings["Excel_Path"].ToString();
                if (!lTempFilePath.EndsWith("\\"))
                {
                    lTempFilePath += "\\";
                }

                if (lFiles.Count > 0)
                {
                    foreach (string key in lFiles.Keys)
                    {
                        String lFileName = lTempFilePath + key;


                        DataSet lDS = lExcelCommon.ReadFile(lFileName, new string[] { "Sheet1$" });

                        if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables.Contains("Sheet1$"))
                        {
                            DataTable tempTable = lDS.Tables["Sheet1$"];

                            T2_Org lOrg = new T2_Org();
                            T3_Job lJob = new T3_Job();

                            T2_PRole lPRole = new T2_PRole();
                            String lDefaultPRoleID = "";

                            T2_DRole lDRole = new T2_DRole();
                            String lDefaultDRoleType = "";

                            T2_RRole lRRole = new T2_RRole();
                            String lDefaultRRoleCode = "";

                            lPRole.Title = ConfigurationManager.AppSettings["Excel_Default_PRole_Title"].ToString();
                            lDRole.Title = ConfigurationManager.AppSettings["Excel_Default_DRole_Title"].ToString();
                            lRRole.Title = ConfigurationManager.AppSettings["Excel_Default_RRole_Title"].ToString();

                            lDefaultPRoleID = lPRole.PRole_GetIDByTitle();
                            lDefaultDRoleType = lDRole.DRole_GetTypeByTitle();
                            lDefaultRRoleCode = lRRole.RRole_GetCodeByTitle();

                            if (tempTable != null && tempTable.Rows.Count > 0)
                            {
                                T1_User_Excel lUserExcel = new T1_User_Excel();
                                lSQLList.Add("DELETE FROM T1_User_Excel");
                                for (int i = 0; i < tempTable.Rows.Count; i++)
                                {
                                    lOrg.Title = tempTable.Rows[i][2].ToString();
                                    lJob.Title = tempTable.Rows[i][3].ToString();

                                    lUserExcel.Name = tempTable.Rows[i][0].ToString();
                                    lUserExcel.LoginName = tempTable.Rows[i][1].ToString();
                                    lUserExcel.OrgCode = lOrg.Org_GetCodeByTitle();
                                    lUserExcel.PRoleID = lDefaultPRoleID;
                                    lUserExcel.DRoleType = lDefaultDRoleType;
                                    lUserExcel.RRoleCode = lDefaultRRoleCode;
                                    lUserExcel.JobCode = lJob.Job_GetCodeByTitle();
                                    lUserExcel.UserKey = tempTable.Rows[i][4].ToString();

                                    lSQL = lUserExcel.GetInsertSQL();

                                    lSQLList.Add(lSQL);
                                }

                                SqlOption.ExecuteSqlTran(lSQLList);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyTool.MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
            }

            return View("PageList_Excel");
        }



        public ActionResult PageList_Excel()
        {
            return View();
        }

        public ActionResult Edit_Excel(String ID)
        {
            T1_User_Excel obj = new T1_User_Excel();
            obj.ID = ID;
            obj.User_GetOne(ref _model_ret.mrd01.dt);

            SelectOption lSelectOption = new SelectOption();
            lSelectOption.SelectType = "JobType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd02.dt);

            T2_Org lTOrg = new T2_Org();
            lTOrg.Org_GetAll_ZTree(ref _model_ret.mrd03.dt);

            lSelectOption.SelectType = "DRoleType";
            lSelectOption.Common_GetAll(ref _model_ret.mrd04.dt);

            T2_RRole lRRole = new T2_RRole();
            lRRole.RRole_GetAll_ZTree(ref _model_ret.mrd05.dt);

            ViewBag.Ret = _model_ret.Get_Ret();

            return View();
        }
    }
}