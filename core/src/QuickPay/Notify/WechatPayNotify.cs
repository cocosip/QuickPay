using Microsoft.Extensions.DependencyInjection;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>微信支付通知策略
    /// </summary>
    public abstract class WechatPayNotify : BaseNotify
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.WeChatPay;

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>微信支付配置信息
        /// </summary>
        protected WeChatPayConfig Config { get; }

        /// <summary>PayDataHelper
        /// </summary>
        protected WeChatPayDataHelper PayDataHelper { get; }

        /// <summary>微信支付相关辅助服务
        /// </summary>
        protected IWeChatPayAssistService WechatPayAssistService { get; }

        /// <summary>Ctor
        /// </summary>
        public WechatPayNotify(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Config = ServiceProvider.GetService<WeChatPayConfig>();
            PayDataHelper = ServiceProvider.GetService<WeChatPayDataHelper>();
            WechatPayAssistService = ServiceProvider.GetService<IWeChatPayAssistService>();
        }

        /// <summary>从PayData中获取App信息
        /// </summary>
        protected WeChatPayApp GetApp(PayData payData)
        {
            //获取AppId
            var appId = PayDataHelper.GetWechatAppId(payData);
            return Config.GetByAppId(appId);
        }


        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        public override Task<bool> IsRealNotify(string notifyBody)
        {
            var payData = PayDataHelper.FromXml(notifyBody);
            var weChatPayApp = GetApp(payData);
            using(WechatPayAssistService.Use(weChatPayApp))
            {
                return WechatPayAssistService.VerifySign(payData);
            }
        }
    }
}
