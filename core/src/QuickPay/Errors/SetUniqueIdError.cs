namespace QuickPay.Errors
{

    /// <summary>设置UniqueId错误
    /// </summary>
    public class SetUniqueIdError: Error
    {
        /// <summary>
        /// </summary>
        public SetUniqueIdError(string message) : base(message, (int)QuickPayErrorCodes.ExecuteError)
        {
        }
    }
}
