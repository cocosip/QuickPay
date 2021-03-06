﻿using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public interface IPaymentStore
    {
        /// <summary>创建或者修改支付信息
        /// </summary>
        Task CreateOrUpdateAsync(Payment payment);

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo);

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        Task<Payment> GetByTransactionId(int payPlatId, string appId, string transactionId);

        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        Task<Payment> GetByUniqueIdAsync(string uniqueId);
    }
}
