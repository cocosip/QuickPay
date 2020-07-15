using DotCommon.Caching;
using QuickPay.WeChat.Frame.Model;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Infrastructure
{
    /// <summary>微信SDK缓存存储
    /// </summary>
    public class WeChatSdkTicketMemoryStore : BaseWeChatMemoryStore, IWeChatSdkTicketStore
    {
        private readonly IDistributedCache<SdkTicketInfo> _sdkTicketCache;

        /// <summary>Ctor
        /// </summary>
        public WeChatSdkTicketMemoryStore(IDistributedCache<SdkTicketInfo> sdkTicketCache)
        {
            _sdkTicketCache = sdkTicketCache;
        }

        /// <summary>获取SdkTicket
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="ticketType">类型</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<SdkTicketInfo> GetSdkTicketAsync(string appId, string ticketType, string tenantId = "")
        {
            var key = FormatCacheKey(tenantId, appId);
            return await _sdkTicketCache.GetAsync(key);
        }

        /// <summary>创建或者修改SdkTicket信息
        /// </summary>
        /// <param name="sdkTicket"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateSdkTicketAsync(SdkTicketInfo sdkTicket)
        {
            var key = FormatCacheKey(sdkTicket.TenantId, sdkTicket.AppId);
            await _sdkTicketCache.SetAsync(key, sdkTicket);
        }

    }
}
