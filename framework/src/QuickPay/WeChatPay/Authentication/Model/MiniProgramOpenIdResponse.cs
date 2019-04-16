using Newtonsoft.Json;

namespace QuickPay.WeChatPay.Authentication
{
    /// <summary>小程序用户OpenId返回信息
    /// </summary>
    public class MiniProgramOpenIdResponse
    {
        /// <summary>用户OpenId
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>会话密钥,Session Key
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        /// <summary>用户在开放平台的唯一标识符
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
