using System;
using System.Collections.Generic;

namespace QuickPay.Assist.Store
{
    /// <summary>使用内存存储时,退款交易映射信息,按照OutTradeNo进行映射utTrad
    /// </summary>
    [Serializable]

    public class RefundTradeMapperItem
    {
        /// <summary>支付平台utTrad
        /// </summary>utTradeNo
        public int PayPlatId { get; set; }

        /// <summary>应用Id(支付宝或者微信的AppId)
        /// </summary>
        public string AppId { get; set; }

        /// <summary>本系统退款编号utTradeNo
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>UniqueId集合
        /// </summary>
        public List<string> UniqueIds { get; set; } = new List<string>();

        /// <summary>Ctor
        /// </summary>
        public RefundTradeMapperItem()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public RefundTradeMapperItem(int payPlatId, string appId, string outTradeNo)
        {
            PayPlatId = payPlatId;
            AppId = appId;
            OutTradeNo = outTradeNo;
        }

        /// <summary>获取格式化的key
        /// </summary>
        public string GetFormatKey()
        {
            return $"QuickPay:Payment:TradeMapper:{PayPlatId}-{AppId}-{OutTradeNo}";
        }
    }
}
