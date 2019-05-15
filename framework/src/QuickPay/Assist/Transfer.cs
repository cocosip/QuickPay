using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPay.Assist
{
    /// <summary>转账信息
    /// </summary>
    [Serializable]
    public class Transfer
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

        /// <summary>交易编号(本系统)
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>交易类型
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>业务代码
        /// </summary>
        public string BusinessCode { get; set; }

        /// <summary>微信付款单号/支付宝转账单据号,转账后支付宝或者微信返回的
        /// </summary>
        public string TransferNo { get; set; }

        /// <summary>金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>支付状态
        /// </summary>
        public int TransferStatusId { get; set; }

        /// <summary>转账的时间
        /// </summary>
        public DateTime? TransferTime { get; set; }

        /// <summary>参数
        /// </summary>
        public string PayObject { get; set; }

        /// <summary>描述
        /// </summary>
        public string Describe { get; set; }
    }
}
