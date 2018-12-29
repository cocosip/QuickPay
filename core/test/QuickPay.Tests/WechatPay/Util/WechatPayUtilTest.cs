using QuickPay.WeChatPay.Util;
using Xunit;

namespace QuickPay.Tests.WechatPay.Util
{
    public class WechatPayUtilTest
    {
        [Fact]
        public void GenerateTimeStamp_Test()
        {
            var t = WeChatPayUtil.GenerateTimeStamp();
            Assert.NotEmpty(t);
        }

        [Fact]
        public void GenerateNonceStr_Test()
        {
            var nonceStr = WeChatPayUtil.GenerateNonceStr();
            Assert.Equal(32, nonceStr.Length);
        }

    }
}
