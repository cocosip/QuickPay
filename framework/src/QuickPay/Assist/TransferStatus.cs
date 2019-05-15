using System.ComponentModel;

namespace QuickPay.Assist
{
    /// <summary>转账状态
    /// </summary>
    public enum TransferStatus
    {
        /// <summary>转账成功
        /// </summary>
        [Description("转换成功")]
        Success = 1,

        /// <summary>转账失败
        /// </summary>
        [Description("转账失败")]
        Failed = 2
    }
}
