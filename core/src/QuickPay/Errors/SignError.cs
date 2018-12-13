namespace QuickPay.Errors
{
    /// <summary>签名错误
    /// </summary>
    public class SignError : Error
    {
        /// <summary>
        /// </summary>
        public SignError(string message) : base(message, (int)QuickPayErrorCodes.SignError)
        {

        }
    }
}
