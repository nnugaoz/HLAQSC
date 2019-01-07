using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using MyTool.Model;

namespace MyTool.Log
{
    /// <summary>
    /// 日志
    /// </summary>
    public static class LogOption
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        private static int _log_level = Convert.ToInt32(ConfigurationManager.AppSettings["Log_Level"]);
        /// <summary>
        /// 日志路径
        /// </summary>
        private static string _path = ConfigurationManager.AppSettings["Log_Path"].ToString();
        /// <summary>
        /// 日志记录
        /// </summary>
        private static List<Model_Log> _logs = new List<Model_Log>();
        /// <summary>
        /// 状态
        /// </summary>
        private static bool _status = false;
        /// <summary>
        /// 版本
        /// </summary>
        private static string _version = "";

        /// <summary>
        /// 添加日志
        /// </summary>
        public static void Log_Add(Model_Log log)
        {
            // 当前日志大于日志等级 允许写日志
            if (log._level >= _log_level)
            {
                _version = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffff");
                _logs.Add(log);
                // 当前没有操作日志
                if (!_status)
                {
                    _status = true;
                    Log_Write();
                }
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        private static void Log_Write()
        {
            // 不存在路径则创建路径
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            string _file_path = _path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            StreamWriter sw = new StreamWriter(_file_path, true);
            //开始写入
            Log_Write_Detail(sw);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();

            _status = false;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="sw"></param>
        private static void Log_Write_Detail(StreamWriter sw)
        {
            // 获取初始版本
            string v1 = _version;

            // 循环处理日志
            int count = _logs.Count;
            for (int i = 0; i < count; i++)
            {
                sw.WriteLine(_logs[0]._date + "==>" + Get_Log_Level(_logs[0]._level) + "   " + _logs[0]._content);
                _logs.Remove(_logs[0]);
            }

            // 对比版本
            if (v1 == _version)
            {
                // 版本一致 描述未有更新
            }
            else
            {
                // 版本不一致 递归
                Log_Write_Detail(sw);
            }
        }

        /// <summary>
        /// 获取日志等级
        /// </summary>
        /// <param name="level">等级 int型</param>
        /// <returns></returns>
        private static string Get_Log_Level(int level)
        {
            string ret = "";
            if (level == (int)MyEnum.MyEnum.Enum_LogLevel.Error)
            {
                ret = "Error";
            }
            if (level == (int)MyEnum.MyEnum.Enum_LogLevel.Info)
            {
                ret = "Info";
            }

            return ret;
        }
    }
}
