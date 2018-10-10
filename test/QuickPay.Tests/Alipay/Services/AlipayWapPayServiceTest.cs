using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using Xunit;

namespace QuickPay.Tests.Alipay.Services
{
    /// <summary>支付宝手机网站支付
    /// </summary>
    public class AlipayWapPayServiceTest : TestBase
    {
        [Fact]
        public void TradePay_Test()
        {
            var wapPayService = Provider.GetService<IAlipayWapPayService>();
            using (wapPayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new WapTradePayInput("手机网站支付1", "支付宝手机网站支付", ObjectId.GenerateNewStringId(), "0.1");
                var response = AsyncHelper.RunSync(() => wapPayService.TradePay(input));
                Assert.Equal(wapPayService.App.AppId, response.AppId);
            }
        }

        [Fact]
        public void TradePayStringResponse_Test()
        {
            var wapPayService = Provider.GetService<IAlipayWapPayService>();
            using (wapPayService.Use(AlipayConfig.GetByName("App1")))
            {
                var input = new WapTradePayInput("手机网站支付1", "支付宝手机网站支付", ObjectId.GenerateNewStringId(), "0.1");
                var responseString = AsyncHelper.RunSync(() => wapPayService.TradePayStringResponse(input));
                Assert.NotEmpty(responseString);
            }
        }
    }
}
