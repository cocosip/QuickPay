using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{

    /// <summary>微信支付异步通知
    /// </summary>
    public class WechatPaymentNotify : WechatPayNotify
    {
        /// <summary>微信支付时候的异步通知
        /// </summary>
        public override string NotifyUrlFragments => QuickPaySettings.WechatPayNotifyUrlFragments.PaymentUrlFragments;


        /// <summary>Ctor
        /// </summary>
        public WechatPaymentNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }


    }
}
