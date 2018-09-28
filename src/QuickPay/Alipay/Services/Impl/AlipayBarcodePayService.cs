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
    /// <summary>条码支付
    /// </summary>
    public class AlipayBarcodePayService : BaseAlipayService, IAlipayBarcodePayService
    {
        public AlipayBarcodePayService(IServiceProvider provider, IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider) : base(provider, alipayAppOverrideScopeProvider)
        {
        }

        /// <summary>条码支付统一下单
        /// </summary>
        public async Task<BarcodeTradePayResponse> TradePay(BarcodeTradePayInput input)
        {
            var bizContentRequest = input.MapTo<BarcodeTradeBizContentPayRequest>();
            var request = new BarcodeTradePayRequest(bizContentRequest, input.NotifyUrl);
            //发送请求到支付宝服务器
            var response = await Executer.ExecuteAsync<BarcodeTradePayResponse>(request, App);
            return response;
        }
    }
}
