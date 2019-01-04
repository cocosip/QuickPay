using System;

namespace QuickPay.Assist.Store
{
    /// <summary>使用内存存储时,退款映射信息
    /// </summary>
    [Serializable]
    public class RefundMapperItem
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

        /// <summary>本系统退款编号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public RefundMapperItem()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public RefundMapperItem(string uniqueId, int payPlatId, string appId, string outRefundNo)
        {
            UniqueId = uniqueId;
            PayPlatId = payPlatId;
            AppId = appId;
            OutRefundNo = outRefundNo;
        }

        /// <summary>获取格式化的key
        /// </summary>
        public string GetFormatKey()
        {
            return $"QuickPay:Payment:Mapper:{PayPlatId}-{AppId}-{OutRefundNo}";
        }
    }
}
