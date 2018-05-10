using DotCommon.Dependency;
using DotCommon.Logging;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class EmptyJsApiTicketStore : IJsApiTicketStore
    {
        private ILogger Logger { get; }

        public EmptyJsApiTicketStore()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }

        public static EmptyAccessTokenStore Instance()
        {
            return new EmptyAccessTokenStore();
        }

        public Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket)
        {
            Logger.Warn($"未实现方法:EmptyJsApiTicketStore.CreateOrUpdateJsApiTicketAsync");
            return Task.FromResult(0);
        }

        public Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            Logger.Warn($"未实现方法:EmptyJsApiTicketStore.GetJsApiTicketAsync");
            return Task.FromResult<JsApiTicket>(null);
        }
    }
}
