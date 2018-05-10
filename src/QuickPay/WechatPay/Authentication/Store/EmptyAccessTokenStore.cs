using DotCommon.Dependency;
using DotCommon.Logging;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class EmptyAccessTokenStore : IAccessTokenStore
    {
        private ILogger Logger { get; }
        public EmptyAccessTokenStore()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }

        public static EmptyAccessTokenStore Instance()
        {
            return new EmptyAccessTokenStore();
        }

        public Task CreateOrUpdateAccessTokenAsync(AccessToken info)
        {
            Logger.Warn($"未实现方法:EmptyAccessTokenStore.CreateOrUpdateAccessTokenAsync");
            return Task.FromResult(0);
        }

        public Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            Logger.Warn($"未实现方法:EmptyAccessTokenStore.GetAccessTokenAsync");
            return Task.FromResult<AccessToken>(null);
        }
    }
}
