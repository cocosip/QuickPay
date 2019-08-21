using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信App支付
    /// </summary>
    public class WeChatAppPayService : BaseWeChatPayService, IWeChatAppPayService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatAppPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>App支付统一下单
        /// </summary>
        public async Task<AppUnifiedOrderCallResponse> UnifiedOrder(AppUnifiedOrderInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var request = ObjectMapper.Map<AppUnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<AppUnifiedOrderResponse>(request, Config, App);
            if (response.ReturnSuccess)
            {
                if (response.ResultSuccess)
                {
                    //请求执行成功,需要组合参数给接口
                    var prepayId = response.PrepayId;

                    var appUnifiedOrderCallRequest = new AppUnifiedOrderCallRequest(prepayId);
                    var appUnifiedOrderCallResonse = await Executer.SignRequest<AppUnifiedOrderCallResponse>(appUnifiedOrderCallRequest, Config, App);
                    return appUnifiedOrderCallResonse;
                }
            }
            throw new Exception(response.ErrCodeDes);
        }

    }
}
