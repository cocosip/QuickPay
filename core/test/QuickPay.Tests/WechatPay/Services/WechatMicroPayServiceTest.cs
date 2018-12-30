using DotCommon.Threading;
using DotCommon.Utility;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using Xunit;

namespace QuickPay.Tests.WeChatPay.Services
{
    /// <summary>微信刷卡支付
    /// </summary>
    public class WechatMicroPayServiceTest : TestBase
    {
        /// <summary>刷卡支付提交订单
        /// </summary>
        [Fact]
        public void UnifiedOrder_Test()
        {
            var microPayService = Provider.GetService<IWeChatMicroPayService>();
            using (microPayService.Use(WechatPayConfig.GetByName("App2")))
            {
                var input = new MicropayUnifiedOrderInput("刷卡支付1", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "扫码支付授权码，设备读取用户微信中的条码或者二维码信息");
                // var response = AsyncHelper.RunSync(() => microPayService.UnifiedOrder(input));
                Assert.Throws<Exception>(() => AsyncHelper.RunSync(() => microPayService.UnifiedOrder(input)));
            }
        }
    }
}
