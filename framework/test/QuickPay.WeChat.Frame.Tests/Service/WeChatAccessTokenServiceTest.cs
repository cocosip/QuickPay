using DotCommon.Json4Net;
using DotCommon.Serializing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Model;
using QuickPay.WeChat.Frame.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuickPay.WeChat.Frame.Tests.Service
{
    public class WeChatAccessTokenServiceTest : TestBase
    {

        private readonly Mock<ILogger<WeChatFrameServiceBase>> _mockLogger;

        public WeChatAccessTokenServiceTest()
        {
            _mockLogger = new Mock<ILogger<WeChatFrameServiceBase>>();
        }


        [Fact]
        public async Task GetRemoteAccessTokenAsync_Test()
        {
            var weChatAccessTokenService = BuildRemoteAccessTokenService(new Mock<IWeChatAccessTokenStore>().Object, HttpStatusCode.OK, new AccessToken()
            {
                Token = "Token1",
                ExpiresIn = 1900
            });

            var accessToken = await weChatAccessTokenService.GetRemoteAccessTokenAsync("app1", "appSecret1");

            Assert.Equal("Token1", accessToken.Token);
            Assert.Equal(1900, accessToken.ExpiresIn);
        }

        [Fact]
        private void GetRemoteAccessTokenAsync_Exception_Test()
        {
            var weChatAccessTokenService = BuildRemoteAccessTokenService(new Mock<IWeChatAccessTokenStore>().Object, HttpStatusCode.InternalServerError, new AccessToken()
            {
                Token = "Token2",
                ExpiresIn = 3600
            });

            Assert.Throws<AggregateException>(() =>
            {
                weChatAccessTokenService.GetRemoteAccessTokenAsync("app4", "appSecret4").Wait();
            });
        }


        [Fact]
        public async Task GetAccessTokenAsync_CacheToken_NotExpired_Test()
        {
            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockWeChatAccessTokenStore = new Mock<IWeChatAccessTokenStore>();
            mockWeChatAccessTokenStore
                .Setup(x => x.GetAccessTokenAsync("AppId1", "TenantId1"))
                .Returns(Task.FromResult(new AccessTokenInfo()
                {
                    TenantId = "TenantId2",
                    AppId = "AppId2",
                    ExpiredIn = 3600,
                    Token = "AccessToken2",
                    UpdateTime = DateTime.Now.AddSeconds(-100)
                }));

            IWeChatAccessTokenService weChatAccessTokenService = new WeChatAccessTokenService(mockServiceProvider.Object, _mockLogger.Object, mockWeChatAccessTokenStore.Object);

            var token = await weChatAccessTokenService.GetAccessTokenAsync("AppId1", "Secret1", "TenantId1");
            Assert.Equal("AccessToken2", token);
        }


        [Fact]
        public async Task GetAccessTokenAsync_CacheToken_NullOrExpired_Test()
        {

            var mockWeChatAccessTokenStore = new Mock<IWeChatAccessTokenStore>();
            mockWeChatAccessTokenStore
                .Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((AccessTokenInfo)null);

            mockWeChatAccessTokenStore.Setup(x => x.CreateOrUpdateAccessTokenAsync(It.Is<AccessTokenInfo>(x => x.AppId == "app123" && x.ExpiredIn == 1800 && x.TenantId == "tenatId123")))
                ;

            var weChatAccessTokenService = BuildRemoteAccessTokenService(mockWeChatAccessTokenStore.Object, HttpStatusCode.OK, new AccessToken()
            {
                Token = "Token1",
                ExpiresIn = 1800
            });
            var accessToken = await weChatAccessTokenService.GetAccessTokenAsync("appxxx", "appSecretxxx", "tenatIdxxx");
            Assert.Equal("Token1", accessToken);

            mockWeChatAccessTokenStore.Verify(x => x.CreateOrUpdateAccessTokenAsync(It.IsAny<AccessTokenInfo>()), Times.Once);

            mockWeChatAccessTokenStore = new Mock<IWeChatAccessTokenStore>();
            mockWeChatAccessTokenStore
                .Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new AccessTokenInfo()
                {
                    AppId = "xxx",
                    ExpiredIn = 3600,
                    Token = "xxx",
                    UpdateTime = DateTime.Now.AddSeconds(-7000)
                });

            mockWeChatAccessTokenStore.Setup(x => x.CreateOrUpdateAccessTokenAsync(It.Is<AccessTokenInfo>(x => x.AppId == "appxxx" && x.ExpiredIn == 1800 && x.TenantId == "tenatIdxxx")))
              ;

            weChatAccessTokenService = BuildRemoteAccessTokenService(mockWeChatAccessTokenStore.Object, HttpStatusCode.OK, new AccessToken()
            {
                Token = "Token33",
                ExpiresIn = 3600
            });
            accessToken = await weChatAccessTokenService.GetAccessTokenAsync("appxxx", "appSecretxxx", "tenatIdxxx");
            Assert.Equal("Token33", accessToken);
        }

        private IWeChatAccessTokenService BuildRemoteAccessTokenService(IWeChatAccessTokenStore weChatAccessTokenStore, HttpStatusCode httpStatusCode, AccessToken accessToken)
        {
            //var url = "https://api.weixin.qq.com/cgi-bin/token?appid=app1&secret=appSecret1&grant_type=client_credential";

            var httpClient = BuildMockHttpClient(new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent($"{{\"access_token\":\"{accessToken.Token}\",\"expires_in\":{accessToken.ExpiresIn}}}")
            });

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var mockProvider = new Mock<IServiceProvider>();
            mockProvider.Setup(x => x.GetService(typeof(IHttpClientFactory)))
                .Returns(mockHttpClientFactory.Object);

            mockProvider.Setup(x => x.GetService(typeof(IJsonSerializer)))
                .Returns(DefaultNewtonsoftJsonSerializer);

            IWeChatAccessTokenService weChatAccessTokenService = new WeChatAccessTokenService(mockProvider.Object, _mockLogger.Object, weChatAccessTokenStore);
            return weChatAccessTokenService;
        }
    }
}
