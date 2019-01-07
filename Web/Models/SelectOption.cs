using MyTool.DB;
using System.Data;

namespace Web.Models
{
    public class SelectOption
    {
        public int Common_GetAll(ref DataTable dt)
        {
            string lSql = "";
            switch (SelectType)
            {
                case "PRoleID":
                    lSql = ""
                       + " select "
                           + " row_number() over (order by ID) i "
                           + ",ID SelectVal "
                           + ",Title SelectTitle "
                       + " from T2_PRole "
                       + " where 1=1 "
                           + " and Del = '0' ";
                    break;
                case "EquipmentType":
                    lSql = ""
                       + " select "
                           + " row_number() over (order by Type) i "
                           + ",Type SelectVal "
                           + ",Title SelectTitle "
                       + " from T3_EquipmentType "
                       + " where 1=1 "
                           + " and Del = '0' ";
                    break;
                default:
                    lSql = ""
                        + " select "
                            + " row_number() over (order by DircKey) i "
                            + ",DircKey SelectVal "
                            + ",DircTitle SelectTitle "
                        + " from T1_DataDirc "
                        + " where 1=1 "
                            + " and Type = '" + SelectType + "' "
                            + " and Del = '0' ";
                    break;
            }


            return DataTool.Get_DataTable_From_DataSet_2(lSql, ref dt);
        }

        public string SelectType { get; set; }

        public string SelectVal { get; set; }

        public string SelectTitle { get; set; }
    }
}