using DotCommon.AutoMapper;
using DotCommon.Runtime;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>PC网站支付
    /// </summary>
    public class AlipayPagePayService : BaseAlipayService, IAlipayPagePayService
    {
        public AlipayPagePayService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider) : base(alipayAppOverrideScopeProvider)
        {
        }


        /// <summary>PC网站支付生成订单
        /// </summary>
        public async Task<PageTradePayResponse> TradePay(PageTradePayInput input)
        {
            var bizContentRequest = input.MapTo<PageTradeBizContentPayRequest>();
            var request = new PageTradePayRequest(bizContentRequest, input.ReturnUrl, input.NotifyUrl);
            var response = await Executer.SignRequest<PageTradePayResponse>(request, App);
            return response;
        }

    }
}
