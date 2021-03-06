﻿using QuickPay.Assist;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>支付宝辅助服务
    /// </summary>
    public interface IAlipayAssistService : IAlipayService
    {
        /// <summary>签名验证
        /// </summary>
        Task<bool> VerifySign(PayData payData);

        /// <summary>支付成功
        /// </summary>
        Task PaySuccess(PayData payData, Action<Payment> action = null);
    }
}
