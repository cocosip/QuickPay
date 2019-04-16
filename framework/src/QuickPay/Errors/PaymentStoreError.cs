namespace QuickPay.Errors
{
    /// <summary>支付信息存储错误
    /// </summary>
    public class PaymentStoreError : Error
    {
        /// <summary>
        /// </summary>
        public PaymentStoreError(string message) : base(message, (int)QuickPayErrorCodes.PaymentStoreError)
        {
        }
    }
}
