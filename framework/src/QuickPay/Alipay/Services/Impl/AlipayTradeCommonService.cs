using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>通用交易接口
    /// </summary>
    public class AlipayTradeCommonService : BaseAlipayService, IAlipayTradeCommonService
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayTradeCommonService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>支付宝订单查询
        /// </summary>
        public async Task<TradeQueryResponse> Query(TradeQueryInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeQueryBizContentRequest>(input);
            var request = new TradeQueryRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeQueryResponse>(request, App);
            return response;
        }

        /// <summary>支付订单关闭
        /// </summary>
        public async Task<TradeCloseResponse> Close(TradeCloseInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeCloseBizContentRequest>(input);
            var request = new TradeCloseRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeCloseResponse>(request, App);
            return response;
        }

        /// <summary>交易取消
        /// </summary>
        public async Task<TradeCancelResponse> Cancel(TradeCancelInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeCancelBizContentRequest>(input);
            var request = new TradeCancelRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeCancelResponse>(request, App);
            return response;
        }

        /// <summary>支付退款
        /// </summary>
        public async Task<TradeRefundResponse> Refund(TradeRefundInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeRefundBizContentRequest>(input);
            var request = new TradeRefundRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeRefundResponse>(request, App);
            return response;
        }

        /// <summary>退款查询
        /// </summary>
        public async Task<TradeRefundQueryResponse> RefundQuery(TradeRefundQueryInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeRefundQueryBizContentRequest>(input);
            var request = new TradeRefundQueryRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeRefundQueryResponse>(request, App);
            return response;
        }

        /// <summary>查询对账单下载地址
        /// </summary>
        public async Task<TradeBillDownloadUrlResponse> BillDownloadUrl(TradeBillDownloadUrlInput input)
        {
            var bizContentRequest = ObjectMapper.Map<TradeBillDownloadUrlBizContentRequest>(input);
            var request = new TradeBillDownloadUrlRequest(bizContentRequest);
            var response = await Executer.ExecuteAsync<TradeBillDownloadUrlResponse>(request, App);
            return response;
        }



    }
}
