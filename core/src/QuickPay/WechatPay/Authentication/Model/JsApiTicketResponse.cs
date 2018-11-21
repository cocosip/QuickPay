using Newtonsoft.Json;

namespace QuickPay.WechatPay.Authentication
{
    public class JsApiTicketResponse
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
