using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class EmptyJsApiTicketStore : IJsApiTicketStore
    {
        private readonly ILogger _logger;
        public EmptyJsApiTicketStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger= logger;
        }

   

        public Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket)
        {
            _logger.LogWarning($"未实现方法:EmptyJsApiTicketStore.CreateOrUpdateJsApiTicketAsync");
            return Task.FromResult(0);
        }

        public Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            _logger.LogWarning($"未实现方法:EmptyJsApiTicketStore.GetJsApiTicketAsync");
            return Task.FromResult<JsApiTicket>(null);
        }
    }
}
