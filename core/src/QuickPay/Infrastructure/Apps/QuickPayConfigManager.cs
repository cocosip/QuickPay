using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
using System;
namespace QuickPay.Infrastructure.Apps
{
    /// <summary>QuickPay配置管理
    /// </summary>
    public class QuickPayConfigManager : IQuickPayConfigManager
    {
        private readonly IServiceProvider _provider;
        /// <summary>Ctor
        /// </summary>
        public QuickPayConfigManager(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>根据Provider名称获取当前配置
        /// </summary>
        public QuickPayConfig GetCurrentConfig(string providerName)
        {
            if (providerName == QuickPaySettings.Provider.Alipay)
            {
                return _provider.GetService<AlipayConfig>();
            }
            else
            {
                return _provider.GetService<WechatPayConfig>();
            }
        }
    }
}
