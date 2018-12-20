using QuickPay.Exceptions;
using QuickPay.Notify;
using System;
using System.Linq;

namespace QuickPay.Configurations
{
    /// <summary>QuickPay配置扩展
    /// </summary>
    public static class QuickPayConfigurationOptionExtensions
    {

        /// <summary>添加异步通知
        /// </summary>
        public static QuickPayConfigurationOption AddNotify<T>(this QuickPayConfigurationOption option)
        {
            return AddNotify(option, typeof(T));
        }

        /// <summary>添加异步通知
        /// </summary>
        public static QuickPayConfigurationOption AddNotify(this QuickPayConfigurationOption option, Type notifyType)
        {
            if (!notifyType.IsAssignableFrom(typeof(AbstractNotify)) || notifyType.IsAbstract)
            {
                throw new QuickPayException($"类型:{notifyType}不是一个有效的异步通知类型.");
            }
            var notifyDefination = new NotifyDefination(notifyType, GetProviderByNotifyType(notifyType));
            return option.AddNotifyByCover(notifyDefination);
        }

        /// <summary>如果已经存在相同类型的通知,就覆盖掉
        /// </summary>
        private static QuickPayConfigurationOption AddNotifyByCover(this QuickPayConfigurationOption option, NotifyDefination notifyDefination)
        {
            var queryNotifyDefination = option.NotifyDefinations.FirstOrDefault(x => x.NotifyType == notifyDefination.NotifyType);
            if (queryNotifyDefination != null)
            {
                option.NotifyDefinations.Remove(queryNotifyDefination);
            }
            option.NotifyDefinations.Add(notifyDefination);
            return option;
        }


        /// <summary>根据通知类型获取通知的Provider
        /// </summary>
        private static string GetProviderByNotifyType(Type notifyType)
        {
            if (notifyType.IsSubclassOf(typeof(AlipayNotify)))
            {
                return QuickPaySettings.Provider.Alipay;
            }

            if (notifyType.IsSubclassOf(typeof(WechatPayNotify)))
            {
                return QuickPaySettings.Provider.WechatPay;
            }
            throw new QuickPayException($"QuickPay通知类型不正确!Type:{notifyType}");
        }

    }
}
