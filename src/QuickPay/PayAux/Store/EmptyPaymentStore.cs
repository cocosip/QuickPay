using DotCommon.Dependency;
using DotCommon.Logging;
using System.Threading.Tasks;

namespace QuickPay.PayAux.Store
{
    public class EmptyPaymentStore : IPaymentStore
    {
        private ILogger Logger { get; }

        public static EmptyPaymentStore Instance()
        {
            return new EmptyPaymentStore();
        }
        public EmptyPaymentStore()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }
        public Task CreateOrUpdateAsync(Payment payment)
        {
            Logger.Warn($"未实现方法:EmptyPaymentStore.CreateOrUpdateAsync");
            return Task.FromResult(0);
        }

        public Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            Logger.Warn($"未实现方法:EmptyPaymentStore.GetAsync");
            return Task.FromResult<Payment>(null);
        }

        public Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            Logger.Warn($"未实现方法:EmptyPaymentStore.GetByUniqueIdAsync");
            return Task.FromResult<Payment>(null);
        }

    }
}
