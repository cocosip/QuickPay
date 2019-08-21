using QuickPay.Alipay.Apps;
using System;

namespace QuickPay.Alipay.Services
{
    /// <summary>支付宝接口
    /// </summary>
    public interface IAlipayService
    {
        /// <summary>Use
        /// </summary>
        IDisposable Use(AlipayConfig config);

        /// <summary>Use
        /// </summary>
        IDisposable Use(AlipayConfig config, string appName);

        /// <summary>Use
        /// </summary>
        IDisposable Use(AlipayConfig config, Func<AlipayConfig, AlipayApp> predicate);

        /// <summary>支付宝应用
        /// </summary>
        AlipayApp App { get; }
    }
}
