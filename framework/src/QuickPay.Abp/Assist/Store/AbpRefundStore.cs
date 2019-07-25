using Abp.Domain.Repositories;
using DotCommon.ObjectMapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>退款存储
    /// </summary>
    public class AbpRefundStore : IRefundStore
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<AbpRefund> _abpRefundRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpRefundStore(IObjectMapper objectMapper, IRepository<AbpRefund> abpRefundRepository)
        {
            _objectMapper = objectMapper;
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
                abpRefund = _objectMapper.Map<AbpRefund>(abpRefund);
                await _abpRefundRepository.InsertAsync(abpRefund);
            }
            else
            {
                _objectMapper.Map(refund, abpRefund);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutRefundNo == outRefundNo);
            return _objectMapper.Map<Refund>(abpRefund);
        }

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        public async Task<Refund> GetByTransactionId(int payPlatId, string appId, string transactionId)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransactionId == transactionId);
            return _objectMapper.Map<Refund>(abpRefund);
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            var abpRefund = await _abpRefundRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return _objectMapper.Map<Refund>(abpRefund);
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpRefunds = await _abpRefundRepository.GetAllListAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return _objectMapper.Map<List<Refund>>(abpRefunds);
        }
    }
}
