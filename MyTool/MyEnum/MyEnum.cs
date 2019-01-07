namespace MyTool.MyEnum
{
    /// <summary>
    /// 枚举
    /// </summary>
    public static class MyEnum
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public enum Enum_LogLevel
        {
            /// <summary>
            /// 错误
            /// </summary>
            Error = 9,
            /// <summary>
            /// 调试
            /// </summary>
            Info = 5,
        }

        public enum Enum_Ret
        {
            /// <summary>
            /// 错误
            /// </summary>
            Error = 0,
            /// <summary>
            /// 成功
            /// </summary>
            Succes = 1,
            /// <summary>
            /// 无数据
            /// </summary>
            NoData = 2,
            /// <summary>
            /// Session超时
            /// </summary>
            TimeOut_Session = 11,
            /// <summary>
            /// 主键重复
            /// </summary>
            KeyError = 21,
        }
    }
}
