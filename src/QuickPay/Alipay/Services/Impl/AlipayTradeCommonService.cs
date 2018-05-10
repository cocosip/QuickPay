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
    /// <summary>通用交易接口
    /// </summary>
    public class AlipayTradeCommonService : BaseAlipayService, IAlipayTradeCommonService
    {
        public AlipayTradeCommonService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider) : base(alipayAppOverrideScopeProvider)
        {
        }

        /// <summary>支付宝订单查询
        /// </summary>
        public async Task<TradeQueryResponse> Query(TradeQueryInput input)
        {
            var bizContentRequest = input.MapTo<TradeQueryBizContentRequest>();
            var request = new TradeQueryRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeQueryResponse>(request, App);
            return response;
        }

        /// <summary>支付订单关闭
        /// </summary>
        public async Task<TradeCloseResponse> Close(TradeCloseInput input)
        {
            var bizContentRequest = input.MapTo<TradeCloseBizContentRequest>();
            var request = new TradeCloseRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeCloseResponse>(request, App);
            return response;
        }

        /// <summary>支付退款
        /// </summary>
        public async Task<TradeRefundResponse> Refund(TradeRefundInput input)
        {
            var bizContentRequest = input.MapTo<TradeRefundBizContentRequest>();
            var request = new TradeRefundRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeRefundResponse>(request, App);
            return response;
        }

        /// <summary>退款查询
        /// </summary>
        public async Task<TradeRefundQueryResponse> RefundQuery(TradeRefundQueryInput input)
        {
            var bizContentRequest = input.MapTo<TradeRefundQueryBizContentRequest>();
            var request = new TradeRefundQueryRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeRefundQueryResponse>(request, App);
            return response;
        }

        /// <summary>查询对账单下载地址
        /// </summary>
        public async Task<TradeBillDownloadUrlResponse> BillDownloadUrl(TradeBillDownloadUrlInput input)
        {
            var bizContentRequest = input.MapTo<TradeBillDownloadUrlBizContentRequest>();
            var request = new TradeBillDownloadUrlRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeBillDownloadUrlResponse>(request, App);
            return response;
        }
    }
}
