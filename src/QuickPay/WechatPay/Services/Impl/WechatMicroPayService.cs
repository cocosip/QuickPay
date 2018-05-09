using DotCommon.AutoMapper;
using DotCommon.Runtime;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>扫码支付
    /// </summary>
    public class WechatMicroPayService : BaseWechatPayService, IWechatMicroPayService
    {
        public WechatMicroPayService(IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider) : base(wechatPayAppOverrideScopeProvider)
        {
        }

        /// <summary>扫码支付提交订单
        /// </summary>
        public async Task<MicropayUnifiedOrderResponse> UnifiedOrder(MicropayUnifiedOrderInput input)
        {
            var request = input.MapTo<MicropayUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<MicropayUnifiedOrderResponse>(request, App);
            return response;
        }
    }
}
