using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>
    /// </summary>
    public class EmptyJsApiTicketStore : IJsApiTicketStore
    {
        private readonly ILogger _logger;
        /// <summary>
        /// </summary>
        public EmptyJsApiTicketStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger= logger;
        }


        /// <summary>
        /// </summary>
        public Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket)
        {
            _logger.LogWarning($"未实现方法:EmptyJsApiTicketStore.CreateOrUpdateJsApiTicketAsync");
            return Task.FromResult(0);
        }

        /// <summary>
        /// </summary>
        public Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            _logger.LogWarning($"未实现方法:EmptyJsApiTicketStore.GetJsApiTicketAsync");
            return Task.FromResult<JsApiTicket>(null);
        }
    }
}
