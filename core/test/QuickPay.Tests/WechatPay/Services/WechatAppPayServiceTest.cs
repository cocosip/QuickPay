using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.DTOs;
using Xunit;

namespace QuickPay.Tests.WeChatPay.Services
{
    public class WechatAppPayServiceTest : TestBase
    {
        /// <summary>微信统一下单
        /// </summary>
        [Fact]
        public void UnifiedOrder_Test()
        {
            var appService = Provider.GetService<IWeChatAppPayService>();
            using(appService.Use(WechatPayConfig.GetByName("App1")))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                var response = AsyncHelper.RunSync(() => appService.UnifiedOrder(input));
                Assert.Equal(appService.App.AppId, response.AppId);
            }
        }

    }
}
