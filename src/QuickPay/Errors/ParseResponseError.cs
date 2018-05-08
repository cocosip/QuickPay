namespace QuickPay.Errors
{
    public class ParseResponseError : Error
    {
        public ParseResponseError(string message) : base(message, (int)QuickPayErrorCodes.ParseResponse)
        {

        }
    }
}
