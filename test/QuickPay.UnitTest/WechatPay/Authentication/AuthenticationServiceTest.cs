using DotCommon.Dependency;
using QuickPay.WechatPay.Authentication;
using Xunit;

namespace QuickPay.UnitTest.WechatPay.Authentication
{
    public class AuthenticationServiceTest : TestBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationServiceTest()
        {
            _authenticationService = IocManager.GetContainer().Resolve<IAuthenticationService>();
        }

        [Fact]
        public void GetAuthorizationCodeUrlTest()
        {
            var url = _authenticationService.GetAuthorizationCodeUrl("123456", "http://127.0.0.1", state: "state1234");
            Assert.Equal($"https://open.weixin.qq.com/connect/oauth2/authorize?appid=123456&redirect_uri=http%3A%2F%2F127.0.0.1&response_type=code&scope=snsapi_base&state=state1234#wechat_redirect", url);
        }
    }
}
