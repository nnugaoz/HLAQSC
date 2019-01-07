using System;
using System.Data;
using System.Text;
using MyTool.Model;
using MyTool.Log;
using System.Collections;

namespace MyTool.DB
{
    public class DataTool
    {
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="dt">取出的DataTable</param>
        /// <returns></returns>
        public static bool Get_DataTable_From_DataSet_1(string sql, ref DataTable dt)
        {
            try
            {
                // sql文为空
                if (String.IsNullOrEmpty(sql))
                {
                    return false;
                }

                DataSet ds = new DataSet();
                ds = SqlOption.Query(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // 存在数据
                    dt = ds.Tables[0];
                    return true;
                }
                else
                {
                    // 不存在数据
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Info, sql));
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="dt">取出的DataTable</param>
        /// <returns></returns>
        public static int Get_DataTable_From_DataSet_2(string sql, ref DataTable dt)
        {
            try
            {
                // sql文为空
                if (String.IsNullOrEmpty(sql))
                {
                    return (int)MyEnum.MyEnum.Enum_Ret.Error;
                }

                DataSet ds = new DataSet();
                ds = SqlOption.Query(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // 存在数据
                    dt = ds.Tables[0];
                    return (int)MyEnum.MyEnum.Enum_Ret.Succes;
                }
                else
                {
                    // 不存在数据
                    return (int)MyEnum.MyEnum.Enum_Ret.NoData;
                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Info, sql));
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return (int)MyEnum.MyEnum.Enum_Ret.Error;
            }
        }

        public static bool Update(string sql)
        {
            try
            {
                // sql文为空
                if (String.IsNullOrEmpty(sql))
                {
                    return false;
                }

                int ret = SqlOption.ExecuteSql(sql);
                if (ret > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Info, sql));
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return false;
            }
        }

        public static bool Delete(string sql)
        {
            try
            {
                // sql文为空
                if (String.IsNullOrEmpty(sql))
                {
                    return false;
                }

                int ret = SqlOption.ExecuteSql(sql);
                if (ret > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Info, sql));
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// 实现数据库事务
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ExecuteSqlTran(string sql)
        {
            try
            {
                // sql文为空
                if (String.IsNullOrEmpty(sql))
                {
                    return false;
                }

                ArrayList it = new ArrayList();
                it.Add(sql);
                return SqlOption.ExecuteSqlTran(it);
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Info, sql));
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// 将DataTable转化成json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static bool Get_Json_From_DataTable(DataTable dt, ref string json, bool is_one)
        {
            try
            {
                StringBuilder Json = new StringBuilder();
                // 单条数据
                if (is_one)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j > 0)
                            {
                                Json.Append(",");
                            }
                            Json.Append("'" + dt.Columns[j].ColumnName.ToString() + "':'" + dt.Rows[0][j].ToString() + "'");
                        }
                        Json.Append("}");
                    }
                }
                // 多条数据
                else
                {
                    Json.Append("[");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                Json.Append(",");
                            }
                            Json.Append("{");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (j > 0)
                                {
                                    Json.Append(",");
                                }
                                Json.Append("'" + dt.Columns[j].ColumnName.ToString() + "':'" + dt.Rows[i][j].ToString() + "'");
                            }
                            Json.Append("}");
                        }
                    }
                    Json.Append("]");
                }

                json = Json.ToString();
                return true;
            }
            catch (Exception ex)
            {
                LogOption.Log_Add(new Model_Log((int)MyEnum.MyEnum.Enum_LogLevel.Error, ex.ToString()));
                return false;
            }
        }
    }
}