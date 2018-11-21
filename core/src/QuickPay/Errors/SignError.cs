namespace QuickPay.Errors
{
    public class SignError : Error
    {
        public SignError(string message) : base(message, (int)QuickPayErrorCodes.SignError)
        {

        }
    }
}
