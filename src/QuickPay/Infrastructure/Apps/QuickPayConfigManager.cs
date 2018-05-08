using DotCommon.Dependency;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;

namespace QuickPay.Infrastructure.Apps
{
    public class QuickPayConfigManager : IQuickPayConfigManager
    {
        public QuickPayConfig GetCurrentConfig(string provider)
        {
            if (provider == QuickPaySettings.Provider.Alipay)
            {
                return IocManager.GetContainer().Resolve<AlipayConfig>();
            }
            else
            {
                return IocManager.GetContainer().Resolve<WechatPayConfig>();
            }
        }
    }
}
