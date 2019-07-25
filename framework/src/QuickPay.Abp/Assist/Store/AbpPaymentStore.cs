using Abp.Domain.Repositories;
using DotCommon.ObjectMapping;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class AbpPaymentStore : IPaymentStore
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<AbpPayment> _abpPaymentRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpPaymentStore(IObjectMapper objectMapper, IRepository<AbpPayment> abpPaymentRepository)
        {
            _objectMapper = objectMapper;
            _abpPaymentRepository = abpPaymentRepository;
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.UniqueId == payment.UniqueId && x.PayPlatId == payment.PayPlatId);
            if (abpPayment == null)
            {
                //Create
                abpPayment = _objectMapper.Map<AbpPayment>(payment);
                await _abpPaymentRepository.InsertAsync(abpPayment);
            }
            else
            {
                _objectMapper.Map(payment, abpPayment);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return _objectMapper.Map<Payment>(abpPayment);
        }

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        public async Task<Payment> GetByTransactionId(int payPlatId, string appId, string transactionId)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransactionId == transactionId);
            return _objectMapper.Map<Payment>(abpPayment);
        }


        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return _objectMapper.Map<Payment>(abpPayment);
        }

    }
}
