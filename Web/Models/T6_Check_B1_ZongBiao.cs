using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Check_B1_ZongBiao : AutoFiles.T6_Check_B1_ZongBiao
    {
        public string getInsertSQL()
        {
            string lSQL = "";

            lSQL += "INSERT INTO T6_Check_B1_ZongBiao(";
            lSQL += " ID";
            lSQL += ", CID";
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", BYYS";
            lSQL += ")VALUES(";
            lSQL += "'ZB' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Check_B1_ZongBiao), 10)";
            lSQL += ", '" + CID + "'";
            lSQL += ", '" + ZB1 + "'";
            lSQL += ", '" + ZB2 + "'";
            lSQL += ", '" + DW + "'";
            lSQL += ", '" + BYYS + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByCID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Check_B1_ZongBiao ";
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
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", BYYS";
            lSQL += " FROM T6_Check_B1_ZongBiao ";
            lSQL += " WHERE CID='" + CID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);            
        }
    }
}