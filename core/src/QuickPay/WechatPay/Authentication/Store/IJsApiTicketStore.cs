using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public interface IJsApiTicketStore
    {
        /// <summary>根据应用Id获取JsApiTicket
        /// </summary>
        Task<JsApiTicket> GetJsApiTicketAsync(string appId);

        /// <summary>修改JsApiTicket
        /// </summary>
        Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket);
    }
}
