using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>AccessToken存储
    /// </summary>
    public interface IAccessTokenStore
    {
        /// <summary>根据应用Id获取当前token
        /// </summary>
        Task<AccessToken> GetAccessTokenAsync(string appId);

        /// <summary>创建或者修改AccessToken
        /// </summary>
        Task CreateOrUpdateAccessTokenAsync(AccessToken accessToken);
    }
}
