using DotCommon.Utility;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.WechatPay.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.ConsoleTest
{
    public class AlipayDemoService
    {
        private WechatPayDataHelper _wechatPayDataHelper;
        private readonly AlipayConfig _alipayConfig;
        private readonly IAlipayAppPayService _alipayAppPayService;
        private readonly IAlipayPagePayService _alipayPagePayService;
        public AlipayDemoService(WechatPayDataHelper wechatPayDataHelper, AlipayConfig alipayConfig, IAlipayAppPayService alipayAppPayService, IAlipayPagePayService alipayPagePayService)
        {
            _wechatPayDataHelper = wechatPayDataHelper;
            _alipayConfig = alipayConfig;
            _alipayAppPayService = alipayAppPayService;
            _alipayPagePayService = alipayPagePayService;
        }

        /// <summary>支付宝App支付
        /// </summary>
        public async Task AlipayAppTradePay()
        {
            using (_alipayAppPayService.Use(_alipayConfig.GetByName("App1")))
            {
                var input = new AppTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1");
                var responseString = await _alipayAppPayService.TradePayStringResponse(input);
                Console.WriteLine("ResponseString:{0}", responseString);
            }
        }

        /// <summary>支付宝网站支付
        /// </summary>
        public async Task AlipayPageTradePay()
        {
            using (_alipayPagePayService.Use(_alipayConfig.GetByName("App1")))
            {
                var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
                {
                    ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
                };

                var response = await _alipayPagePayService.TradePay(input);
                Console.WriteLine("AlipayPageTradePay,ReturnUrl:{0},NotifyUrl:{1}", response.ReturnUrl, response.NotifyUrl, _wechatPayDataHelper.DictToJson(new Dictionary<string, object>(response.PayData.GetValues())));
            }
        }



    }
}
