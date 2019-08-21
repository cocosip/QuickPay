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
        IDisposable Use(string appId);

        /// <summary>Use
        /// </summary>
        IDisposable Use(string configId, string appId);

        /// <summary>Use
        /// </summary>
        IDisposable Use(AlipayConfig config, AlipayApp app);

        /// <summary>支付宝配置信息
        /// </summary>
        AlipayConfig Config { get; }

        /// <summary>支付宝应用
        /// </summary>
        AlipayApp App { get; }

    }
}
