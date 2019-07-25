using DotCommon.Extensions;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝扫码支付
    /// </summary>
    public class AlipayQrcodePayService : BaseAlipayService, IAlipayQrcodePayService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayQrcodePayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>统一收单线下交易预创建
        /// </summary>
        public async Task<QrcodeTradePayResponse> PrepayCreate(QrcodePayPreCreateInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var bizContentRequest = ObjectMapper.Map<QrcodeTradeBizContentPayRequest>(input);
            var request = new QrcodeTradePayRequest(bizContentRequest, input.NotifyUrl);
            //发送请求到支付宝服务器
            var response = await Executer.ExecuteAsync<QrcodeTradePayResponse>(request, App);
            return response;
        }
    }
}
