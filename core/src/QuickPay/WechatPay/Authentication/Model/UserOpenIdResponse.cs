using Newtonsoft.Json;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>公众号等用户OpenId返回信息
    /// </summary>
    public class UserOpenIdResponse
    {
        /// <summary>AccessToken
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>用于刷新的Token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>用户OpenId
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>用户授权的作用域
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>UnionId
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
