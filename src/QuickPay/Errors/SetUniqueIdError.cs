namespace QuickPay.Errors
{
    public class SetUniqueIdError: Error
    {
        public SetUniqueIdError(string message) : base(message, (int)QuickPayErrorCodes.ExecuteError)
        {
        }
    }
}
