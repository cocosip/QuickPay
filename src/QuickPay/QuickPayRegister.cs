using DotCommon.Dependency;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Executers;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Services.Impl;
using System.Linq;
using System.Reflection;

namespace QuickPay
{
    /// <summary>支付注入
    /// </summary>
    public class QuickPayRegister
    {

        public static void RegisterQuickPay(IIocContainer container, AlipayConfig alipayConfig, WechatPayConfig wechatPayConfig)
        {
            container.Register<AlipayConfig>(alipayConfig);
            container.Register<WechatPayConfig>(wechatPayConfig);

            container.Register<QuickPayConfigLoader>();
            container.Register<IExecuteContextFactory, ExecuteContextFactory>();
            container.Register<IQuickPayConfigManager, QuickPayConfigManager>();
            container.Register<IRequestExecuter, DefaultRequestExecuter>();
            container.Register<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();
            container.Register<IAuthenticationService, AuthenticationService>(DependencyLifeStyle.Transient);

            //支付数据存储
            container.Register<IPaymentStore, EmptyPaymentStore>(DependencyLifeStyle.Transient);
            container.Register<IRefundStore, EmptyRefundStore>(DependencyLifeStyle.Transient);
            container.Register<IAccessTokenStore, EmptyAccessTokenStore>(DependencyLifeStyle.Transient);
            container.Register<IJsApiTicketStore, EmptyJsApiTicketStore>(DependencyLifeStyle.Transient);

            //支付宝Service
            container.Register<IAlipayAppPayService, AlipayAppPayService>(DependencyLifeStyle.Transient);
            container.Register<IAlipayBarcodePayService, AlipayBarcodePayService>(DependencyLifeStyle.Transient);
            container.Register<IAlipayPagePayService, AlipayPagePayService>(DependencyLifeStyle.Transient);
            container.Register<IAlipayQrcodePayService, AlipayQrcodePayService>(DependencyLifeStyle.Transient);
            container.Register<IAlipayWapPayService, AlipayWapPayService>(DependencyLifeStyle.Transient);
            container.Register<IAlipayTradeCommonService, AlipayTradeCommonService>(DependencyLifeStyle.Transient);
            //微信Service
            container.Register<IWechatAppPayService, WechatAppPayService>(DependencyLifeStyle.Transient);
            container.Register<IWechatH5PayService, WechatH5PayService>(DependencyLifeStyle.Transient);
            container.Register<IWechatJsApiPayService, WechatJsApiPayService>(DependencyLifeStyle.Transient);
            container.Register<IWechatMicroPayService, WechatMicroPayService>(DependencyLifeStyle.Transient);
            container.Register<IWechatNativePayService, WechatNativePayService>(DependencyLifeStyle.Transient);
            container.Register<IWechatPayTradeCommonService, WechatPayTradeCommonService>(DependencyLifeStyle.Transient);

            //注册pipeline
            RegisterPipeline(container);
        }

        //注册Pipeline
        public static void RegisterPipeline(IIocContainer container)
        {
            var assemply = Assembly.Load(AssemblyName.GetAssemblyName(QuickPaySettings.AssemblyName));
            var middlewareTypies = assemply.GetTypes().Where(x => typeof(QuickPayMiddleware).IsAssignableFrom(x));
            foreach (var middlewareType in middlewareTypies)
            {
                container.Register(middlewareType, DependencyLifeStyle.Transient);
            }
        }

    }
}
