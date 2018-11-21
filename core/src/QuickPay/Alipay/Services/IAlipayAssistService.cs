using QuickPay.Infrastructure.RequestData;
using QuickPay.Assist;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    public interface IAlipayAssistService
    {
        /// <summary>签名验证
        /// </summary>
        Task<bool> VerifySign(PayData payData);

        /// <summary>支付成功
        /// </summary>
        Task PaySuccess(PayData payData, Action<PayData, Payment> action = null);
    }
}
