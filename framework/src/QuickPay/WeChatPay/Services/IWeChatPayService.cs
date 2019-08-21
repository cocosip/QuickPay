using QuickPay.WeChatPay.Apps;
using System;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信支付接口
    /// </summary>
    public interface IWeChatPayService
    {
        /// <summary>Use
        /// </summary>
        IDisposable Use(string appId);

        /// <summary>Use
        /// </summary>
        IDisposable Use(string configId, string appId);

        /// <summary>Use
        /// </summary>
        IDisposable Use(WeChatPayConfig config, WeChatPayApp app);

        /// <summary>微信支付配置信息
        /// </summary>
        WeChatPayConfig Config { get; }

        /// <summary>微信支付应用
        /// </summary>
        WeChatPayApp App { get; }
    }
}
