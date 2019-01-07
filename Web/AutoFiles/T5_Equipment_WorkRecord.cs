using System;

namespace Web.AutoFiles
{
    public class T5_Equipment_WorkRecord
    {
        public T5_Equipment_WorkRecord()
        {
        }

		public string ID { get; set; }		public string EquipmentID { get; set; }		public string WorkDate { get; set; }		public string Reamrk { get; set; }		public string EntryManID { get; set; }		public string EntryManName { get; set; }		public string RRoleCode { get; set; }		public string Status { get; set; }		public string Del { get; set; }
        public bool Select(ref string sql, string where)
        {
            sql = ""
                + " select "
				+ " T5_Equipment_WorkRecord.ID "				+ ",T5_Equipment_WorkRecord.EquipmentID "				+ ",T5_Equipment_WorkRecord.WorkDate "				+ ",T5_Equipment_WorkRecord.Reamrk "				+ ",T5_Equipment_WorkRecord.EntryManID "				+ ",T5_Equipment_WorkRecord.EntryManName "				+ ",T5_Equipment_WorkRecord.RRoleCode "				+ ",T5_Equipment_WorkRecord.Status "				+ ",T5_Equipment_WorkRecord.Del "                + " from [HLAQSC].dbo.T5_Equipment_WorkRecord "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Insert(ref string sql)
        {
            sql = "";
            sql += " insert into [HLAQSC].dbo.T5_Equipment_WorkRecord( ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate ";			}			if (!String.IsNullOrEmpty(Reamrk))			{				count++;				sql += (count > 1 ? "," : " ") + "Reamrk ";			}			if (!String.IsNullOrEmpty(EntryManID))			{				count++;				sql += (count > 1 ? "," : " ") + "EntryManID ";			}			if (!String.IsNullOrEmpty(EntryManName))			{				count++;				sql += (count > 1 ? "," : " ") + "EntryManName ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del ";			}
            sql += " ) ";
            sql += " select ";

            count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + ID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(Reamrk))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Reamrk + "' ";			}			if (!String.IsNullOrEmpty(EntryManID))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EntryManID + "' ";			}			if (!String.IsNullOrEmpty(EntryManName))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + EntryManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "'" + Del + "' ";			}
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
                + " update [HLAQSC].dbo.T5_Equipment_WorkRecord "
                + " set "
				+ " T5_Equipment_WorkRecord.ID = '" + ID + "' "				+ ",T5_Equipment_WorkRecord.EquipmentID = '" + EquipmentID + "' "				+ ",T5_Equipment_WorkRecord.WorkDate = '" + WorkDate + "' "				+ ",T5_Equipment_WorkRecord.Reamrk = '" + Reamrk + "' "				+ ",T5_Equipment_WorkRecord.EntryManID = '" + EntryManID + "' "				+ ",T5_Equipment_WorkRecord.EntryManName = '" + EntryManName + "' "				+ ",T5_Equipment_WorkRecord.RRoleCode = '" + RRoleCode + "' "				+ ",T5_Equipment_WorkRecord.Status = '" + Status + "' "				+ ",T5_Equipment_WorkRecord.Del = '" + Del + "' "                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Update_1(ref string sql, string where)
        {
            sql = "";
            sql += " update [HLAQSC].dbo.T5_Equipment_WorkRecord "
                + " set ";

            int count = 0;
			if (!String.IsNullOrEmpty(ID))			{				count++;				sql += (count > 1 ? "," : " ") + "ID = '" + ID + "' ";			}			if (!String.IsNullOrEmpty(EquipmentID))			{				count++;				sql += (count > 1 ? "," : " ") + "EquipmentID = '" + EquipmentID + "' ";			}			if (!String.IsNullOrEmpty(WorkDate))			{				count++;				sql += (count > 1 ? "," : " ") + "WorkDate = '" + WorkDate + "' ";			}			if (!String.IsNullOrEmpty(Reamrk))			{				count++;				sql += (count > 1 ? "," : " ") + "Reamrk = '" + Reamrk + "' ";			}			if (!String.IsNullOrEmpty(EntryManID))			{				count++;				sql += (count > 1 ? "," : " ") + "EntryManID = '" + EntryManID + "' ";			}			if (!String.IsNullOrEmpty(EntryManName))			{				count++;				sql += (count > 1 ? "," : " ") + "EntryManName = '" + EntryManName + "' ";			}			if (!String.IsNullOrEmpty(RRoleCode))			{				count++;				sql += (count > 1 ? "," : " ") + "RRoleCode = '" + RRoleCode + "' ";			}			if (!String.IsNullOrEmpty(Status))			{				count++;				sql += (count > 1 ? "," : " ") + "Status = '" + Status + "' ";			}			if (!String.IsNullOrEmpty(Del))			{				count++;				sql += (count > 1 ? "," : " ") + "Del = '" + Del + "' ";			}
            sql += " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }

        public bool Delete(ref string sql, string where)
        {
            sql = ""
                + " delete [HLAQSC].dbo.T5_Equipment_WorkRecord "
                + " where 1=1 ";
				if (String.IsNullOrEmpty(where))				{					sql += " and T5_Equipment_WorkRecord.ID = '" + ID + "' ";				}				else				{					sql += where;				}
            return true;
        }
    }
}