using DotCommon.AutoMapper;
using DotCommon.Threading;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>刷卡支付
    /// </summary>
    public class WechatMicroPayService : BaseWechatPayService, IWechatMicroPayService
    {
        public WechatMicroPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>刷卡支付提交订单
        /// </summary>
        public async Task<MicropayUnifiedOrderResponse> UnifiedOrder(MicropayUnifiedOrderInput input)
        {
            var request = input.MapTo<MicropayUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<MicropayUnifiedOrderResponse>(request, App);
            return response;
        }
    }
}
