namespace QuickPay.Errors
{
    /// <summary>转化Reponse错误
    /// </summary>
    public class ParseResponseError : Error
    {
        /// <summary>Ctor
        /// </summary>
        public ParseResponseError(string message) : base(message, (int)QuickPayErrorCodes.ParseResponseError)
        {

        }
    }
}
