using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>转账信息存储
    /// </summary>
    public interface ITransferStore
    {
        /// <summary>创建或者修改转账信息
        /// </summary>
        Task CreateOrUpdateAsync(Transfer transfer);

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        Task<Transfer> GetAsync(int payPlatId, string appId, string outTradeNo);

        /// <summary>根据平台Id,AppId,微信支付宝返回的交易号,获取数据
        /// </summary>
        Task<Transfer> GetByTransferNo(int payPlatId, string appId, string transferNo);

        /// <summary>根据UniqueId获取转账信息
        /// </summary>
        Task<Transfer> GetByUniqueIdAsync(string uniqueId);
    }
}
