using System.Threading.Tasks;

namespace QuickPay.PayAux.Store
{
    public interface IPaymentStore
    {
        /// <summary>创建或者修改支付信息
        /// </summary>
        Task CreateOrUpdateAsync(Payment payment);

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo);

        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        Task<Payment> GetByUniqueIdAsync(string uniqueId);
    }
}
