using DotCommon.Caching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>退款存储
    /// </summary>
    public class MemoryRefundStore : IRefundStore
    {
        private readonly IDistributedCache<Refund> _refundCache;
        private readonly IDistributedCache<RefundMapperItem> _refundMapperItemCache;
        private readonly IDistributedCache<RefundTradeMapperItem> _refundTradeMapperItemCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryRefundStore(IDistributedCache<Refund> refundCache, IDistributedCache<RefundMapperItem> refundMapperItemCache, IDistributedCache<RefundTradeMapperItem> refundTradeMapperItemCache)
        {
            _refundCache = refundCache;
            _refundMapperItemCache = refundMapperItemCache;
            _refundTradeMapperItemCache = refundTradeMapperItemCache;
        }

        /// <summary>创建或者修改退款信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Refund refund)
        {
            await _refundCache.SetAsync(FormatRefundKey(refund.UniqueId), refund);
            //item
            var refundMapperItem = new RefundMapperItem(refund.UniqueId, refund.PayPlatId, refund.AppId, refund.OutRefundNo);
            await _refundMapperItemCache.SetAsync(refundMapperItem.GetFormatKey(), refundMapperItem);
            //trade
            var refundTradeMapperItem = new RefundTradeMapperItem(refund.PayPlatId, refund.AppId, refund.OutTradeNo);
            var queryRefundTradeMapperItem = await _refundTradeMapperItemCache.GetAsync(refundTradeMapperItem.GetFormatKey());
            if (queryRefundTradeMapperItem == null)
            {
                queryRefundTradeMapperItem = new RefundTradeMapperItem(refund.PayPlatId, refund.AppId, refund.OutTradeNo);
            }
            queryRefundTradeMapperItem.UniqueIds.Add(refund.UniqueId);
            await _refundTradeMapperItemCache.SetAsync(queryRefundTradeMapperItem.GetFormatKey(), queryRefundTradeMapperItem);

        }

        /// <summary>根据平台Id,AppId,退款交易号,获取退款信息
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var refundMapperItem = new RefundMapperItem("", payPlatId, appId, outRefundNo);
            var queryRefundMapperItem = await _refundMapperItemCache.GetAsync(refundMapperItem.GetFormatKey());
            if (queryRefundMapperItem != null)
            {
                return await GetByUniqueIdAsync(queryRefundMapperItem.UniqueId);
            }
            return default(Refund);
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            return await _refundCache.GetAsync(FormatRefundKey(uniqueId));
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var refunds = new List<Refund>();
            //trade
            var refundTradeMapperItem = new RefundTradeMapperItem(payPlatId, appId, outTradeNo);
            var queryRefundTradeMapperItem = await _refundTradeMapperItemCache.GetAsync(refundTradeMapperItem.GetFormatKey());
            if (queryRefundTradeMapperItem != null)
            {
                foreach (var uniqueId in queryRefundTradeMapperItem.UniqueIds)
                {
                    refunds.Add(await GetByUniqueIdAsync(uniqueId));
                }
            }
            return refunds;
        }


        /// <summary>支付存储的Key
        /// </summary>
        private string FormatRefundKey(string uniqueId)
        {
            return $"QuickPay:Refund:{uniqueId}";
        }
    }
}
