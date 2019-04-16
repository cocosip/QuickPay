using Newtonsoft.Json;

namespace QuickPay.WeChatPay.Authentication
{
    /// <summary>JsApiTicketResponse
    /// </summary>
    public class JsApiTicketResponse
    {
        /// <summary>JsApiTicket
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        /// <summary>过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
