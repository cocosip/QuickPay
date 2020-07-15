using DotCommon.Caching;
using Moq;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Model;
using QuickPay.WeChat.Frame.Service;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuickPay.WeChat.Frame.Tests.Infrastructure
{
    public class WeChatAccessTokenMemoryStoreTest
    {
        public WeChatAccessTokenMemoryStoreTest()
        {

        }


        [Fact]
        public async Task GetAccessTokenAsync_Test()
        {
            Mock<IDistributedCache<AccessTokenInfo>> mockAccessTokenCache = new Mock<IDistributedCache<AccessTokenInfo>>();
            mockAccessTokenCache.Setup(x => x.GetAsync("QuickPay.WeChat.Frame:2#AppId1", default))
            .ReturnsAsync(new AccessTokenInfo()
            {
                TenantId = "1",
                AppId = "App1",
                Token = "Token:0000",
                ExpiredIn = 1800,
                UpdateTime = new DateTime(2020, 05, 01)
            });

            IWeChatAccessTokenStore weChatAccessTokenStore = new WeChatAccessTokenMemoryStore(mockAccessTokenCache.Object);

            var accessTokenInfo = await weChatAccessTokenStore.GetAccessTokenAsync("AppId1", "2");
            Assert.Equal("1", accessTokenInfo.TenantId);
            Assert.Equal("App1", accessTokenInfo.AppId);
            Assert.Equal(1800, accessTokenInfo.ExpiredIn);
            Assert.Equal(new DateTime(2020, 05, 01), accessTokenInfo.UpdateTime);

        }

        [Fact]
        public async Task CreateOrUpdateAccessTokenAsync_Test()
        {

            var accessToken = new AccessTokenInfo()
            {
                TenantId = "2",
                AppId = "3",
                ExpiredIn = 1200,
                Token = "Token:00001",
                UpdateTime = new DateTime(2020, 05, 01)
            };

            Mock<IDistributedCache<AccessTokenInfo>> mockAccessTokenCache = new Mock<IDistributedCache<AccessTokenInfo>>();

            mockAccessTokenCache.Setup(x => x.SetAsync(It.IsAny<string>(), accessToken, default, default));

            IWeChatAccessTokenStore weChatAccessTokenStore = new WeChatAccessTokenMemoryStore(mockAccessTokenCache.Object);

            await weChatAccessTokenStore.CreateOrUpdateAccessTokenAsync(accessToken);

            mockAccessTokenCache.Verify(x => x.SetAsync(It.IsAny<string>(), accessToken, default, default), Times.Once());
        }




    }
}
