using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>支付宝扫码支付
    /// </summary>
    public interface IAlipayQrcodePayService : IAlipayService
    {
        /// <summary>统一收单线下交易预创建
        /// </summary>
        Task<QrcodeTradePayResponse> PrepayCreate(QrcodePayPreCreateInput input);


    }
}
