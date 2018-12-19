using Microsoft.Extensions.DependencyInjection;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>微信支付通知基类
    /// </summary>
    public abstract class WechatPayNotify : AbstractNotify
    {

        /// <summary>微信支付管道名
        /// </summary>
        public override string Provider { get; } = QuickPaySettings.Provider.WechatPay;

        /// <summary>微信支付配置
        /// </summary>
        protected WechatPayConfig Config { get; }

        /// <summary>WechatPayDataHelper
        /// </summary>
        protected WechatPayDataHelper WechatPayDataHelper { get; }

        /// <summary>WechatPayAssistService
        /// </summary>
        protected IWechatPayAssistService WechatPayAssistService { get; }

        /// <summary>Ctor
        /// </summary>
        public WechatPayNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Config = ServiceProvider.GetService<WechatPayConfig>();
            WechatPayDataHelper = ServiceProvider.GetService<WechatPayDataHelper>();
            WechatPayAssistService = ServiceProvider.GetService<IWechatPayAssistService>();
        }

        /// <summary>获取微信支付的App
        /// </summary>
        protected virtual WechatPayApp GetWechatPayApp(PayData payData)
        {
            var appId = GetAppId(payData);
            var wechatPayApp = Config.GetByAppId(appId);
            return wechatPayApp;
        }

        /// <summary>是否为真实的通知
        /// </summary>
        public override Task<bool> IsRealNotify(string notifyBody)
        {
            var payData = WechatPayDataHelper.FromXml(notifyBody);
            var wechatPayApp = GetWechatPayApp(payData);
            using(WechatPayAssistService.Use(wechatPayApp))
            {
                return WechatPayAssistService.VerifySign(payData);
            }
        }
    }
}
