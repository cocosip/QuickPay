using Microsoft.Extensions.DependencyInjection;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Util;
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
        public override string Provider => QuickPaySettings.Provider.WechatPay;

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>微信支付配置信息
        /// </summary>
        protected WechatPayConfig Config { get; }

        /// <summary>PayDataHelper
        /// </summary>
        protected WechatPayDataHelper PayDataHelper { get; }

        /// <summary>微信支付相关辅助服务
        /// </summary>
        protected IWechatPayAssistService WechatPayAssistService { get; }

        /// <summary>Ctor
        /// </summary>
        public WechatPayNotify(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Config = ServiceProvider.GetService<WechatPayConfig>();
            PayDataHelper = ServiceProvider.GetService<WechatPayDataHelper>();
            WechatPayAssistService = ServiceProvider.GetService<IWechatPayAssistService>();
        }

        /// <summary>从PayData中获取App信息
        /// </summary>
        protected WechatPayApp GetApp(PayData payData)
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
            var wechatPayApp = GetApp(payData);
            using(WechatPayAssistService.Use(wechatPayApp))
            {
                return WechatPayAssistService.VerifySign(payData);
            }
        }
    }
}
