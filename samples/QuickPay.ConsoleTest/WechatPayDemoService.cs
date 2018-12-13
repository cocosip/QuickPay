using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.ConsoleTest
{
    public class WechatPayDemoService
    {
        private readonly ILogger _logger;
        private readonly WechatPayConfig _wechatPayConfig;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWechatPayTradeCommonService _wechatPayTradeCommonService;
        private readonly IWechatAppPayService _wechatAppPayService;
        private readonly IWechatJsApiPayService _wechatJsApiPayService;
        private readonly IWechatMiniProgramPayService _wechatMiniProgramPayService;

        public WechatPayDemoService(ILogger<QuickPayLoggerName> logger, WechatPayConfig wechatPayConfig, IAuthenticationService authenticationService, IWechatPayTradeCommonService wechatPayTradeCommonService, IWechatAppPayService wechatAppPayService, IWechatJsApiPayService wechatJsApiPayService, IWechatMiniProgramPayService wechatMiniProgramPayService)
        {
            _logger = logger;
            _wechatPayConfig = wechatPayConfig;
            _authenticationService = authenticationService;
            _wechatPayTradeCommonService = wechatPayTradeCommonService;
            _wechatAppPayService = wechatAppPayService;
            _wechatJsApiPayService = wechatJsApiPayService;
            _wechatMiniProgramPayService = wechatMiniProgramPayService;
        }


        /// <summary>微信App统一下单
        /// </summary>
        public async Task WechatAppUnifiedOrder()
        {
            using (_wechatAppPayService.Use(_wechatPayConfig.GetByName("App1")))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                await _wechatAppPayService.UnifiedOrder(input);
            }
        }

        /// <summary>微信JsApi下单
        /// </summary>
        public async Task WechatJsApiUnifiedOrder()
        {
            using (_wechatJsApiPayService.Use(_wechatPayConfig.GetByName("App2")))
            {
                var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "opaInxF28ub-ea5JVrZOosDHyXZY");
                await _wechatJsApiPayService.UnifiedOrder(input);
            }
        }

        /// <summary>微信小程序下单
        /// </summary>
        public async Task WechatMiniProgramUnifiedOrder()
        {
            using (_wechatMiniProgramPayService.Use(_wechatPayConfig.GetByName("App3")))
            {
                //var openId = await authenticationService.GetMiniProgramOpenId(miniProgramService.App.AppId, miniProgramService.App.Appsecret, "071FCmZX1ixU011SMv0Y1KAvZX1FCmZo");

                // ofKnT5CCHaMNzinIfiLpPIb3S014
                var input = new MiniProgramUnifiedOrderInput("小程序支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "ofKnT5CCHaMNzinIfiLpPIb3S014");
                await _wechatMiniProgramPayService.UnifiedOrder(input);
            }
        }


        /// <summary>微信订单查询
        /// </summary>
        public async Task WechatOrderQuery()
        {
            using (_wechatPayTradeCommonService.Use(_wechatPayConfig.GetByName("App3")))
            {
                var response = await _wechatPayTradeCommonService.OrderQuery(new OrderQueryInput("123"));
                _logger.LogInformation("ReturnSuccess:{0},outTradeNo:{1}", response.ReturnSuccess, response.OutTradeNo);
            }
        }



    }
}
