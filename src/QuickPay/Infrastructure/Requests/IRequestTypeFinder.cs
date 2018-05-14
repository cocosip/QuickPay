using System;
using System.Collections.Generic;

namespace QuickPay.Infrastructure.Requests
{
    public interface IRequestTypeFinder
    {
        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindPaymentStoreTypies();

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindPaymentStoreTypies(Func<Type, bool> selector);

        /// <summary>获取需要进行退款存储的
        /// </summary>
        List<Type> FindRefundStoreTypies();

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        List<Type> FindRefundStoreTypies(Func<Type, bool> selector);
    }
}
