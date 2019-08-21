using Microsoft.Extensions.Logging;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信小程序支付
    /// </summary>
    public class WeChatMiniProgramPayService : BaseWeChatPayService, IWeChatMiniProgramPayService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatMiniProgramPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>小程序支付统一下单
        /// </summary>
        public async Task<MiniProgramUnifiedOrderCallResponse> UnifiedOrder(MiniProgramUnifiedOrderInput input)
        {
            var request = ObjectMapper.Map<MiniProgramUnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<MiniProgramUnifiedOrderResponse>(request, Config, App);

            //响应与执行都成功
            if (response.ReturnSuccess && response.ResultSuccess)
            {
                //请求执行成功,需要组合参数给接口
                var prepayId = response.PrepayId;

                var miniProgramUnifiedOrderCallRequest = new MiniProgramUnifiedOrderCallRequest(response.PrepayId);
                var miniProgramUnifiedOrderCallResponse = await Executer.SignRequest<MiniProgramUnifiedOrderCallResponse>(miniProgramUnifiedOrderCallRequest, Config, App);
                return miniProgramUnifiedOrderCallResponse;
            }
            Logger.LogError($"微信小程序下单请求出错,ReturnMsg:{response.ReturnMsg},ErrorCodeMsg:{response.ErrCodeDes}");
            throw new Exception(response.ReturnMsg);
        }
    }
}
