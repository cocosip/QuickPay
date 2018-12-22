using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.Alipay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝手机网站支付
    /// </summary>
    public class AlipayWapPayService : BaseAlipayService, IAlipayWapPayService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayWapPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>支付宝手机网站支付生成支付信息
        /// </summary>
        public async Task<WapTradePayResponse> TradePay(WapTradePayInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }
            var bizContentRequest = input.MapTo<WapTradeBizContentPayRequest>();
            var request = new WapTradePayRequest(bizContentRequest, input.ReturnUrl, input.NotifyUrl);
            var response = await Executer.SignRequest<WapTradePayResponse>(request, App);
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
