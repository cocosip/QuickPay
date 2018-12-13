using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>JsApiTicket存储
    /// </summary>
    public interface IJsApiTicketStore
    {
        /// <summary>根据应用Id获取JsApiTicket
        /// </summary>
        Task<JsApiTicket> GetJsApiTicketAsync(string appId);

        /// <summary>创建或者修改JsApiTicket
        /// </summary>
        Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket);
    }
}
