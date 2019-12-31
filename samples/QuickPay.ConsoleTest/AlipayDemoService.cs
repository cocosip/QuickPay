using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.WeChatPay.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickPay.ConsoleTest
{
    public class AlipayDemoService
    {
        private readonly ILogger _logger;
        private WeChatPayDataHelper _weChatPayDataHelper;
        private readonly IAlipayTradeCommonService _alipayTradeCommonService;
        private readonly IAlipayAppPayService _alipayAppPayService;
        private readonly IAlipayPagePayService _alipayPagePayService;
        public AlipayDemoService(ILogger<AlipayDemoService> logger, WeChatPayDataHelper weChatPayDataHelper, IAlipayTradeCommonService alipayTradeCommonService, IAlipayAppPayService alipayAppPayService, IAlipayPagePayService alipayPagePayService)
        {
            _logger = logger;
            _weChatPayDataHelper = weChatPayDataHelper;
            _alipayTradeCommonService = alipayTradeCommonService;
            _alipayAppPayService = alipayAppPayService;
            _alipayPagePayService = alipayPagePayService;
        }

        /// <summary>支付宝App支付
        /// </summary>
        public async Task AlipayAppTradePay()
        {
            using (_alipayAppPayService.Use("2017061307479603"))
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
            using (_alipayPagePayService.Use("2017061307479603"))
            {
                var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
                {
                    ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
                };

                var response = await _alipayPagePayService.TradePay(input);
                _logger.LogInformation("AlipayPageTradePay,ReturnUrl:{0},NotifyUrl:{1}", response.ReturnUrl, response.NotifyUrl, _weChatPayDataHelper.DictToJson(new Dictionary<string, object>(response.PayData.GetValues())));
            }
        }

        /// <summary>支付宝订单查询
        /// </summary>
        public async Task Query()
        {
            using (_alipayTradeCommonService.Use("2017061307479603"))
            {
                var response = await _alipayTradeCommonService.Query(new TradeQueryInput("123456"));
                _logger.LogInformation("ReturnSuccess:{0},Code:{1}", response.ReturnSuccess, response.Code);
            }
        }

        /// <summary>支付宝退款申请
        /// </summary>
        public async Task RefundQuery()
        {
            using (_alipayTradeCommonService.Use("2017061307479603"))
            {
                var response = await _alipayTradeCommonService.RefundQuery(new TradeRefundQueryInput("123456", "1234567890"));
                _logger.LogInformation("ReturnSuccess:{0},Code:{1}", response.ReturnSuccess, response.Code);
            }
        }

        /// <summary>账单下载地址
        /// </summary>
        public async Task BillDownloadUrl()
        {
            using (_alipayTradeCommonService.Use("2017061307479603"))
            {
                var response = await _alipayTradeCommonService.BillDownloadUrl(new TradeBillDownloadUrlInput(AlipaySettings.BillType.Trade, "2017-03"));
                _logger.LogInformation("ReturnSuccess:{0},Code:{1},BillDownloadUrl:{2}", response.ReturnSuccess, response.Code, response.BillDownloadUrl);
            }
        }

    }
}
