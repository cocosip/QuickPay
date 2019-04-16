using System;

namespace QuickPay.Exceptions
{
    /// <summary>QuickPay异常类
    /// </summary>
    public class QuickPayException : Exception
    {
        /// <summary>异常代码
        /// </summary>
        public int Code { get; }

        /// <summary>异常消息
        /// </summary>
        public string Msg { get; }

        /// <summary>
        /// </summary>
        public QuickPayException() : base()
        {

        }

        /// <summary>
        /// </summary>
        public QuickPayException(string msg) : base(msg)
        {
            Msg = msg;
        }

        /// <summary>
        /// </summary>
        public QuickPayException(int code, string msg) : base($"{code}|{msg}")
        {
            Code = code;
            Msg = msg;
        }

    }
}
