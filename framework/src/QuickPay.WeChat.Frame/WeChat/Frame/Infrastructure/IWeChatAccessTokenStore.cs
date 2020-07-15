using QuickPay.WeChat.Frame.Model;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Infrastructure
{
    /// <summary>微信AccessToken存储
    /// </summary>
    public interface IWeChatAccessTokenStore
    {
        /// <summary>获取AccessToken
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        Task<AccessTokenInfo> GetAccessTokenAsync(string appId, string tenantId = "");

        /// <summary>创建或者修改AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task CreateOrUpdateAccessTokenAsync(AccessTokenInfo accessToken);
    }
}
