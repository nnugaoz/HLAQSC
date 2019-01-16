using System;

namespace Web.AutoFiles
{
    public class T8_ChuKuang
    {
        public T8_ChuKuang()
        {
        }

		public string ID { get; set; }		public string WorkDate { get; set; }		public string WorkClassCode { get; set; }		public string WorkClassName { get; set; }		public string PositionCode { get; set; }		public string EquipmentID { get; set; }		public string W1 { get; set; }		public string W2 { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T8_ChuKuang.ID "				+ ",T8_ChuKuang.WorkDate "				+ ",T8_ChuKuang.WorkClassCode "				+ ",T8_ChuKuang.WorkClassName "				+ ",T8_ChuKuang.PositionCode "				+ ",T8_ChuKuang.EquipmentID "				+ ",T8_ChuKuang.W1 "				+ ",T8_ChuKuang.W2 "                + " from [HLAQSC].dbo.T8_ChuKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T8_ChuKuang( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "W1 ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "W2 ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W1 + "' ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + W2 + "' ";			}
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(ref string sql, string where)
        {
            sql = ""
                + " update [HLAQSC].dbo.T8_ChuKuang "
                + " set "
				+ " T8_ChuKuang.ID = '" + ID + "' "				+ ",T8_ChuKuang.WorkDate = '" + WorkDate + "' "				+ ",T8_ChuKuang.WorkClassCode = '" + WorkClassCode + "' "				+ ",T8_ChuKuang.WorkClassName = '" + WorkClassName + "' "				+ ",T8_ChuKuang.PositionCode = '" + PositionCode + "' "				+ ",T8_ChuKuang.EquipmentID = '" + EquipmentID + "' "				+ ",T8_ChuKuang.W1 = '" + W1 + "' "				+ ",T8_ChuKuang.W2 = '" + W2 + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T8_ChuKuang "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate = '" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(WorkClassCode))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassCode = '" + WorkClassCode + "' ";			}			if (!String.IsNullOrEmpty(WorkClassName))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkClassName = '" + WorkClassName + "' ";			}			if (!String.IsNullOrEmpty(PositionCode))			{				count++;				sql += (count > 1 ? "," : " ") + "PositionCode = '" + PositionCode + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(W1))			{				count++;				sql += (count > 1 ? "," : " ") + "W1 = '" + W1 + "' ";			}			if (!String.IsNullOrEmpty(W2))			{				count++;				sql += (count > 1 ? "," : " ") + "W2 = '" + W2 + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T8_ChuKuang "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T8_ChuKuang.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}