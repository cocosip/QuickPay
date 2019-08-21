using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信通用交易接口
    /// </summary>
    public class WeChatPayTradeCommonService : BaseWeChatPayService, IWeChatPayTradeCommonService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatPayTradeCommonService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>根据订单交易号查询订单
        /// </summary>
        public async Task<OrderQueryResponse> OrderQuery(OrderQueryInput input)
        {
            var request = ObjectMapper.Map<OrderQueryRequest>(input);
            var response = await Executer.ExecuteAsync<OrderQueryResponse>(request, Config, App);
            return response;
        }


        /// <summary>关闭订单
        /// </summary>
        public async Task<OrderCloseResponse> OrderClose(OrderCloseInput input)
        {
            var request = new OrderCloseRequest(input.OutTradeNo);
            var response = await Executer.ExecuteAsync<OrderCloseResponse>(request, Config, App);
            return response;
        }

        /// <summary>交易退款
        /// </summary>
        public async Task<OrderRefundResponse> Refund(OrderRefundInput input)
        {
            var request = ObjectMapper.Map<OrderRefundRequest>(input);
            var response = await Executer.ExecuteAsync<OrderRefundResponse>(request, Config, App);
            return response;
        }

        /// <summary>退款查询
        /// </summary>
        public async Task<RefundQueryResponse> RefundQuery(RefundQueryInput input)
        {
            var request = ObjectMapper.Map<RefundQueryRequest>(input);
            var response = await Executer.ExecuteAsync<RefundQueryResponse>(request, Config, App);
            return response;
        }

        /// <summary>下载对账单
        /// </summary>
        public async Task<DownloadBillResponse> DownloadBill(DownloadBillInput input)
        {
            var request = ObjectMapper.Map<DownloadBillRequest>(input);
            var response = await Executer.ExecuteAsync<DownloadBillResponse>(request, Config, App);
            return response;
        }

        /// <summary>上报
        /// </summary>
        public async Task<ReportResponse> Report(ReportInput input)
        {
            var request = ObjectMapper.Map<ReportRequest>(input);
            var response = await Executer.ExecuteAsync<ReportResponse>(request, Config, App);
            return response;
        }

    }
}
