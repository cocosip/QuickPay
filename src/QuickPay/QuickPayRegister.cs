using DotCommon.Dependency;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
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
            container.Register<IRequestExecuter, DefaultRequestExecuter>();
            container.Register<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();
            container.Register<IAuthenticationService, AuthenticationService>();

            //支付数据存储
            container.Register<IPaymentStore, EmptyPaymentStore>();
            container.Register<IRefundStore, EmptyRefundStore>();
            container.Register<IAccessTokenStore, EmptyAccessTokenStore>();
            container.Register<IJsApiTicketStore, EmptyJsApiTicketStore>();

            //支付宝Service
            container.Register<IAlipayAppPayService, AlipayAppPayService>();
            container.Register<IAlipayBarcodePayService, AlipayBarcodePayService>();
            container.Register<IAlipayPagePayService, AlipayPagePayService>();
            container.Register<IAlipayQrcodePayService, AlipayQrcodePayService>();
            container.Register<IAlipayWapPayService, AlipayWapPayService>();
            container.Register<IAlipayTradeCommonService, AlipayTradeCommonService>();
            //微信Service
            container.Register<IWechatAppPayService, WechatAppPayService>();
            container.Register<IWechatH5PayService, WechatH5PayService>();
            container.Register<IWechatJsApiPayService, WechatJsApiPayService>();
            container.Register<IWechatMicroPayService, WechatMicroPayService>();
            container.Register<IWechatNativePayService, WechatNativePayService>();
            container.Register<IWechatPayTradeCommonService, WechatPayTradeCommonService>();
            
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
