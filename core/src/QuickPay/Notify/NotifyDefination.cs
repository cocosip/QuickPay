using System;

namespace QuickPay.Notify
{
    /// <summary>通知定义
    /// </summary>
    public class NotifyDefination
    {
        /// <summary>通知类型
        /// </summary>
        public Type NotifyType { get; set; }

        /// <summary>Provider
        /// </summary>
        public string Provider { get; set; }

        /// <summary>Ctor
        /// </summary>
        public NotifyDefination()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="notifyType">通知类型</param>
        /// <param name="provider">通道,支付宝或微信</param>
        public NotifyDefination(Type notifyType, string provider)
        {
            NotifyType = notifyType;
            Provider = provider;
        }
    }
}
