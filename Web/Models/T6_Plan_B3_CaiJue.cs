using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Plan_B3_CaiJue:AutoFiles.T6_Plan_B3_CaiJue
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Plan_B3_CaiJue( ";
            lSQL += " ID";
            lSQL += ", PID";
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", NDJH";
            lSQL += ", YJWC1";
            lSQL += ", WCL1";
            lSQL += ", BYJH";
            lSQL += ", YJWC2";
            lSQL += ", WCL2";
            lSQL += ")VALUES(";
            lSQL += " 'CJ' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Plan_B3_CaiJue), 10)";
            lSQL += ", '" + PID + "'";
            lSQL += ", '" + ZB1 + "'";
            lSQL += ", '" + ZB2 + "'";
            lSQL += ", '" + DW + "'";
            lSQL += ", '" + NDJH + "'";
            lSQL += ", '" + YJWC1 + "'";
            lSQL += ", '" + WCL1 + "'";
            lSQL += ", '" + BYJH + "'";
            lSQL += ", '" + YJWC2 + "'";
            lSQL += ", '" + WCL2 + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByPID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Plan_B3_CaiJue ";
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
            lSQL += ", NDJH";
            lSQL += ", YJWC1";
            lSQL += ", WCL1";
            lSQL += ", BYJH";
            lSQL += ", YJWC2";
            lSQL += ", WCL2";
            lSQL += " FROM T6_Plan_B3_CaiJue ";
            lSQL += " WHERE PID='" + PID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);    
        }
    }
}