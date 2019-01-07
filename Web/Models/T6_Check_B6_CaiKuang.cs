using MyTool.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class T6_Check_B6_CaiKuang : AutoFiles.T6_Check_B6_CaiKuang
    {
        public String GetInsertSQL()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " INSERT INTO T6_Check_B6_CaiKuang( ";
            lSQL += " ID";
            lSQL += ", CID";
            lSQL += ", ZD";
            lSQL += ", CC";
            lSQL += ", CKLX";
            lSQL += ", DZPW_X";
            lSQL += ", DZPW_T";
            lSQL += ", DZPW_C";
            lSQL += ", DZPW_L";
            lSQL += ", CKL";
            lSQL += ", TCZL";
            lSQL += ", WSL";
            lSQL += ", JJL";
            lSQL += ", KSSJ";
            lSQL += ", JSSJ";
            lSQL += ", BZ";
            lSQL += ")VALUES(";
            lSQL += " 'CK' + dbo.FP_Tool_IDAddOne((select max(ID) from T6_Check_B6_CaiKuang), 10)";
            lSQL += ", '" + CID + "'";
            lSQL += ", '" + ZD + "'";
            lSQL += ", '" + CC + "'";
            lSQL += ", '" + CKLX + "'";
            lSQL += ", '" + DZPW_X + "'";
            lSQL += ", '" + DZPW_T + "'";
            lSQL += ", '" + DZPW_C + "'";
            lSQL += ", '" + DZPW_L + "'";
            lSQL += ", '" + CKL + "'";
            lSQL += ", '" + TCZL + "'";
            lSQL += ", '" + WSL + "'";
            lSQL += ", '" + JJL + "'";
            lSQL += ", '" + KSSJ + "'";
            lSQL += ", '" + JSSJ + "'";
            lSQL += ", '" + BZ + "'";
            lSQL += ")";

            return lSQL;
        }

        public string DeleteByCID()
        {
            string lSQL = "";

            lSQL = "";
            lSQL += " DELETE FROM T6_Check_B6_CaiKuang ";
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
            lSQL += ", CKLX";
            lSQL += ", DZPW_X";
            lSQL += ", DZPW_T";
            lSQL += ", DZPW_C";
            lSQL += ", DZPW_L";
            lSQL += ", CKL";
            lSQL += ", TCZL";
            lSQL += ", WSL";
            lSQL += ", JJL";
            lSQL += ", KSSJ";
            lSQL += ", JSSJ";
            lSQL += ", BZ";
            lSQL += " FROM T6_Check_B6_CaiKuang ";
            lSQL += " WHERE CID='" + CID + "'";

            return DataTool.Get_DataTable_From_DataSet_2(lSQL, ref pDT);            
        }
    }
}