using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotCommon.AutoMapper;
using DotCommon.Runtime;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.Alipay.Util;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝手机网站支付
    /// </summary>
    public class AlipayWapPayService : BaseAlipayService, IAlipayWapPayService
    {
        public AlipayWapPayService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider) : base(alipayAppOverrideScopeProvider)
        {

        }

        /// <summary>支付宝手机网站支付生成支付信息
        /// </summary>
        public async Task<WapTradePayResponse> TradePay(WapTradePayInput input)
        {
            var bizContentRequest = input.MapTo<WapTradeBizContentPayRequest>();
            var request = new WapTradePayRequest(bizContentRequest, input.ReturnUrl, input.NotifyUrl);
            var response = await Executer.ExecuteAsync<WapTradePayResponse>(request, App);
            return response;
        }

        /// <summary>支付宝支付生成支付信息返回可以直接提交的字符串
        /// </summary>
        public async Task<string> TradePayStringResponse(WapTradePayInput input)
        {
            var response = await TradePay(input);
            var queryString = AlipayUtil.BuildQuery(response.PayData.GetValues(), App.Charset);
            return queryString;
        }
    }
}
