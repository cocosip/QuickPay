using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Util;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>支付宝通知基类
    /// </summary>
    public abstract class AlipayNotify : AbstractNotify
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider { get; } = QuickPaySettings.Provider.Alipay;

        /// <summary>支付宝配置
        /// </summary>
        protected AlipayConfig Config { get; }

        /// <summary>AlipayPayDataHelper
        /// </summary>
        protected AlipayPayDataHelper AlipayPayDataHelper { get; }

        /// <summary>AlipayAssistService
        /// </summary>
        protected IAlipayAssistService AlipayAssistService { get; }

        /// <summary>Ctor
        /// </summary>
        public AlipayNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Config = ServiceProvider.GetService<AlipayConfig>();
            AlipayPayDataHelper = ServiceProvider.GetService<AlipayPayDataHelper>();
            AlipayAssistService = ServiceProvider.GetService<IAlipayAssistService>();
        }

        /// <summary>获取支付宝的App
        /// </summary>
        protected virtual AlipayApp GetAlipayApp(PayData payData)
        {
            var appId = GetAppId(payData);
            var alipayApp = Config.GetByAppId(appId);
            return alipayApp;
        }

        /// <summary>是否为真实的通知
        /// </summary>
        public override Task<bool> IsRealNotify(string notifyBody)
        {
            var payData = AlipayPayDataHelper.FromJson(notifyBody);
            var alipayApp = GetAlipayApp(payData);
            using(AlipayAssistService.Use(alipayApp))
            {
                return AlipayAssistService.VerifySign(payData);
            }
        }
    }
}
