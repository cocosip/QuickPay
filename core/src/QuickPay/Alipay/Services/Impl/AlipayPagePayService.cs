using DotCommon.AutoMapper;
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
            var bizContentRequest = input.MapTo<PageTradeBizContentPayRequest>();
            var request = new PageTradePayRequest(bizContentRequest, input.ReturnUrl, input.NotifyUrl);
            var response = await Executer.SignRequest<PageTradePayResponse>(request, App);
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
