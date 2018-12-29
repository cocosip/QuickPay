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
        IDisposable Use(WeChatPayApp app);

        /// <summary>微信支付应用
        /// </summary>
        WeChatPayApp App { get; }
    }
}
