using QuickPay.Exceptions;
using QuickPay.Notify;
using System;

namespace QuickPay.Configurations
{
    /// <summary>QuickPay配置扩展
    /// </summary>
    public static class QuickPayConfigurationOptionExtensions
    {

        /// <summary>根据泛型添加通知
        /// </summary>
        public static QuickPayConfigurationOption AddNotify<T>(this QuickPayConfigurationOption option)where T : INotify
        {
            return option.AddNotify(typeof(T));
        }

        /// <summary>根据类型添加通知
        /// </summary>
        public static QuickPayConfigurationOption AddNotify(this QuickPayConfigurationOption option, Type notifyType)
        {
            if (!notifyType.IsAssignableFrom(typeof(INotify)))
            {
                throw new QuickPayException($"添加的通知不是有效的通知类型。Type:{notifyType}");
            }
            //管道名
            var notifyProvider = notifyType.IsAssignableFrom(typeof(AlipayNotify)) ? QuickPaySettings.Provider.Alipay : QuickPaySettings.Provider.WechatPay;
            var notifyDefination = new NotifyDefination(notifyProvider, notifyType);
            //添加通知
            option.NotifyDefinations.Add(notifyDefination);
            return option;
        }


    }
}
