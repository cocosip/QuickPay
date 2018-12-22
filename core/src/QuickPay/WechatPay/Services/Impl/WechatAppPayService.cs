using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信App支付
    /// </summary>
    public class WechatAppPayService : BaseWechatPayService, IWechatAppPayService
    {
        /// <summary>Ctor
        /// </summary>
        public WechatAppPayService(IServiceProvider provider) : base(provider)
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

            var request = input.MapTo<AppUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<AppUnifiedOrderResponse>(request, App);
            if (response.ReturnSuccess)
            {
                if (response.ResultSuccess)
                {
                    //请求执行成功,需要组合参数给接口
                    var prepayId = response.PrepayId;

                    var appUnifiedOrderCallRequest = new AppUnifiedOrderCallRequest(prepayId);
                    var appUnifiedOrderCallResonse = await Executer.SignRequest<AppUnifiedOrderCallResponse>(appUnifiedOrderCallRequest, App);
                    return appUnifiedOrderCallResonse;
                }
            }
            throw new Exception(response.ErrCodeDes);
        }

    }
}
