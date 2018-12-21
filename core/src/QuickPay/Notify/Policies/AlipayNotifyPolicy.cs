using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Util;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{

    /// <summary>支付宝通知策略
    /// </summary>
    public abstract class AlipayNotifyPolicy : INotifyPolicy
    {

        /// <summary>支付宝管道名
        /// </summary>
        public string Provider => QuickPaySettings.Provider.Alipay;

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>支付宝配置信息
        /// </summary>
        protected AlipayConfig Config { get; }

        /// <summary>PayDataHelper
        /// </summary>
        protected AlipayPayDataHelper PayDataHelper { get; }

        /// <summary>支付宝相关辅助服务
        /// </summary>
        protected IAlipayAssistService AlipayAssistService { get; }

        /// <summary>Ctor
        /// </summary>
        public AlipayNotifyPolicy(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Config = ServiceProvider.GetService<AlipayConfig>();
            PayDataHelper = ServiceProvider.GetService<AlipayPayDataHelper>();
            AlipayAssistService = ServiceProvider.GetService<IAlipayAssistService>();
        }

        /// <summary>从PayData中获取App信息
        /// </summary>
        protected AlipayApp GetApp(PayData payData)
        {
            //获取AppId
            var appId = PayDataHelper.GetAlipayAppId(payData);
            return Config.GetByAppId(appId);
        }



        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        public Task<bool> IsRealNotify(PayData payData)
        {
            var alipayApp = GetApp(payData);
            using(AlipayAssistService.Use(alipayApp))
            {
                return AlipayAssistService.VerifySign(payData);
            }
        }
    }
}
