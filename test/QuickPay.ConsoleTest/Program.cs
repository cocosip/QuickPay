using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Threading;
using DotCommon.Utility;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Services.DTOs;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuickPay.ConsoleTest
{
    class Program
    {
        private static ILogger _logger;
        private static WechatPayConfig _wechatPayConfig;
        private static AlipayConfig _alipayConfig;
        private static Stopwatch _stopwatch = new Stopwatch();
        static void Main(string[] args)
        {

            //初始化
            Bootstraper.Initialize();
            _logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
            _wechatPayConfig = IocManager.GetContainer().Resolve<WechatPayConfig>();
            _alipayConfig = IocManager.GetContainer().Resolve<AlipayConfig>();
            _logger.Info($"初始化完成");


            _stopwatch.Start();
            //微信
            //AsyncHelper.RunSync(() =>
            //{
            //    //微信App下单
            //    return WechatAppUnifiedOrder();
            //    //微信JsApi下单
            //    // return WechatJsApiUnifiedOrder();
            //});

            //支付宝
            AsyncHelper.RunSync(() =>
            {
                //支付宝App下单
                return AlipayAppTradePay();
                //支付宝PC下单
                //return AlipayPageTradePay();
            });


            _stopwatch.Stop();

            _logger.Info(_stopwatch.Elapsed);


            Console.ReadLine();
        }

        /// <summary>微信App统一下单
        /// </summary>
        static async Task WechatAppUnifiedOrder()
        {
            var appService = IocManager.GetContainer().Resolve<IWechatAppPayService>();
            using (appService.Use(_wechatPayConfig.GetByName("App1")))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                await appService.UnifiedOrder(input);
            }
        }

        /// <summary>微信JsApi下单
        /// </summary>
        static async Task WechatJsApiUnifiedOrder()
        {
            var jsApiService = IocManager.GetContainer().Resolve<IWechatJsApiPayService>();
            using (jsApiService.Use(_wechatPayConfig.GetByName("App2")))
            {
                var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "opaInxF28ub-ea5JVrZOosDHyXZY");
                await jsApiService.UnifiedOrder(input);
            }
        }

        /// <summary>支付宝App支付
        /// </summary>
        static async Task<string> AlipayAppTradePay()
        {
            var appPayService = IocManager.GetContainer().Resolve<IAlipayAppPayService>();
            using (appPayService.Use(_alipayConfig.GetByName("App1")))
            {
                var input = new AppTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1");
                var responseString = await appPayService.TradePayStringResponse(input);
                return responseString;
            }
        }

        /// <summary>支付宝网站支付
        /// </summary>
        static async Task<PageTradePayResponse> AlipayPageTradePay()
        {
            var pagePayService = IocManager.GetContainer().Resolve<IAlipayPagePayService>();
            using (pagePayService.Use(_alipayConfig.GetByName("App1")))
            {
                var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
                {
                    ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
                };

                return await pagePayService.TradePay(input);
            }
        }



    }
}
