﻿using DotCommon.Extensions;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.Alipay.Utility;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>PC网站支付
    /// </summary>
    public class AlipayPagePayService : BaseAlipayService, IAlipayPagePayService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayPagePayService(IServiceProvider provider) : base(provider)
        {

        }


        /// <summary>PC网站支付生成订单Post表单的数据
        /// </summary>
        public async Task<PageTradePayResponse> TradePay(PageTradePayInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }
            var bizContentRequest = ObjectMapper.Map<PageTradeBizContentPayRequest>(input);
            var request = new PageTradePayRequest(bizContentRequest, input.ReturnUrl, input.NotifyUrl);
            var response = await Executer.SignRequest<PageTradePayResponse>(request, Config, App);
            return response;
        }


        /// <summary>PC网站支付生成订单Get请求字符串
        /// </summary>
        public async Task<string> TradePayStringResponse(PageTradePayInput input)
        {
            var response = await TradePay(input);
            var queryString = AlipayUtil.BuildQuery(response.PayData.GetValues(), App.Charset);
            return queryString;
        }
    }
}
