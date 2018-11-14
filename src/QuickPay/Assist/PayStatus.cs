using System.ComponentModel;

namespace QuickPay.Assist
{
    /// <summary>支付状态
    /// </summary>
    public enum PayStatus
    {
        /// <summary>待支付
        /// </summary>
        [Description("待支付")]
        Pending = 1,
        /// <summary>进行中
        /// </summary>
        [Description("进行中")]
        Processing = 2,
        /// <summary>支付成功
        /// </summary>
        [Description("支付成功")]
        Success = 4,
        /// <summary>已关闭
        /// </summary>
        [Description("已关闭")]
        Closed = 8,
        /// <summary>全部退款
        /// </summary>
        [Description("全额退款")]
        Refund = 16,
        /// <summary>部分退款
        /// </summary>
        [Description("部分退款")]
        PartRefund = 32,
        /// <summary>无效(通常出现异常等情况的时候)
        /// </summary>
        [Description("失效")]
        Invalid = 64
    }
}
