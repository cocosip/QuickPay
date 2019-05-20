using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.Assist;
using QuickPay.Assist.Store;
using QuickPay.WeChatPay.Apps;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>退款存储
    /// </summary>
    public class RefundStoreMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IRefundStore _refundStore;
        private readonly IRequestTypeFinder _requestTypeFinder;
        /// <summary>Ctor
        /// </summary>
        public RefundStoreMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, IRefundStore refundStore, IRequestTypeFinder requestTypeFinder)
        {
            _next = next;
            Logger = logger;
            _refundStore = refundStore;
            _requestTypeFinder = requestTypeFinder;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                //判断是否需要调用Store存储支付信息
                if (ShouldStore(context.Request.GetType()))
                {
                    var payment = PrepareRefund(context);
                    await _refundStore.CreateOrUpdateAsync(payment);
                }
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new RefundStoreError($"退款信息存储发生错误,{ex.Message}"));
                return;
            }

            Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            await _next.Invoke(context);
        }

        private Refund PrepareRefund(ExecuteContext context)
        {
            var refund = new Refund()
            {
                UniqueId = context.Request.UniqueId ?? ObjectId.GenerateNewStringId(),
            };

            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                refund.PayPlatId = (int)PayPlat.Alipay;
                refund.AppId = ((AlipayApp)context.App).AppId;

                //支付宝是获取bizContentRequest中的数据
                var property = context.Request.GetType().GetProperty("BizContentRequest");
                var bizContentRequest = property.GetValue(context.Request);
                var payData = RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest);
                //下单时唯一编号,由本系统生成
                refund.OutTradeNo = payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
                //下单成功后,支付宝生成的编号
                refund.TransactionId = payData.GetValue(x => x.Key.ToLower() == "trade_no").ToString();
                //本系统退款唯一编号
                refund.OutRefundNo = payData.GetValue(x => x.Key.ToLower() == "out_request_no").ToString();
                //退款支付金额,以元为单位
                refund.RefundAmount = Convert.ToDecimal(payData.GetValue(x => x.Key.ToLower() == "refund_amount"));

            }
            else
            {
                refund.PayPlatId = (int)PayPlat.WechatPay;
                refund.AppId = ((WeChatPayApp)context.App).AppId;

                //交易号,本系统唯一
                refund.OutTradeNo = context.RequestPayData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
                //下单成功后,微信生成的编号
                refund.TransactionId = context.RequestPayData.GetValue(x => x.Key.ToLower() == "transaction_id").ToString();
                //本系统退款唯一编号
                refund.OutRefundNo = context.RequestPayData.GetValue(x => x.Key.ToLower() == "out_refund_no").ToString();
                //退款金额,微信以分为单位,int类型需要转换成元
                refund.RefundAmount = Convert.ToDecimal(Convert.ToInt32(context.RequestPayData.GetValue(x => x.Key.ToLower() == "total_fee")) / 100.0);

            }
            return refund;
        }

        private bool ShouldStore(Type requestType)
        {
            var refundTypies = _requestTypeFinder.FindRefundStoreTypes();
            if (refundTypies.Contains(requestType))
            {
                return true;
            }
            return false;
        }
    }
}
