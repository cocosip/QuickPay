using DotCommon.AutoMapper;
using DotCommon.Threading;
using QuickPay.Alipay.Apps;
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
        public AlipayQrcodePayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>统一收单线下交易预创建
        /// </summary>
        public async Task<QrcodeTradePayResponse> PrepayCreate(QrcodePayPreCreateInput input)
        {
            var bizContentRequest = input.MapTo<QrcodeTradeBizContentPayRequest>();
            var request = new QrcodeTradePayRequest(bizContentRequest, input.NotifyUrl);
            //发送请求到支付宝服务器
            var response = await Executer.ExecuteAsync<QrcodeTradePayResponse>(request, App);
            return response;
        }
    }
}
