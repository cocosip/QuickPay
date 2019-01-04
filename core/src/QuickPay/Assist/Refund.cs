using System;

namespace QuickPay.Assist
{
    /// <summary>退款
    /// </summary>
    [Serializable]
    public class Refund
    {
        /// <summary>唯一Id
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>支付平台
        /// </summary>
        public int PayPlatId { get; set; }

        /// <summary>应用Id(支付宝或者微信的AppId)
        /// </summary>
        public string AppId { get; set; }

        /// <summary>交易编号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>交易号(支付宝或者微信系统)
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>本系统退款编号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>微信或者支付宝退款号
        /// </summary>
        public string RefundId { get; set; }


        /// <summary>参数
        /// </summary>
        public string PayObject { get; set; }

        /// <summary>描述
        /// </summary>
        public string Describe { get; set; }
    }
}
