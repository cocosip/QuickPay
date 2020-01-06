using QuickPay.WeChatPay.Utility;
using Xunit;

namespace QuickPay.Tests.WeChatPay.Util
{
    public class WeChatPayUtilTest
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
