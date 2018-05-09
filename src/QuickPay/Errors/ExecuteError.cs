namespace QuickPay.Errors
{
    /// <summary>执行错误
    /// </summary>
    public class ExecuteError : Error
    {
        public ExecuteError(string message) : base(message, (int)QuickPayErrorCodes.ExecuteError)
        {
        }
    }
}
