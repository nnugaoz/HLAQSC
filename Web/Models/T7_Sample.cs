using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T7_Sample : AutoFiles.T7_Sample
    {
        public int GetList(ref DataTable pDT)
        {
            String lSQL = "";
            lSQL += "SELECT ";
            lSQL += " ID";
            lSQL += ", Code";
            lSQL += ", STime";
            lSQL += ", PID";
            lSQL += ", Sampler";
            lSQL += ", Memo";
            lSQL += ", Result";
            lSQL += ", RTime";
            lSQL += ", Analyst";
            lSQL += " FROM T7_Sample";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL ,ref pDT);

        }
    }
}