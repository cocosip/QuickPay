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
    /// <summary>微信小程序支付
    /// </summary>
    public class WechatMiniProgramPayService : BaseWechatPayService, IWechatMiniProgramPayService
    {
        public WechatMiniProgramPayService(IServiceProvider provider, IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider) : base(provider, wechatPayAppOverrideScopeProvider)
        {

        }

        /// <summary>小程序支付统一下单
        /// </summary>
        public async Task<MiniProgramUnifiedOrderResponse> UnifiedOrder(MiniProgramUnifiedOrderInput input)
        {
            var request = input.MapTo<MiniProgramUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<MiniProgramUnifiedOrderResponse>(request, App);
            return response;
        }
    }
}
