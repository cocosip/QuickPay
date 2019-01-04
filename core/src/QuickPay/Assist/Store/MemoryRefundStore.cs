using DotCommon.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>退款存储
    /// </summary>
    public class MemoryRefundStore : IRefundStore
    {
        private const string RefundTableKey = "QuickPay.RefundTable";
        private readonly IDistributedCache<List<Refund>> _refundTableCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryRefundStore(IDistributedCache<List<Refund>> refundTableCache)
        {
            _refundTableCache = refundTableCache;
        }

        private async Task<List<Refund>> GetTable()
        {
            var table = await _refundTableCache.GetAsync(RefundTableKey);
            if (table == null)
            {
                table = new CacheTable<Refund>();
            }
            return table;
        }
        private async Task UpdateTable(List<Refund> paymentTable)
        {
            await _refundTableCache.SetAsync(RefundTableKey, paymentTable);
        }

        /// <summary>创建或者修改退款信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Refund refund)
        {
            var refundTable = await GetTable();
            refundTable.Add(refund);
            await UpdateTable(refundTable);
        }

        /// <summary>根据平台Id,AppId,退款交易号,获取退款信息
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var refundTable = await GetTable();
            var refund = refundTable.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutRefundNo == outRefundNo);
            return refund;
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            var refundTable = await GetTable();
            var refund = refundTable.FirstOrDefault(x => x.UniqueId == uniqueId);
            return refund;
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var refundTable = await GetTable();
            var refunds = refundTable.Where(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo).ToList();
            return refunds;
        }


    }
}
