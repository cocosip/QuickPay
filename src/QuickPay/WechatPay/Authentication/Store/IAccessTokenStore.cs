using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public interface IAccessTokenStore
    {
        /// <summary>根据应用Id获取当前token
        /// </summary>
        Task<AccessToken> GetAccessTokenAsync(string appId);

        /// <summary>修改AccessToken
        /// </summary>
        Task CreateOrUpdateAccessTokenAsync(AccessToken info);
    }
}
