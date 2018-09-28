using DotCommon.AutoMapper;
using DotCommon.Threading;
using Microsoft.Extensions.Logging;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信JsApi支付
    /// </summary>
    public class WechatJsApiPayService : BaseWechatPayService, IWechatJsApiPayService
    {
        private readonly IAuthenticationService _authenticationService;
        public WechatJsApiPayService(IServiceProvider provider, IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider, IAuthenticationService authenticationService) : base(provider, wechatPayAppOverrideScopeProvider)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>JsApi支付统一下单
        /// </summary>
        public async Task<JsApiUnifiedOrderCallResponse> UnifiedOrder(JsApiUnifiedOrderInput input)
        {
            var request = input.MapTo<JsApiUnifiedOrderRequest>();
            var response = await Executer.ExecuteAsync<JsApiUnifiedOrderResponse>(request, App);
            //响应与执行都成功
            if (response.ReturnSuccess && response.ResultSuccess)
            {
                //请求执行成功,需要组合参数给接口
                var prepayId = response.PrepayId;

                var jsApiUnifiedOrderCallRequest = new JsApiUnifiedOrderCallRequest(response.PrepayId);
                var jsApiUnifiedOrderCallResonse = await Executer.SignRequest<JsApiUnifiedOrderCallResponse>(jsApiUnifiedOrderCallRequest, App);
                return jsApiUnifiedOrderCallResonse;
            }
            throw new Exception(response.ErrCodeDes);
        }

        /// <summary>获取JsSdk config配置
        /// </summary>
        public async Task<JsSdkConfigResponse> GetJsSdkConfig(string currentUrl)
        {
            //JsApi Ticket
            var jsApiTicket = await _authenticationService.GetJsApiTicketAsync(App.AppId, App.Appsecret);
            Logger.LogInformation(WechatPayUtil.ParseLog($"获取微信JsApiTicket:{jsApiTicket}"));
            var request = new JsSdkConfigRequest(jsApiTicket, currentUrl);
            //签名,获取JsSdk的时候,签名用的是Sha1
            var response = await Executer.SignRequest<JsSdkConfigResponse>(request, App);
            return response;
        }

    }
}
