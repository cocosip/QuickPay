using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace QuickPay.ConsoleTest
{
    class Program
    {
        private static ILogger _logger;
        private static Stopwatch _stopwatch = new Stopwatch();
        private static IServiceProvider _provider;
        static void Main(string[] args)
        {

            //初始化
            _provider = Bootstraper.Initialize();
            _logger = _provider.GetService<ILogger<Program>>();
            _logger.LogInformation($"初始化完成");

            var alipayDemoService = _provider.GetService<AlipayDemoService>();
            var weChatPayDemoService = _provider.GetService<WeChatPayDemoService>();

            _stopwatch.Start();

            //微信
            AsyncHelper.RunSync(() =>
            {
                //微信App下单
                //return weChatPayDemoService.WeChatAppUnifiedOrder();

                ////微信JsApi下单
                //return weChatPayDemoService.WeChatJsApiUnifiedOrder();

                //微信小程序下单
                return weChatPayDemoService.WeChatMiniProgramUnifiedOrder();

                //微信订单查询
                //return weChatPayDemoService.WeChatOrderQuery();

            });

            //支付宝
            //AsyncHelper.RunSync(() =>
            //{
            //    ////支付宝App下单
            //    //return alipayDemoService.AlipayAppTradePay();
            //    //支付宝PC下单
            //    return alipayDemoService.AlipayPageTradePay();
            //    ////支付宝订单查询
            //    //return alipayDemoService.Query();
            //    ////支付宝退款查询
            //    //return alipayDemoService.RefundQuery();
            //    ////支付宝账单下载
            //    //return alipayDemoService.BillDownloadUrl();
            //});


            _stopwatch.Stop();

            _logger.LogInformation(_stopwatch.Elapsed.ToString());


            Console.ReadLine();
        }







    }
}
