using System;

namespace QuickPay.Notify
{
    /// <summary>通知定义
    /// </summary>
    public class NotifyDefination
    {
        /// <summary>管道
        /// </summary>
        public string Provider { get; set; }

        /// <summary>通知类型
        /// </summary>
        public Type NotifyType { get; set; }

        /// <summary>Ctor
        /// </summary>
        public NotifyDefination()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public NotifyDefination(string provider, Type notifyType)
        {
            Provider = provider;
            NotifyType = notifyType;
        }
    }
}
