using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Plan_B7_ChuKuang:AutoFiles.T6_Plan_B7_ChuKuang
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Plan_B7_ChuKuang( ";
            lSQL += " ID";
            lSQL += ", PID";
            lSQL += ", ZD";
            lSQL += ", CC";
            lSQL += ", XHKL";
            lSQL += ", DZPW_X";
            lSQL += ", DZPW_T";
            lSQL += ", DZPW_C";
            lSQL += ", DZPW_L";
            lSQL += ", PHL";
            lSQL += ", SSL";
            lSQL += ", CKPW_X";
            lSQL += ", CKPW_T";
            lSQL += ", CKL";
            lSQL += ")VALUES(";
            lSQL += " 'CK' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Plan_B7_ChuKuang), 10)";
            lSQL += ", '" + PID + "'";
            lSQL += ", '" + ZD + "'";
            lSQL += ", '" + CC + "'";
            lSQL += ", '" + XHKL + "'";
            lSQL += ", '" + DZPW_X + "'";
            lSQL += ", '" + DZPW_T + "'";
            lSQL += ", '" + DZPW_C + "'";
            lSQL += ", '" + DZPW_L + "'";
            lSQL += ", '" + PHL + "'";
            lSQL += ", '" + SSL + "'";
            lSQL += ", '" + CKPW_X + "'";
            lSQL += ", '" + CKPW_T + "'";
            lSQL += ", '" + CKL + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByPID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Plan_B7_ChuKuang ";
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
            lSQL += ", ZD";
            lSQL += ", CC";
            lSQL += ", XHKL";
            lSQL += ", DZPW_X";
            lSQL += ", DZPW_T";
            lSQL += ", DZPW_C";
            lSQL += ", DZPW_L";
            lSQL += ", PHL";
            lSQL += ", SSL";
            lSQL += ", CKPW_X";
            lSQL += ", CKPW_T";
            lSQL += ", CKL";
            lSQL += " FROM T6_Plan_B7_ChuKuang ";
            lSQL += " WHERE PID='" + PID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);    
        }
    }
}