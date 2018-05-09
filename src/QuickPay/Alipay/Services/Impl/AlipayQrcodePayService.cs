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

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝扫码支付
    /// </summary>
    public class AlipayQrcodePayService : BaseAlipayService, IAlipayQrcodePayService
    {
        public AlipayQrcodePayService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider) : base(alipayAppOverrideScopeProvider)
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
