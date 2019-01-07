using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Check_B4_JueJin : AutoFiles.T6_Check_B4_JueJin
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Check_B4_JueJin( ";
            lSQL += " ID";
            lSQL += ", CID";
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
            lSQL += " 'JJ' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Check_B4_JueJin), 10)";
            lSQL += ", '" + CID + "'";
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

        public string DeleteByCID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Check_B4_JueJin ";
            lSQL += " WHERE CID='" + CID + "'";

            return lSQL;
        }

        public int GetDetailByCID(ref DataTable pDT)
        {
            string lSQL = "";

            lSQL = "";
            lSQL += "SELECT ";
            lSQL += " ID";
            lSQL += ", CID";
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
            lSQL += " FROM T6_Check_B4_JueJin ";
            lSQL += " WHERE CID='" + CID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);            
        }
    }
}