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
    public abstract class AlipayNotify : BaseNotify
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.Alipay;

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>PayDataHelper
        /// </summary>
        protected AlipayPayDataHelper PayDataHelper { get; }

        /// <summary>支付宝相关辅助服务
        /// </summary>
        protected IAlipayAssistService AlipayAssistService { get; }

        /// <summary>支付宝配置文件存储
        /// </summary>
        protected IAlipayConfigStore ConfigStore { get; }

        /// <summary>Ctor
        /// </summary>
        public AlipayNotify(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            PayDataHelper = ServiceProvider.GetService<AlipayPayDataHelper>();
            AlipayAssistService = ServiceProvider.GetService<IAlipayAssistService>();
            ConfigStore = ServiceProvider.GetService<IAlipayConfigStore>();
        }


        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        public override Task<bool> IsRealNotify(string notifyBody)
        {
            var payData = PayDataHelper.FromJson(notifyBody);
            var appId = PayDataHelper.GetAlipayAppId(payData);
            using (AlipayAssistService.Use(appId))
            {
                return AlipayAssistService.VerifySign(payData);
            }
        }
    }
}
