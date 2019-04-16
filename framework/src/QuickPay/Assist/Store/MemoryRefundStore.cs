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
        private const string RefundListKey = "QuickPay.RefundList";
        private readonly IDistributedCache<List<Refund>> _refundTableCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryRefundStore(IDistributedCache<List<Refund>> refundTableCache)
        {
            _refundTableCache = refundTableCache;
        }

        private async Task<List<Refund>> GetList()
        {
            var refundList = (await _refundTableCache.GetAsync(RefundListKey)) ?? new List<Refund>();
            return refundList;
        }
        private async Task UpdateList(List<Refund> refundList)
        {
            await _refundTableCache.SetAsync(RefundListKey, refundList);
        }

        /// <summary>创建或者修改退款信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Refund refund)
        {
            var refundList = await GetList();
            refundList.Add(refund);
            await UpdateList(refundList);
        }

        /// <summary>根据平台Id,AppId,退款交易号,获取退款信息
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var refundList = await GetList();
            var refund = refundList.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutRefundNo == outRefundNo);
            return refund;
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            var refundList = await GetList();
            var refund = refundList.FirstOrDefault(x => x.UniqueId == uniqueId);
            return refund;
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var refundList = await GetList();
            var refunds = refundList.Where(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo).ToList();
            return refunds;
        }


    }
}
