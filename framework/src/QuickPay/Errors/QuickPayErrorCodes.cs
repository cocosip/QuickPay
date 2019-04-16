namespace QuickPay.Errors
{
    /// <summary>支付错误代码
    /// </summary>
    public enum QuickPayErrorCodes
    {
        /// <summary>签名错误
        /// </summary>
        SignError = 1,

        /// <summary>设置Necessary参数错误
        /// </summary>
        SetNecessaryError = 2,

        /// <summary>设置UniqueId错误
        /// </summary>
        SetUniqueIdError = 3,

        /// <summary>PayData转化错误
        /// </summary>
        PayDataTransformError = 4,

        /// <summary> Execute执行错误
        /// </summary>
        ExecuteError = 5,

        /// <summary>转化Response错误
        /// </summary>
        ParseResponseError = 6,

        /// <summary>支付存储错误
        /// </summary>
        PaymentStoreError = 7,

        /// <summary>退款存储错误
        /// </summary>
        RefundStoreError = 8,
    }
}
