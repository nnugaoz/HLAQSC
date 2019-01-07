using MyTool.Model;
using MyTool.MyEnum;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Api
{
    public class Z05_ZYMController : ApiController
    {
        private Model_Ret _model_ret = new Model_Ret();

        #region 作业面管理
        /// <summary>
        /// 作业面初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ListInit(string LoginName, string CCCode)
        {
            T2_Position obj_position = new T2_Position();
            obj_position.Code = CCCode;
            obj_position.Type = "3";
            obj_position.SCJBatch_GetAll_ByType(ref _model_ret.mrd01.dt, LoginName);

            T4_MP obj_mp = new T4_MP();
            obj_mp.SCJBatch_GetMonthInfo_ZYM(ref _model_ret.mrd02.dt, LoginName, "", CCCode, "");
            obj_mp.SCJBatch_GetInfo_ZYM(ref _model_ret.mrd03.dt, LoginName, "", CCCode, "");

            T5_MessageBoard obj_message = new T5_MessageBoard();
            _model_ret.mrd04.ret_json = "[]";
            obj_message.SCJBatch_GetAll(ref _model_ret.mrd04.dt, LoginName, "");

            _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
            return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
        }
        #endregion 作业面管理

        #region 作业面编辑
        /// <summary>
        /// 作业面初始化
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DoSave([FromBody]JObject obj)
        {
            #region 写日志
            T5_WorkRecord obj_wr = new T5_WorkRecord();
            //obj_wr.Txt_UpdateOne(obj.ToString());
            #endregion 写日志

            #region
            try
            {
                obj_wr.WorkManID = obj["WorkManID"].ToString();
                obj_wr.WorkDate = obj["WorkDate"].ToString();
                obj_wr.WorkClassCode = obj["WorkClassCode"].ToString();

                obj_wr.RecordType = "M";
                obj_wr.Status = "1";
                obj_wr.Del = "0";

                string ps = "";

                JArray details = (JArray)obj["Detail"];
                for (int i = 0; i < details.Count; i++)
                {
                    if (i > 0)
                    {
                        ps += "**";
                    }

                    ps += details[i]["PositionCode"].ToString();
                    ps += ";";
                    ps += details[i]["WhereAbout"].ToString();
                    ps += ";";
                    ps += details[i]["WorkHour"].ToString();
                    ps += ";";

                    JArray dfs = (JArray)details[i]["DF"];
                    for (int j = 0; j < dfs.Count; j++)
                    {
                        if (j > 0)
                        {
                            ps += "*";
                        }

                        ps += dfs[j]["FieldKey"].ToString();
                        ps += ",";
                        ps += dfs[j]["FieldValue"].ToString();
                        ps += ",";
                        if (dfs[j]["FieldType"] != null)
                        {
                            ps += dfs[j]["FieldType"].ToString();
                        }
                        ps += ",";
                        if (dfs[j]["FieldUnit"] != null)
                        {
                            ps += dfs[j]["FieldUnit"].ToString();
                        }
                    }

                    ps += ";";
                    ps += details[i]["PositionStatus"].ToString();
                }

                obj_wr.Positions = ps;

                obj_wr.SCJ_UpdateOne();

                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Succes;
                return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                _model_ret.ret_status = (int)MyEnum.Enum_Ret.Error;
                return new HttpResponseMessage { Content = new StringContent(_model_ret.Get_Ret(), System.Text.Encoding.UTF8, "application/json") };
            }
            #endregion
        }
        #endregion 作业面编辑
    }
}
