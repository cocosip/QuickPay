using Newtonsoft.Json;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>微信公众号AccessToken返回
    /// </summary>
    public class AccessTokenResponse
    {
        /// <summary>微信公众号AccessToken
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
