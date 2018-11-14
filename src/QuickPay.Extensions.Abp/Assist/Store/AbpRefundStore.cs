using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    public class AbpRefundStore : IRefundStore
    {
        private readonly IRepository<AbpRefund> _abpRefundRepository;
        public AbpRefundStore(IRepository<AbpRefund> abpRefundRepository)
        {
            _abpRefundRepository = abpRefundRepository;
        }

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

        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutRefundNo == outRefundNo);
            return abpRefund.MapTo<Refund>();
        }

        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return abpRefund.MapTo<Refund>();
        }

        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpRefunds = await _abpRefundRepository.GetAllListAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return abpRefunds.MapTo<List<Refund>>();
        }
    }
}
