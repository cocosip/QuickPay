using DotCommon.Caching;
using Microsoft.Extensions.Logging;
using Moq;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuickPay.WeChat.Frame.Tests.Infrastructure
{
    public class WeChatSdkTicketMemoryStoreTest
    {
        private readonly Mock<IServiceProvider> _mockProvider;
        private readonly Mock<ILogger<BaseWeChatMemoryStore>> _mockLogger;
        public WeChatSdkTicketMemoryStoreTest()
        {
            _mockProvider = new Mock<IServiceProvider>();
            _mockLogger = new Mock<ILogger<BaseWeChatMemoryStore>>();
        }

        [Fact]
        public async Task GetSdkTicketAsync_Test()
        {
            Mock<IDistributedCache<SdkTicketInfo>> mockSdkTicketCache = new Mock<IDistributedCache<SdkTicketInfo>>();
            mockSdkTicketCache.Setup(x => x.GetAsync("QuickPay.WeChat.Frame:#AppId3", default))
            .ReturnsAsync(new SdkTicketInfo()
            {
                TenantId = "3",
                AppId = "App1",
                Ticket = "Ticket:0000",
                TicketType = "jsapi",
                ExpiredIn = 1200,
                UpdateTime = new DateTime(2020, 1, 1)
            });

            IWeChatSdkTicketStore weChatSdkTicketStore = new WeChatSdkTicketMemoryStore(mockSdkTicketCache.Object);

            var sdkTicketInfo = await weChatSdkTicketStore.GetSdkTicketAsync("AppId3", "app", "");
            Assert.Equal("3", sdkTicketInfo.TenantId);
            Assert.Equal("App1", sdkTicketInfo.AppId);
            Assert.Equal("Ticket:0000", sdkTicketInfo.Ticket);
            Assert.Equal("jsapi", sdkTicketInfo.TicketType);
            Assert.Equal(1200, sdkTicketInfo.ExpiredIn);
            Assert.Equal(new DateTime(2020, 1, 1), sdkTicketInfo.UpdateTime);
        }


        [Fact]
        public async Task CreateOrUpdateSdkTicketAsync_Test()
        {

            var sdkTicketInfo = new SdkTicketInfo()
            {
                TenantId = "3",
                AppId = "App3",
                ExpiredIn = 1000,
                Ticket = "Ticket:0003",
                TicketType = "jssdk",
                UpdateTime = new DateTime(2020, 05, 01)
            };

            Mock<IDistributedCache<SdkTicketInfo>> mockSdkTicketCache = new Mock<IDistributedCache<SdkTicketInfo>>();

            mockSdkTicketCache.Setup(x => x.SetAsync(It.IsAny<string>(), sdkTicketInfo, default, default));

            IWeChatSdkTicketStore weChatSdkTicketStore = new WeChatSdkTicketMemoryStore(mockSdkTicketCache.Object);

            await weChatSdkTicketStore.CreateOrUpdateSdkTicketAsync(sdkTicketInfo);

            mockSdkTicketCache.Verify(x => x.SetAsync(It.IsAny<string>(), sdkTicketInfo, default, default), Times.Once());
        }


    }
}
