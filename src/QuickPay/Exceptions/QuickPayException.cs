using System;

namespace QuickPay.Exceptions
{
    public class QuickPayException : Exception
    {
        public int Code { get; }

        public string Msg { get; }

        public QuickPayException() : base()
        {

        }

        public QuickPayException(string msg) : base(msg)
        {
            Msg = msg;
        }

        public QuickPayException(int code, string msg) : base($"{code}|{msg}")
        {
            Code = code;
            Msg = msg;
        }

    }
}
