using DotCommon.AutoMapper;
using DotCommon.Runtime;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信通用交易接口
    /// </summary>
    public class WechatPayTradeCommonService : BaseWechatPayService, IWechatPayTradeCommonService
    {
        public WechatPayTradeCommonService(IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider) : base(wechatPayAppOverrideScopeProvider)
        {
        }

        /// <summary>根据订单交易号查询订单
        /// </summary>
        public async Task<OrderQueryResponse> OrderQuery(OrderQueryInput input)
        {
            var request = input.MapTo<OrderQueryRequest>();
            var response = await Executer.ExecuteAsync<OrderQueryResponse>(request, App);
            return response;
        }


        /// <summary>关闭订单
        /// </summary>
        public async Task<OrderCloseResponse> OrderClose(OrderCloseInput input)
        {
            var request = new OrderCloseRequest(input.OutTradeNo);
            var response = await Executer.ExecuteAsync<OrderCloseResponse>(request, App);
            return response;
        }

        /// <summary>交易退款
        /// </summary>
        public async Task<OrderRefundResponse> Refund(OrderRefundInput input)
        {
            var request = input.MapTo<OrderRefundRequest>();
            var response = await Executer.ExecuteAsync<OrderRefundResponse>(request, App);
            return response;
        }

        /// <summary>退款查询
        /// </summary>
        public async Task<RefundQueryResponse> RefundQuery(RefundQueryInput input)
        {
            var request = input.MapTo<RefundQueryRequest>();
            var response = await Executer.ExecuteAsync<RefundQueryResponse>(request, App);
            return response;
        }

        /// <summary>下载对账单
        /// </summary>
        public async Task<DownloadBillResponse> DownloadBill(DownloadBillInput input)
        {
            var request = input.MapTo<DownloadBillRequest>();
            var response = await Executer.ExecuteAsync<DownloadBillResponse>(request, App);
            return response;
        }

        /// <summary>上报
        /// </summary>
        public async Task<ReportResponse> Report(ReportInput input)
        {
            var request = input.MapTo<ReportRequest>();
            var response = await Executer.ExecuteAsync<ReportResponse>(request, App);
            return response;
        }
    }
}
