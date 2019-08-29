using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using QuickPay.Alipay.Utility;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>App支付
    /// </summary>
    public class AlipayAppPayService : BaseAlipayService, IAlipayAppPayService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayAppPayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>支付宝支付生成支付信息
        /// </summary>
        public async Task<AppTradePayResponse> TradePay(AppTradePayInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }
            var bizContentRequest = ObjectMapper.Map<AppTradeBizContentPayRequest>(input);
            var request = new AppTradePayRequest(bizContentRequest, input.NotifyUrl);
            var response = await Executer.SignRequest<AppTradePayResponse>(request, Config, App);
            return response;
        }

        /// <summary>支付宝支付生成支付信息返回可以直接提交的字符串
        /// </summary>
        public async Task<string> TradePayStringResponse(AppTradePayInput input)
        {
            var response = await TradePay(input);
            var queryString = AlipayUtil.BuildQuery(response.PayData.GetValues(), App.Charset);
            return queryString;
        }
    }
}
