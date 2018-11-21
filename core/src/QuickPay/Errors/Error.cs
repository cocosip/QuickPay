namespace QuickPay.Errors
{
    public abstract class Error
    {
        protected Error(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; private set; }
        public int Code { get; private set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
