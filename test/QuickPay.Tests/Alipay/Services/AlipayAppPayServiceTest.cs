using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using Xunit;

namespace QuickPay.Tests.Alipay.Services
{
    /// <summary>支付宝App支付
    /// </summary>
    public class AlipayAppPayServiceTest : TestBase
    {
        [Fact]
        public void TradePay_Test()
        {
            var appPayService = Provider.GetService<IAlipayAppPayService>();
            using (appPayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new AppTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1");
                var response = AsyncHelper.RunSync(() => appPayService.TradePay(input));
                Assert.Equal(appPayService.App.AppId, response.AppId);
            }
        }

        [Fact]
        public void TradePayStringResponse_Test()
        {
            var appPayService = Provider.GetService<IAlipayAppPayService>();
            using (appPayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new AppTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1");
                var responseString = AsyncHelper.RunSync(() => appPayService.TradePayStringResponse(input));
                Assert.NotEmpty(responseString);
            }
        }




    }
}
