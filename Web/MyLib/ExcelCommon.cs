using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.MyLib
{
    public class ExcelCommon
    {
        public DataSet ReadFile(String lFilePath, String[] pSheetNames)
        {

            OleDbConnection OledbConn;
            OleDbCommand OledbCmd;
            OleDbDataAdapter OleDataAdpt = new OleDbDataAdapter();
            DataSet lRetDS = new DataSet();
            string connString = "";

            if (lFilePath.EndsWith(".xls") || lFilePath.EndsWith(".xlsx"))
            {
                connString = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + lFilePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;';";
            }
            else
            {
                return null;
            }
            OledbConn = new OleDbConnection(connString);

            DataTable schemaTable = new DataTable();
            OledbCmd = new OleDbCommand();
            OledbCmd.Connection = OledbConn;
            OledbConn.Open();

            //Get the name of First Sheet
            DataTable dtExcelSchema;
            dtExcelSchema = OledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
            {
                string SheetName = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                for (int j = 0; j < pSheetNames.Length; j++)
                {
                    if (pSheetNames[j] == SheetName)
                    {
                        DataTable ContentTable = new DataTable(pSheetNames[j]);
                        //Read Data from First Sheet
                        OledbCmd.CommandText = "SELECT * From [" + SheetName + "]";
                        OleDataAdpt.SelectCommand = OledbCmd;
                        OleDataAdpt.Fill(ContentTable);
                        lRetDS.Tables.Add(ContentTable);
                    }
                }
            }

            OledbConn.Close();

            return lRetDS;

        }

        public String ExportToExcel(String[] pColumnsName, DataTable pDT)
        {
            String lFilePath = "";
            lFilePath = ConfigurationManager.AppSettings["Excel_Path"].ToString();
            if (!lFilePath.EndsWith("\\"))
            {
                lFilePath += lFilePath + "\\";
            }
            lFilePath += Guid.NewGuid().ToString() + ".xlsx";
            Microsoft.Office.Interop.Excel.Application lExcelApplication = null;
            Microsoft.Office.Interop.Excel.Workbook lWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet lWorkSheet = null;
            Microsoft.Office.Interop.Excel.Range lRange = null;

            try
            {
                lExcelApplication = new Microsoft.Office.Interop.Excel.Application();
                //lExcelApplication.Visible = true;

                lWorkBook = lExcelApplication.Workbooks.Add(Missing.Value);
                lWorkSheet = lWorkBook.ActiveSheet;

                for (int i = 1; i <= pColumnsName.Length; i++)
                {
                    lWorkSheet.Cells[1, i] = pColumnsName[i - 1];
                }

                //Format A1:D1 as bold, vertical alignment=center.
                lWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, 1]
                    , (Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, pColumnsName.Length]).Font.Bold = true;
                lWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, 1]
                    , (Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, pColumnsName.Length]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //Fill A2:B6 with an array of values(First and Last Name)
                object[,] arr = new object[pDT.Rows.Count, pDT.Columns.Count];
                for (int r = 0; r < pDT.Rows.Count; r++)
                {
                    DataRow dr = pDT.Rows[r];
                    for (int c = 0; c < pDT.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                }
                lWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[2, 1]
    , (Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[pDT.Rows.Count + 1, pColumnsName.Length]).Value2 = arr;


                //Fill C2:C6 with a relative formula(A2 + " " + B2).
                //lRange = lWorkSheet.get_Range("C2", "C6");
                //lRange.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                //lRange = lWorkSheet.get_Range("D2", "D6");
                //lRange.Formula = "=RAND()*100000";
                //lRange.NumberFormat = "$0.00";

                //AutoFit columns A:D
                lRange = lWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, 1]
                    , (Microsoft.Office.Interop.Excel.Range)lWorkSheet.Cells[1, pColumnsName.Length]);
                lRange.EntireColumn.AutoFit();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                //lExcelApplication.Visible = true;
                //lExcelApplication.UserControl = true;

                lWorkBook.SaveAs(lFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (lWorkBook != null)
                {
                    lWorkBook.Close(true);
                }

                if (lExcelApplication != null)
                {
                    lExcelApplication.Quit();

                }
                releaseObject(lWorkSheet);
                releaseObject(lWorkBook);
                releaseObject(lExcelApplication);
            }
            return lFilePath;
        }

        private void releaseObject(Object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        public Dictionary<string, string> Upload(HttpRequestBase pRequest)
        {
            Dictionary<string, string> lFilesDic = new Dictionary<string, string>();
            String lTempFilePath = ConfigurationManager.AppSettings["Excel_Path"].ToString();
            if (!lTempFilePath.EndsWith("\\"))
            {
                lTempFilePath += "\\";
            }

            if (pRequest.Files.Count > 0)
            {
                for (int i = 0; i < pRequest.Files.Count; i++)
                {
                    String lFileExt = GetFileExtension(pRequest.Files[i].FileName);
                    String lFileName = Guid.NewGuid().ToString() + lFileExt;

                    pRequest.Files[i].SaveAs(lTempFilePath + lFileName);
                    lFilesDic.Add(lFileName, pRequest.Files[i].FileName);
                }
            }
            return lFilesDic;
        }

        private string GetFileExtension(string pFileName)
        {
            String lExtension = "";

            if (pFileName.LastIndexOf(".") > 0)
            {
                lExtension = pFileName.Substring(pFileName.LastIndexOf("."));
            }

            return lExtension;
        }
    }
}