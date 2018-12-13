﻿using QuickPay.Alipay.Apps;
using System;

namespace QuickPay.Alipay.Services
{
    /// <summary>支付宝接口
    /// </summary>
    public interface IAlipayService
    {
        /// <summary>Use
        /// </summary>
        IDisposable Use(AlipayApp app);

        /// <summary>支付宝应用
        /// </summary>
        AlipayApp App { get; }
    }
}
