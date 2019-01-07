using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Plan_B4_JueJin : AutoFiles.T6_Plan_B4_JueJin
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Plan_B4_JueJin( ";
            lSQL += " ID";
            lSQL += ", PID";
            lSQL += ", ZD";
            lSQL += ", CC";
            lSQL += ", ZYM";
            lSQL += ", GCLX";
            lSQL += ", TX";
            lSQL += ", TB";
            lSQL += ", GG";
            lSQL += ", DMJ";
            lSQL += ", CD";
            lSQL += ", TJ";
            lSQL += ", JJL";
            lSQL += ", ZHBM";
            lSQL += ", FC";
            lSQL += ", SGSJ";
            lSQL += ", JT";
            lSQL += ")VALUES(";
            lSQL += " 'JJ' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Plan_B4_JueJin), 10)";
            lSQL += ", '" + PID + "'";
            lSQL += ", '" + ZD + "'";
            lSQL += ", '" + CC + "'";
            lSQL += ", '" + ZYM + "'";
            lSQL += ", '" + GCLX + "'";
            lSQL += ", '" + TX + "'";
            lSQL += ", '" + TB + "'";
            lSQL += ", '" + GG + "'";
            lSQL += ", '" + DMJ + "'";
            lSQL += ", '" + CD + "'";
            lSQL += ", '" + TJ + "'";
            lSQL += ", '" + JJL + "'";
            lSQL += ", '" + ZHBM + "'";
            lSQL += ", '" + FC + "'";
            lSQL += ", '" + SGSJ + "'";
            lSQL += ", '" + JT + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByPID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Plan_B4_JueJin ";
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
            lSQL += ", ZYM";
            lSQL += ", GCLX";
            lSQL += ", TX";
            lSQL += ", TB";
            lSQL += ", GG";
            lSQL += ", DMJ";
            lSQL += ", CD";
            lSQL += ", TJ";
            lSQL += ", JJL";
            lSQL += ", ZHBM";
            lSQL += ", FC";
            lSQL += ", SGSJ";
            lSQL += ", JT";
            lSQL += " FROM T6_Plan_B4_JueJin ";
            lSQL += " WHERE PID='" + PID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);    
        }
    }
}