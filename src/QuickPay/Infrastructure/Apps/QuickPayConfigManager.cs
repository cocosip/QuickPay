using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
using System;
namespace QuickPay.Infrastructure.Apps
{
    public class QuickPayConfigManager : IQuickPayConfigManager
    {
        private readonly IServiceProvider _provider;
        public QuickPayConfigManager(IServiceProvider provider)
        {
            _provider = provider;
        }

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
