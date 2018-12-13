using DotCommon.AutoMapper;
using DotCommon.Threading;
using Microsoft.Extensions.Logging;
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
        /// <summary>Ctor
        /// </summary>
        public WechatMiniProgramPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>小程序支付统一下单
        /// </summary>
        public async Task<MiniProgramUnifiedOrderCallResponse> UnifiedOrder(MiniProgramUnifiedOrderInput input)
        {
            var request = input.MapTo<MiniProgramUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<MiniProgramUnifiedOrderResponse>(request, App);

            //响应与执行都成功
            if (response.ReturnSuccess && response.ResultSuccess)
            {
                //请求执行成功,需要组合参数给接口
                var prepayId = response.PrepayId;

                var miniProgramUnifiedOrderCallRequest = new MiniProgramUnifiedOrderCallRequest(response.PrepayId);
                var miniProgramUnifiedOrderCallResponse = await Executer.SignRequest<MiniProgramUnifiedOrderCallResponse>(miniProgramUnifiedOrderCallRequest, App);
                return miniProgramUnifiedOrderCallResponse;
            }
            Logger.LogError($"微信小程序下单请求出错,ReturnMsg:{response.ReturnMsg},ErrorCodeMsg:{response.ErrCodeDes}");
            throw new Exception(response.ReturnMsg);
        }
    }
}
