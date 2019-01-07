using System;

namespace MyTool.Model
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Model_Log
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="content">日志内容</param>
        public Model_Log(int level, string content)
        {
            _level = level;
            _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffff");
            _content = content;
        }
        /// <summary>
        /// 日志等级
        /// </summary>
        public int _level = 0;
        /// <summary>
        /// 触发时间
        /// </summary>
        public string _date = "";
        /// <summary>
        /// 日志内容
        /// </summary>
        public string _content = "";
    }
}
