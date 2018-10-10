using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using Xunit;

namespace QuickPay.Tests.Alipay.Services
{
    /// <summary>支付宝PC网站支付
    /// </summary>
    public class AlipayPagePayServiceTest : TestBase
    {
        [Fact]
        public void TradePay_Test()
        {
            var pagePayService = Provider.GetService<IAlipayPagePayService>();
            using (pagePayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
                {
                    ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
                };
                var responseString = AsyncHelper.RunSync(() => pagePayService.TradePay(input));
                Assert.Equal(pagePayService.App.AppId, responseString.AppId);
            }
        }

        [Fact]
        public void TradePayStringResponse_Test()
        {
            var pagePayService = Provider.GetService<IAlipayPagePayService>();
            using (pagePayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
                {
                    ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
                };
                var responseString = AsyncHelper.RunSync(() => pagePayService.TradePayStringResponse(input));
                Assert.NotEmpty(responseString);
            }
        }
    }
}
