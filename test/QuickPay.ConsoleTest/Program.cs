using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Threading;
using DotCommon.Utility;
using QuickPay.Alipay.Apps;
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
            //微信App下单
            //AsyncHelper.RunSync(() =>
            //{
            //    return AppUnifiedorder();
            //});

            _stopwatch.Stop();

            _logger.Info(_stopwatch.Elapsed);


            Console.ReadLine();
        }

        /// <summary>App统一下单
        /// </summary>
        static async Task AppUnifiedorder()
        {
            var appService = IocManager.GetContainer().Resolve<IWechatAppPayService>();
            using (appService.Use(_wechatPayConfig.GetByName("App1")))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                await appService.UnifiedOrder(input);
            }
        }




    }
}
