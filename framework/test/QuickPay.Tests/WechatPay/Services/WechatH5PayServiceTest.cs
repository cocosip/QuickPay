using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using Xunit;

namespace QuickPay.Tests.WeChatPay.Services
{
    /// <summary>微信H5支付
    /// </summary>
    public class WechatH5PayServiceTest : TestBase
    {
        [Fact]
        public void UnifiedOrder_Test()
        {
            var h5PayService = Provider.GetService<IWeChatH5PayService>();
            using (h5PayService.Use("App1"))
            {
                var input = new H5UnifiedOrderInput("微信H5支付", ObjectId.GenerateNewStringId(), 1);
                //微信H5开通没有

                Assert.ThrowsAny<Exception>(() => AsyncHelper.RunSync(() => h5PayService.UnifiedOrder(input)));
                // Assert.Empty(responseUrl);
            }
        }
    }
}
