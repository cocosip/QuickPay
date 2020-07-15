using DotCommon.Serializing;
using Microsoft.Extensions.Logging;
using Moq;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Model;
using QuickPay.WeChat.Frame.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuickPay.WeChat.Frame.Tests.Service
{
    public class WeChatSdkTicketServiceTest : TestBase
    {

        private readonly Mock<ILogger<WeChatFrameServiceBase>> _mockLogger;
        public WeChatSdkTicketServiceTest()
        {
            _mockLogger = new Mock<ILogger<WeChatFrameServiceBase>>();
        }

        [Fact]
        public void GetRemoteSdkTicketAsync_AccessTokenNullOrEmpty_Test()
        {
            var mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            var mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync("");

            var weChatSdkTicketService = BuildRemoteSdkTicketService(mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object, HttpStatusCode.OK, new SdkTicket());

            Assert.Throws<AggregateException>(() =>
            {
                weChatSdkTicketService.GetRemoteSdkTicketAsync("appId1", "appSecret1", "jsap1", "tenantId1").Wait();
            });

        }

        [Fact]
        public void GetRemoteSdkTicketAsync_HttpResponseException_Test()
        {
            var mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            var mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync("Token1");

            var weChatSdkTicketService = BuildRemoteSdkTicketService(mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object, HttpStatusCode.InternalServerError, new SdkTicket());
            Assert.Throws<AggregateException>(() =>
            {
                weChatSdkTicketService.GetRemoteSdkTicketAsync("appId1", "appSecret1", "jssdk", "tenantId1").Wait();
            });
        }

        [Fact]
        public async Task GetRemoteSdkTicketAsync_Test()
        {
            var mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            var mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync("Token1");

            var weChatSdkTicketService = BuildRemoteSdkTicketService(mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object, HttpStatusCode.OK, new SdkTicket()
            {
                ErrCode = 0,
                ErrMsg = "",
                Ticket = "Ticket2",
                ExpiresIn = 3600,
                Type = "JSSDK"
            });

            var sdkTicket = await weChatSdkTicketService.GetRemoteSdkTicketAsync("appId1", "appSecret1", "jssdk", "telnet1");

            Assert.Equal(0, sdkTicket.ErrCode);
            Assert.Equal("", sdkTicket.ErrMsg);
            Assert.Equal("Ticket2", sdkTicket.Ticket);
            Assert.Equal(3600, sdkTicket.ExpiresIn);
            Assert.Equal("JSSDK", sdkTicket.Type);
        }

        [Fact]
        public async Task GetSdkTicketAsync_CacheTicket_NotExpired_Test()
        {
            var mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            mockWeChatSdkTicketStore.Setup(x => x.GetSdkTicketAsync("vx0001", "JSSDK", "2001"))
             .ReturnsAsync(new SdkTicketInfo()
             {
                 TenantId = "100",
                 AppId = "app2000",
                 ExpiredIn = 3600,
                 Ticket = "ticket2000",
                 TicketType = "JSSDK",
                 UpdateTime = DateTime.Now.AddSeconds(-100)
             });

            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("token1");

            IWeChatSdkTicketService weChatSdkTicketService = new WeChatSdkTicketService(mockServiceProvider.Object, _mockLogger.Object, mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object);
            var ticket = await weChatSdkTicketService.GetSdkTicketAsync("vx0001", "12345678", "jssdk", "2001");
            Assert.Equal("ticket2000", ticket);
        }

        [Fact]
        public async Task GetSdkTicketAsync_CacheTicket_NullOrExpired_Test()
        {
            var mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            mockWeChatSdkTicketStore.Setup(x => x.GetSdkTicketAsync("vx0001", "JSSDK", "2001"))
            .ReturnsAsync((SdkTicketInfo)null);
            mockWeChatSdkTicketStore.Setup(x => x.CreateOrUpdateSdkTicketAsync(It.IsAny<SdkTicketInfo>()));

            var mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ACCESSTOKEN");

            var weChatSdkTicketService = BuildRemoteSdkTicketService(mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object, HttpStatusCode.OK, new SdkTicket()
            {
                ErrCode = 0,
                ErrMsg = "",
                Ticket = "ticket00001",
                ExpiresIn = 1800,
                Type = "JSAPI"
            });

            var ticket = await weChatSdkTicketService.GetSdkTicketAsync("app01", "123", "jsapi", "1");
            Assert.Equal("ticket00001", ticket);
            mockWeChatSdkTicketStore.Verify(x => x.CreateOrUpdateSdkTicketAsync(It.IsAny<SdkTicketInfo>()), Times.Once);

            mockWeChatSdkTicketStore = new Mock<IWeChatSdkTicketStore>();
            mockWeChatSdkTicketStore.Setup(x => x.GetSdkTicketAsync("vx0002", "JSAPI", "2222"))
            .ReturnsAsync(new SdkTicketInfo()
            {
                TenantId = "111",
                AppId = "Appx",
                ExpiredIn = 3600,
                Ticket = "T1",
                TicketType = "sss",
                UpdateTime = DateTime.Now.AddSeconds(-7200)
            });
            mockWeChatSdkTicketStore.Setup(x => x.CreateOrUpdateSdkTicketAsync(It.IsAny<SdkTicketInfo>()));

            mockWeChatAccessTokenService = new Mock<IWeChatAccessTokenService>();
            mockWeChatAccessTokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("accesstoken");

            weChatSdkTicketService = BuildRemoteSdkTicketService(mockWeChatSdkTicketStore.Object, mockWeChatAccessTokenService.Object, HttpStatusCode.OK, new SdkTicket()
            {
                ErrCode = 0,
                ErrMsg = "",
                Ticket = "ticket222",
                ExpiresIn = 1200,
                Type = "JSAPI"
            });

            ticket = await weChatSdkTicketService.GetSdkTicketAsync("app22", "222", "jsapi", "2");
            Assert.Equal("ticket222", ticket);
            mockWeChatSdkTicketStore.Verify(x => x.CreateOrUpdateSdkTicketAsync(It.IsAny<SdkTicketInfo>()), Times.Once);

        }


        private IWeChatSdkTicketService BuildRemoteSdkTicketService(IWeChatSdkTicketStore weChatSdkTicketStore, IWeChatAccessTokenService weChatAccessTokenService, HttpStatusCode httpStatusCode, SdkTicket sdkTicket)
        {
            //var url = "https://api.weixin.qq.com/cgi-bin/token?appid=app1&secret=appSecret1&grant_type=client_credential";

            var httpClient = BuildMockHttpClient(new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent($"{{\"errcode\":{sdkTicket.ErrCode},\"errmsg\":\"{sdkTicket.ErrMsg}\",\"ticket\":\"{sdkTicket.Ticket}\",\"expires_in\":{sdkTicket.ExpiresIn},\"type\":\"{sdkTicket.Type}\"}}")
            });

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var mockProvider = new Mock<IServiceProvider>();
            mockProvider.Setup(x => x.GetService(typeof(IHttpClientFactory)))
                .Returns(mockHttpClientFactory.Object);

            mockProvider.Setup(x => x.GetService(typeof(IJsonSerializer)))
                .Returns(DefaultNewtonsoftJsonSerializer);

            IWeChatSdkTicketService weChatSdkTicketService = new WeChatSdkTicketService(mockProvider.Object, _mockLogger.Object, weChatSdkTicketStore, weChatAccessTokenService);
            return weChatSdkTicketService;
        }

    }
}
