using System;
namespace QuickPay.Assist.Store
{
    /// <summary>使用内存存储时,支付映射信息
    /// </summary>
    [Serializable]
    public class PaymentMapperItem
    {
        /// <summary>支付UniqueId
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>支付平台Id
        /// </summary>
        public int PayPlatId { get; set; }

        /// <summary>AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>交易信息
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public PaymentMapperItem()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public PaymentMapperItem(string uniqueId, int payPlatId, string appId, string outTradeNo)
        {
            UniqueId = uniqueId;
            PayPlatId = payPlatId;
            AppId = appId;
            OutTradeNo = outTradeNo;
        }



        /// <summary>获取格式化的key
        /// </summary>
        public string GetFormatKey()
        {
            return $"QuickPay:Payment:Mapper:{PayPlatId}-{AppId}-{OutTradeNo}";
        }
    }
}
