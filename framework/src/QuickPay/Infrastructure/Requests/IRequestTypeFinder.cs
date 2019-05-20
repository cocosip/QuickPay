using System;
using System.Collections.Generic;

namespace QuickPay.Infrastructure.Requests
{
    /// <summary>请求类型查询器
    /// </summary>
    public interface IRequestTypeFinder
    {
        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindPaymentStoreTypes();

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindPaymentStoreTypes(Func<Type, bool> selector);

        /// <summary>获取需要进行退款存储的
        /// </summary>
        List<Type> FindRefundStoreTypes();

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindRefundStoreTypes(Func<Type, bool> selector);

        /// <summary>获取需要进行转账存储的类型
        /// </summary>
        List<Type> FindTransferTypes();

        /// <summary>获取需要进行转账存储的类型
        /// </summary>
        List<Type> FindTransferTypes(Func<Type, bool> selector);
    }
}
