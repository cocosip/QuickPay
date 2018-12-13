using QuickPay.WechatPay.Apps;
using System;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信支付接口
    /// </summary>
    public interface IWechatPayService
    {
        /// <summary>Use
        /// </summary>
        IDisposable Use(WechatPayApp app);

        /// <summary>微信支付应用
        /// </summary>
        WechatPayApp App { get; }
    }
}
