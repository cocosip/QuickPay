using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>
    /// </summary>
    public class EmptyRefundStore : IRefundStore
    {
        private readonly ILogger _logger;
        /// <summary>
        /// </summary>
        public EmptyRefundStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// </summary>
        public Task CreateOrUpdateAsync(Refund refund)
        {
            _logger.LogWarning($"未实现方法:EmptyRefundStore.CreateOrUpdateAsync");
            return Task.FromResult(0);
        }
        /// <summary>
        /// </summary>
        public Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            _logger.LogWarning($"未实现方法:EmptyRefundStore.GetAsync");
            return Task.FromResult<Refund>(null);
        }
        /// <summary>
        /// </summary>
        public Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            _logger.LogWarning($"未实现方法:EmptyRefundStore.GetByUniqueIdAsync");
            return Task.FromResult<Refund>(null);
        }
        /// <summary>
        /// </summary>
        public Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            _logger.LogWarning($"未实现方法:EmptyRefundStore.GetRefundsAsync");
            return Task.FromResult<List<Refund>>(null);
        }
    }
}
