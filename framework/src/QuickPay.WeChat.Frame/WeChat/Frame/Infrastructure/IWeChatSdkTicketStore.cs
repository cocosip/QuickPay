using QuickPay.WeChat.Frame.Model;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Infrastructure
{
    /// <summary>微信SdkTicket存储
    /// </summary>
    public interface IWeChatSdkTicketStore
    {

        /// <summary>获取SdkTicket
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="ticketType">类型</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        Task<SdkTicketInfo> GetSdkTicketAsync(string appId, string ticketType, string tenantId = "");


        /// <summary>创建或者修改SdkTicket信息
        /// </summary>
        /// <param name="sdkTicket"></param>
        /// <returns></returns>
        Task CreateOrUpdateSdkTicketAsync(SdkTicketInfo sdkTicket);
    }
}
