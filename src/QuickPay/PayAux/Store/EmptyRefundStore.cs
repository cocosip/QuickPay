using DotCommon.Dependency;
using DotCommon.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.PayAux.Store
{
    public class EmptyRefundStore : IRefundStore
    {
        private ILogger Logger { get; }

        public static EmptyRefundStore Instance()
        {
            return new EmptyRefundStore();
        }

        public EmptyRefundStore()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }

        public Task CreateOrUpdateAsync(Refund refund)
        {
            Logger.Warn($"未实现方法:EmptyRefundStore.CreateOrUpdateAsync");
            return Task.FromResult(0);
        }

        public Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            Logger.Warn($"未实现方法:EmptyRefundStore.GetAsync");
            return Task.FromResult<Refund>(null);
        }

        public Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            Logger.Warn($"未实现方法:EmptyRefundStore.GetByUniqueIdAsync");
            return Task.FromResult<Refund>(null);
        }

        public Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            Logger.Warn($"未实现方法:EmptyRefundStore.GetRefundsAsync");
            return Task.FromResult<List<Refund>>(null);
        }
    }
}
