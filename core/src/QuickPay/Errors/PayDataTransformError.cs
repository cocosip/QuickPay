namespace QuickPay.Errors
{
    /// <summary>PayData转化错误
    /// </summary>
    public class PayDataTransformError : Error
    {
        /// <summary>Ctor
        /// </summary>
        public PayDataTransformError(string message) : base(message, (int)QuickPayErrorCodes.PayDataTransformError)
        {

        }
    }
}
