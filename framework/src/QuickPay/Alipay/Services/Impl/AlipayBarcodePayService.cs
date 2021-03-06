﻿using DotCommon.Extensions;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>条码支付
    /// </summary>
    public class AlipayBarcodePayService : BaseAlipayService, IAlipayBarcodePayService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayBarcodePayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>条码支付统一下单
        /// </summary>
        public async Task<BarcodeTradePayResponse> TradePay(BarcodeTradePayInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }
            var bizContentRequest = ObjectMapper.Map<BarcodeTradeBizContentPayRequest>(input);
            var request = new BarcodeTradePayRequest(bizContentRequest, input.NotifyUrl);
            //发送请求到支付宝服务器
            var response = await Executer.ExecuteAsync<BarcodeTradePayResponse>(request, Config, App);
            return response;
        }
    }
}
