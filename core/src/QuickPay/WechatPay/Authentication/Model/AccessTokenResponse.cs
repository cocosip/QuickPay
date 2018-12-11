using Newtonsoft.Json;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>微信公众号AccessToken返回
    /// </summary>
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
