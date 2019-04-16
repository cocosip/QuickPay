using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>退款存储
    /// </summary>
    public class AbpRefundStore : IRefundStore
    {
        private readonly IRepository<AbpRefund> _abpRefundRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpRefundStore(IRepository<AbpRefund> abpRefundRepository)
        {
            _abpRefundRepository = abpRefundRepository;
        }

        /// <summary>创建或者修改退款信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Refund refund)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.UniqueId == refund.UniqueId && x.PayPlatId == refund.PayPlatId);
            if (abpRefund == null)
            {
                //Create
                abpRefund = abpRefund.MapTo<AbpRefund>();
                await _abpRefundRepository.InsertAsync(abpRefund);
            }
            else
            {
                refund.MapTo(abpRefund);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutRefundNo == outRefundNo);
            return abpRefund.MapTo<Refund>();
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return abpRefund.MapTo<Refund>();
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpRefunds = await _abpRefundRepository.GetAllListAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return abpRefunds.MapTo<List<Refund>>();
        }
    }
}
