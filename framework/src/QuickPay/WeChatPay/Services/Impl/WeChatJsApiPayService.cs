using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using QuickPay.WeChatPay.Util;
using System;
using System.Threading.Tasks;
using WeChat.Framework;
using WeChat.Framework.Service;
namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信JsApi支付
    /// </summary>
    public class WeChatJsApiPayService : BaseWeChatPayService, IWeChatJsApiPayService
    {
        private readonly IWeChatSdkTicketService _weChatSdkTicketService;
        /// <summary>Ctor
        /// </summary>
        public WeChatJsApiPayService(IServiceProvider provider, IWeChatSdkTicketService weChatSdkTicketService) : base(provider)
        {
            _weChatSdkTicketService = weChatSdkTicketService;
        }

        /// <summary>JsApi支付统一下单
        /// </summary>
        public async Task<JsApiUnifiedOrderCallResponse> UnifiedOrder(JsApiUnifiedOrderInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var request = ObjectMapper.Map<JsApiUnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<JsApiUnifiedOrderResponse>(request, Config, App);
            //响应与执行都成功
            if (response.ReturnSuccess && response.ResultSuccess)
            {
                //请求执行成功,需要组合参数给接口
                var prepayId = response.PrepayId;

                var jsApiUnifiedOrderCallRequest = new JsApiUnifiedOrderCallRequest(response.PrepayId);
                var jsApiUnifiedOrderCallResonse = await Executer.SignRequest<JsApiUnifiedOrderCallResponse>(jsApiUnifiedOrderCallRequest, Config, App);
                return jsApiUnifiedOrderCallResonse;
            }
            Logger.LogError($"微信JsApi下单请求出错,ReturnMsg:{response.ReturnMsg},ErrorCodeMsg:{response.ErrCodeDes}");
            throw new Exception(response.ReturnMsg);
        }

        /// <summary>获取JsSdk config配置
        /// </summary>
        public async Task<JsSdkConfigResponse> GetJsSdkConfig(string currentUrl)
        {
            //JsApiTicket
            var jsApiTicket = await _weChatSdkTicketService.GetSdkTicketAsync(App.AppId, App.Appsecret, WeChatSettings.SdkTicketType.JsApi);
            Logger.LogInformation(WeChatPayUtil.ParseLog($"获取微信JsApiTicket:{jsApiTicket}"));
            var request = new JsSdkConfigRequest(jsApiTicket, currentUrl);
            //签名,获取JsSdk的时候,签名用的是Sha1
            var response = await Executer.SignRequest<JsSdkConfigResponse>(request, Config, App);
            return response;
        }

    }
}
