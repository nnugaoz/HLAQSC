using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Plan_B1_ZongBiao : AutoFiles.T6_Plan_B1_ZongBiao
    {
        public string getInsertSQL()
        {
            string lSQL = "";

            lSQL += "INSERT INTO T6_Plan_B1_ZongBiao(";
            lSQL += " ID";
            lSQL += ", PID";
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", BYJH";
            lSQL += ")VALUES(";
            lSQL += "'ZB' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Plan_B1_ZongBiao), 10)";
            lSQL += ", '" + PID + "'";
            lSQL += ", '" + ZB1 + "'";
            lSQL += ", '" + ZB2 + "'";
            lSQL += ", '" + DW + "'";
            lSQL += ", '" + BYJH + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByPID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Plan_B1_ZongBiao ";
            lSQL += " WHERE PID='" + PID + "'";

            return lSQL;
        }

        public int GetDetailByPID(ref DataTable pDT)
        {
            string lSQL = "";

            lSQL = "";
            lSQL += "SELECT ";
            lSQL += " ID";
            lSQL += ", PID";
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", BYJH";
            lSQL += " FROM T6_Plan_B1_ZongBiao ";
            lSQL += " WHERE PID='" + PID + "'";
            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);            
        }
    }
}