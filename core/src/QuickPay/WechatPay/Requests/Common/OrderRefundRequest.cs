using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>退款
    /// </summary>
    public class OrderRefundRequest : BaseWechatPayRequest<OrderRefundResponse>
    {
        //public override string RequestUrl => "https://api.mch.weixin.qq.com/secapi/pay/refund";
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WechatPaySettings.ExtTradeType.OrderRefund;

        /// <summary>签名类型
        /// </summary>
        [PayElement("sign_type", false)]
        public string SignType { get; set; }

        /// <summary>微信订单号
        /// </summary>
        [PayElement("transaction_id", false)]
        public string TransactionId { get; set; }

        /// <summary>货币类型，符合ISO 4217标准的三位字母代码
        /// </summary>
        [PayElement("refund_fee_type", false)]
        public string RefundFeeType { get; set; }

        /// <summary>退款资金来源
        /// REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）
        /// REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款(限非当日交易订单的退款）
        /// </summary>
        [PayElement("refund_account", false)]
        public string RefundAccount { get; set; }


        /// <summary>商户中的订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
        /// </summary>
        [PayElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>订单总金额,单位为分,只能为整数
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>退款总金额
        /// </summary>
        [PayElement("refund_fee")]
        public int RefundFee { get; set; }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            SignType = ((WechatPayConfig)config).SignType;
        }

        /// <summary>Ctor
        /// </summary>
        public OrderRefundRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户中的订单号</param>
        /// <param name="outRefundNo">商户系统内部的退款单号</param>
        /// <param name="totalFee">订单总金额,单位为分,只能为整数</param>
        /// <param name="refundFee">退款总金额</param>
        public OrderRefundRequest(string outTradeNo, string outRefundNo, int totalFee, int refundFee)
        {
            OutTradeNo = outTradeNo;
            OutRefundNo = outRefundNo;
            TotalFee = totalFee;
            RefundFee = refundFee;
        }
    }
}
