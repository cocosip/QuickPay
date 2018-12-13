namespace QuickPay.Errors
{
    /// <summary>支付错误代码
    /// </summary>
    public enum QuickPayErrorCodes
    {
        /// <summary>签名错误
        /// </summary>
        SignError = 1,

        /// <summary>设置UniqueId错误
        /// </summary>
        SetUniqueIdError = 2,

        /// <summary>PayData转化错误
        /// </summary>
        PayDataTransformError = 3,

        /// <summary> Execute执行错误
        /// </summary>
        ExecuteError = 4,
        
        /// <summary>转化Response错误
        /// </summary>
        ParseResponseError = 5,

        /// <summary>支付存储错误
        /// </summary>
        PaymentStoreError = 6,

        /// <summary>退款存储错误
        /// </summary>
        RefundStoreError = 7,
    }
}
