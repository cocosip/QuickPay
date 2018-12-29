using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChatPay.Authentication;
using System;
using System.Net;
using Xunit;

namespace QuickPay.Tests.WechatPay.Services
{
    public class AuthenticationServiceTest : TestBase
    {
        [Fact]
        public void GetAuthorizationCodeUrl_Test()
        {
            var authenticationService = Provider.GetService<IAuthenticationService>();
            var url = authenticationService.GetAuthorizationCodeUrl("AppId1", "http://127.0.0.1", state: "STATE1");
            var expceted = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=AppId1&redirect_uri={WebUtility.UrlEncode("http://127.0.0.1")}&response_type=code&scope=snsapi_base&state=STATE1#weChat_redirect";
            Assert.Equal(expceted, url);
        }

        /// <summary>获取AccessToken
        /// </summary>
        [Fact]
        public void GetAccessToken_Test()
        {
            var weChatPayApp = WechatPayConfig.GetByName("App2");
            var authenticationService = Provider.GetService<IAuthenticationService>();
            //未在白名单
            Assert.Throws<Exception>(() =>
            {
                AsyncHelper.RunSync(() => authenticationService.GetAccessTokenAsync(weChatPayApp.AppId, weChatPayApp.Appsecret));
            });

            //var accessToken = AsyncHelper.RunSync(() => authenticationService.GetAccessTokenAsync(weChatPayApp.AppId,weChatPayApp.Appsecret));

           // Assert.NotEmpty(accessToken);
        }
    }
}
