using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Check_B3_CaiJue : AutoFiles.T6_Check_B3_CaiJue
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Check_B3_CaiJue( ";
            lSQL += " ID";
            lSQL += ", CID";
            lSQL += ", ZB1";
            lSQL += ", ZB2";
            lSQL += ", DW";
            lSQL += ", NDYS";
            lSQL += ", YJWC1";
            lSQL += ", WCL1";
            lSQL += ", BYYS";
            lSQL += ", YJWC2";
            lSQL += ", WCL2";
            lSQL += ")VALUES(";
            lSQL += " 'CJ' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Check_B3_CaiJue), 10)";
            lSQL += ", '" + CID + "'";
            lSQL += ", '" + ZB1 + "'";
            lSQL += ", '" + ZB2 + "'";
            lSQL += ", '" + DW + "'";
            lSQL += ", '" + NDYS + "'";
            lSQL += ", '" + YJWC1 + "'";
            lSQL += ", '" + WCL1 + "'";
            lSQL += ", '" + BYYS + "'";
            lSQL += ", '" + YJWC2 + "'";
            lSQL += ", '" + WCL2 + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByCID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Check_B3_CaiJue ";
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
            lSQL += ", NDYS";
            lSQL += ", YJWC1";
            lSQL += ", WCL1";
            lSQL += ", BYYS";
            lSQL += ", YJWC2";
            lSQL += ", WCL2";
            lSQL += " FROM T6_Check_B3_CaiJue ";
            lSQL += " WHERE CID='" + CID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);            
        }
    }
}