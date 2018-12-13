namespace QuickPay.Errors
{
    /// <summary>退款存储错误
    /// </summary>
    public class RefundStoreError : Error
    {
        /// <summary>
        /// </summary>
        public RefundStoreError(string message) : base(message, (int)QuickPayErrorCodes.RefundStoreError)
        {
        }
    }
}
