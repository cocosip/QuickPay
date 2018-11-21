using QuickPay.WechatPay.Apps;
using System;

namespace QuickPay.WechatPay.Services
{
    public interface IWechatPayService
    {
        IDisposable Use(WechatPayApp app);

        WechatPayApp App { get; }
    }
}
