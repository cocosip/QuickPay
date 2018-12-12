using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
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
        private static IServiceProvider _provider;
        static void Main(string[] args)
        {

            //初始化
            _provider = Bootstraper.Initialize();
            _logger = _provider.GetService<ILogger<QuickPayLoggerName>>();
            _wechatPayConfig = _provider.GetService<WechatPayConfig>();
            _alipayConfig = _provider.GetService<AlipayConfig>();
            _logger.LogInformation($"初始化完成");

            _stopwatch.Start();

            //微信
            AsyncHelper.RunSync(() =>
            {
                //微信App下单
                //return WechatMiniProgramUnifiedOrder();
                //微信JsApi下单
                // return WechatJsApiUnifiedOrder();
                return WechatOrderQuery();
            });

            //支付宝
            // AsyncHelper.RunSync(() =>
            // {
            //     //支付宝App下单
            //     //return AlipayAppTradePay();
            //     //支付宝PC下单
            //     //return AlipayPageTradePay();
            // });


            _stopwatch.Stop();

            _logger.LogInformation(_stopwatch.Elapsed.ToString());


            Console.ReadLine();
        }

        /// <summary>微信App统一下单
        /// </summary>
        static async Task WechatAppUnifiedOrder()
        {
            var appService = _provider.GetService<IWechatAppPayService>();
            using(appService.Use(_wechatPayConfig.GetByName("App1")))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                await appService.UnifiedOrder(input);
            }
        }

        /// <summary>微信JsApi下单
        /// </summary>
        static async Task WechatJsApiUnifiedOrder()
        {
            var jsApiService = _provider.GetService<IWechatJsApiPayService>();
            using(jsApiService.Use(_wechatPayConfig.GetByName("App2")))
            {
                var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "opaInxF28ub-ea5JVrZOosDHyXZY");
                await jsApiService.UnifiedOrder(input);
            }
        }

        /// <summary>微信小程序下单
        /// </summary>
        static async Task WechatMiniProgramUnifiedOrder()
        {
            var miniProgramService = _provider.GetService<IWechatMiniProgramPayService>();
            var authenticationService = _provider.GetService<IAuthenticationService>();
            using(miniProgramService.Use(_wechatPayConfig.GetByName("App3")))
            {
                //var openId = await authenticationService.GetMiniProgramOpenId(miniProgramService.App.AppId, miniProgramService.App.Appsecret, "071FCmZX1ixU011SMv0Y1KAvZX1FCmZo");

                // ofKnT5CCHaMNzinIfiLpPIb3S014
                var input = new MiniProgramUnifiedOrderInput("小程序支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "ofKnT5CCHaMNzinIfiLpPIb3S014");
                await miniProgramService.UnifiedOrder(input);
            }
        }

        /// <summary>微信订单查询
        /// </summary>
        static async Task WechatOrderQuery()
        {
            var wechatPayTradeCommonService = _provider.GetService<IWechatPayTradeCommonService>();
            using(wechatPayTradeCommonService.Use(_wechatPayConfig.GetByName("App3")))
            {
                var response = await wechatPayTradeCommonService.OrderQuery(new OrderQueryInput("123"));
                Console.WriteLine("ReturnSuccess:{0},outTradeNo:{1}", response.ReturnSuccess, response.OutTradeNo);
            }
        }


        /// <summary>支付宝App支付
        /// </summary>
        static async Task<string> AlipayAppTradePay()
        {
            var appPayService = _provider.GetService<IAlipayAppPayService>();
            using(appPayService.Use(_alipayConfig.GetByName("App1")))
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
            var pagePayService = _provider.GetService<IAlipayPagePayService>();
            using(pagePayService.Use(_alipayConfig.GetByName("App1")))
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
