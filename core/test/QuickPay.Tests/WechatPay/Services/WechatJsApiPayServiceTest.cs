using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using Xunit;

namespace QuickPay.Tests.WechatPay.Services
{
    public class WechatJsApiPayServiceTest : TestBase
    {
        [Fact]
        public void UnifiedOrder_Test()
        {
            var jsApiService = Provider.GetService<IWeChatJsApiPayService>();
            using (jsApiService.Use(WechatPayConfig.GetByName("App2")))
            {
                var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "opaInxF28ub-ea5JVrZOosDHyXZY");
                var response = AsyncHelper.RunSync(() => jsApiService.UnifiedOrder(input));
                Assert.Equal(response.AppId, jsApiService.App.AppId);
            }
        }

        [Fact]
        public void GetJsSdkConfig_Test()
        {
            var jsApiService = Provider.GetService<IWeChatJsApiPayService>();
            using (jsApiService.Use(WechatPayConfig.GetByName("App1")))
            {
                //var response = AsyncHelper.RunSync(() => jsApiService.GetJsSdkConfig(""));
                Assert.Throws<Exception>(() =>
                {
                    AsyncHelper.RunSync(() => jsApiService.GetJsSdkConfig("http://127.0.0.1"));
                });
                //Assert.Equal(response.AppId, jsApiService.App.AppId);
            }
        }
    }
}
