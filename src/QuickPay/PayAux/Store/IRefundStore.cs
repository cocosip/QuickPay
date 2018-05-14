﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.PayAux.Store
{
    public interface IRefundStore
    {
        /// <summary>创建或者修改退款信息
        /// </summary>
        Task CreateOrUpdateAsync(Refund refund);

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo);

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        Task<Refund> GetByUniqueIdAsync(string uniqueId);

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo);
    }
}