using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Api
{
    public class E01_SampleController : ApiController
    {
        [HttpGet]
        public string GetSampleList()
        {
            System.Threading.Thread.Sleep(1000 * 60 * 5);
            T7_Sample lSample = new T7_Sample();
            DataTable lDT = null;
            DataFromBackToFront lResult = new DataFromBackToFront();
            lResult.Status = lSample.GetList(ref lDT);

            if (lResult.Status == (int)MyTool.MyEnum.MyEnum.Enum_Ret.Succes)
            {
                lResult.Datas.Tables.Add(lDT);
            }

            return lResult.FormatToJsonString();
        }
    }

    class DataFromBackToFront
    {
        public int Status = 0;

        public string Message = "";

        public DataSet Datas = null;

        public DataFromBackToFront()
        {
            Status = 0;
            Message = "";
            Datas = new DataSet();
        }

        public string FormatToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
