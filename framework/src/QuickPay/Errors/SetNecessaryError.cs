namespace QuickPay.Errors
{
    /// <summary>设置必要参数错误
    /// </summary>
    public class SetNecessaryError : Error
    {
        /// <summary>Ctor
        /// </summary>
        public SetNecessaryError(string message) : base(message, (int)QuickPayErrorCodes.SetNecessaryError) { }
    }
}
