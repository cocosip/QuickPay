using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.WeChatPay.Authentication;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.ConsoleTest
{
    public class WeChatPayDemoService
    {
        private readonly ILogger _logger;

        private readonly IAuthenticationService _authenticationService;
        private readonly IWeChatPayTradeCommonService _weChatPayTradeCommonService;
        private readonly IWeChatAppPayService _weChatAppPayService;
        private readonly IWeChatJsApiPayService _weChatJsApiPayService;
        private readonly IWeChatMiniProgramPayService _weChatMiniProgramPayService;

        public WeChatPayDemoService(ILogger<WeChatPayDemoService> logger, IAuthenticationService authenticationService, IWeChatPayTradeCommonService weChatPayTradeCommonService, IWeChatAppPayService weChatAppPayService, IWeChatJsApiPayService weChatJsApiPayService, IWeChatMiniProgramPayService weChatMiniProgramPayService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _weChatPayTradeCommonService = weChatPayTradeCommonService;
            _weChatAppPayService = weChatAppPayService;
            _weChatJsApiPayService = weChatJsApiPayService;
            _weChatMiniProgramPayService = weChatMiniProgramPayService;
        }


        /// <summary>微信App统一下单
        /// </summary>
        public async Task WeChatAppUnifiedOrder()
        {
            using (_weChatAppPayService.Use("wx7462799678470f25"))
            {
                var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
                await _weChatAppPayService.UnifiedOrder(input);
            }
        }

        /// <summary>微信JsApi下单
        /// </summary>
        public async Task WeChatJsApiUnifiedOrder()
        {
            using (_weChatJsApiPayService.Use("wx0b53a07382f48372"))
            {
                var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "opaInxF28ub-ea5JVrZOosDHyXZY");
                await _weChatJsApiPayService.UnifiedOrder(input);
            }
        }

        /// <summary>微信小程序下单
        /// </summary>
        public async Task WeChatMiniProgramUnifiedOrder()
        {
            using (_weChatMiniProgramPayService.Use("wx4239531fdf3896e6"))
            {
                //var openId = await authenticationService.GetMiniProgramOpenId(miniProgramService.App.AppId, miniProgramService.App.Appsecret, "071FCmZX1ixU011SMv0Y1KAvZX1FCmZo");

                // ofKnT5CCHaMNzinIfiLpPIb3S014
                var input = new MiniProgramUnifiedOrderInput("小程序支付测试", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "http://114.55.101.33", "ofKnT5CCHaMNzinIfiLpPIb3S014");
                await _weChatMiniProgramPayService.UnifiedOrder(input);
            }
        }


        /// <summary>微信订单查询
        /// </summary>
        public async Task WeChatOrderQuery()
        {
            using (_weChatPayTradeCommonService.Use("wx4239531fdf3896e6"))
            {
                var response = await _weChatPayTradeCommonService.OrderQuery(new OrderQueryInput("123"));
                _logger.LogInformation("ReturnSuccess:{0},outTradeNo:{1}", response.ReturnSuccess, response.OutTradeNo);
            }
        }



    }
}
