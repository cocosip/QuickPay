using System;

namespace QuickPay.Notify
{
    /// <summary>支付宝支付后异步通知
    /// </summary>
    public class AlipayPaymentNotify : AlipayNotify
    {
        /// <summary>异步通知地址
        /// </summary>
        public override string NotifyUrlFragments => QuickPaySettings.AlipayNotifyUrlFragments.PaymentUrlFragments;

        /// <summary>Ctor
        /// </summary>
        public AlipayPaymentNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

    }
}
