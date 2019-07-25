using DotCommon.AutoMapper;
using DotCommon.Threading;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>刷卡支付
    /// </summary>
    public class WeChatMicroPayService : BaseWeChatPayService, IWeChatMicroPayService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatMicroPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>刷卡支付提交订单
        /// </summary>
        public async Task<MicropayUnifiedOrderResponse> UnifiedOrder(MicropayUnifiedOrderInput input)
        {
            var request = ObjectMapper.Map<MicropayUnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<MicropayUnifiedOrderResponse>(request, App);
            return response;
        }
    }
}
