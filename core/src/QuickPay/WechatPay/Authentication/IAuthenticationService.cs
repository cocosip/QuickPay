using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>认证服务
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>根据JsCode获取用户OpenId和UnionId
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="appSecret">应用AppSecret</param>
        /// <param name="jsCode">应用的JsCode</param>
        /// <returns></returns>
        Task<MiniProgramOpenIdResponse> GetMiniProgramOpenId(string appId, string appSecret, string jsCode);

        /// <summary>获取用户Code的Url地址
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="redirectUri">回调地址</param>
        /// <param name="scope">权限</param>
        /// <param name="state">附加状态(a-zA-Z0-9的参数值，最多128字节,如果不为空必须唯一)</param>
        /// <returns></returns>
        string GetAuthorizationCodeUrl(string appId, string redirectUri, string scope = WechatPaySettings.Scope.Base, string state = "");

        /// <summary>获取用户的OpenId
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <param name="code">微信返回的用户Code</param>
        /// <param name="verifyStatus">是否验证状态</param>
        /// <param name="status">前一步提交的状态</param>
        /// <returns></returns>
        Task<string> GetUserOpenIdAsync(string appId, string appSecret, string code, bool verifyStatus = false, string status = "");

        /// <summary>从微信接口获取公众号AccessToken
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        Task<AccessTokenResponse> GetRemoteAccessTokenAsync(string appId, string appSecret);

        /// <summary>获取可用的AccessToken(先从本地存储中获取,如果不存在,就从微信接口获取)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync(string appId, string appSecret);

        /// <summary>从微信接口获取微信JsApiTicket
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        Task<JsApiTicketResponse> GetRemoteJsApiTicketAsync(string appId, string appSecret);

        /// <summary>获取可用的微信JsApiTicket(先从本地存储中获取,如果本地存储不存在就从微信接口获取)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        Task<string> GetJsApiTicketAsync(string appId, string appSecret);
    }
}
