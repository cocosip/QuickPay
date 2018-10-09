using QuickPay.WechatPay.Util;
using Xunit;

namespace QuickPay.Tests.WechatPay.Util
{
    public class WechatPayUtilTest
    {
        [Fact]
        public void GenerateTimeStamp_Test()
        {
            var t = WechatPayUtil.GenerateTimeStamp();
            Assert.NotEmpty(t);
        }

        [Fact]
        public void GenerateNonceStr_Test()
        {
            var nonceStr = WechatPayUtil.GenerateNonceStr();
            Assert.Equal(32, nonceStr.Length);
        }

    }
}
