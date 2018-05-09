using QuickPay.Alipay.Apps;
using System;

namespace QuickPay.Alipay.Services
{
    public interface IAlipayService
    {
        IDisposable Use(AlipayApp app);
        AlipayApp App { get; }
    }
}
