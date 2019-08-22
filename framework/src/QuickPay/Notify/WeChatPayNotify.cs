using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>微信支付通知策略
    /// </summary>
    public abstract class WeChatPayNotify : BaseNotify
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.WeChatPay;

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>PayDataHelper
        /// </summary>
        protected WeChatPayDataHelper PayDataHelper { get; }

        /// <summary>微信支付相关辅助服务
        /// </summary>
        protected IWeChatPayAssistService WeChatPayAssistService { get; }

        /// <summary>配置文件存储
        /// </summary>
        protected IWeChatPayConfigStore ConfigStore { get; }

        /// <summary>Ctor
        /// </summary>
        public WeChatPayNotify(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            PayDataHelper = ServiceProvider.GetService<WeChatPayDataHelper>();
            WeChatPayAssistService = ServiceProvider.GetService<IWeChatPayAssistService>();
            ConfigStore = ServiceProvider.GetService<IWeChatPayConfigStore>();
        }

        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        public override Task<bool> IsRealNotify(string notifyBody)
        {
            var payData = PayDataHelper.FromXml(notifyBody);
            var appId = PayDataHelper.GetWeChatAppId(payData);
            using (WeChatPayAssistService.Use(appId))
            {
                return WeChatPayAssistService.VerifySign(payData);
            }
        }
    }
}
