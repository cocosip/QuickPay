namespace QuickPay.Errors
{
    /// <summary>错误信息抽象类
    /// </summary>
    public abstract class Error
    {
        /// <summary>Ctor
        /// </summary>
        protected Error(string message, int code)
        {
            Message = message;
            Code = code;
        }
        /// <summary>错误信息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>错误代码
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// </summary>
        public override string ToString()
        {
            if (Code != 0)
            {
                return Message;
            }
            return $"{Code}|{Message}";
        }
    }
}
