using DotCommon.Caching;
using QuickPay.WeChat.Frame.Model;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Infrastructure
{
    /// <summary>微信AccessToken存储
    /// </summary>
    public class WeChatAccessTokenMemoryStore : BaseWeChatMemoryStore, IWeChatAccessTokenStore
    {
        private readonly IDistributedCache<AccessTokenInfo> _accessTokenCache;

        /// <summary>Ctor
        /// </summary>
        public WeChatAccessTokenMemoryStore(IDistributedCache<AccessTokenInfo> accessTokenCache)
        {
            _accessTokenCache = accessTokenCache;
        }


        /// <summary>获取AccessToken
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<AccessTokenInfo> GetAccessTokenAsync(string appId, string tenantId = "")
        {
            var key = FormatCacheKey(tenantId, appId);
            var accessToken = await _accessTokenCache.GetAsync(key);
            return accessToken;
        }

        /// <summary>创建或者修改AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateAccessTokenAsync(AccessTokenInfo accessToken)
        {
            var key = FormatCacheKey(accessToken.TenantId, accessToken.AppId);
            await _accessTokenCache.SetAsync(key, accessToken);
        }

    }
}
