using QuickPay.Infrastructure.RequestData;
using System;

namespace QuickPay.Alipay.Responses
{
    /// <summary>条码支付
    /// </summary>
    public class BarcodeTradePayResponse : AlipayCommonResponse
    {
        /// <summary>交易号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>支付宝交易号
        /// </summary>
        [PayElement("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>买家支付宝账号
        /// </summary>
        [PayElement("buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        /// <summary>交易金额
        /// </summary>
        [PayElement("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>实收金额
        /// </summary>
        [PayElement("receipt_amount")]
        public decimal ReceiptAmount { get; set; }

        /// <summary>交易支付使用的资金渠道	
        /// </summary>
        [PayElement("fund_bill_list")]
        public string FundBillList { get; set; }

        /// <summary>交易支付时间	
        /// </summary>
        [PayElement("gmt_payment")]
        public DateTime GmtPayment { get; set; }

        /// <summary>买家在支付宝的用户id	
        /// </summary>
        [PayElement("buyer_user_id")]
        public string BuyerUserId { get; set; }

        /// <summary>本次交易支付所使用的单品券优惠的商品优惠信息	
        /// </summary>
        [PayElement("discount_goods_detail")]
        public string DiscountGoodsDetail { get; set; }
        /*************************************************/


        /// <summary>买家付款的金额
        /// </summary>
        [PayElement("buyer_pay_amount", false)]
        public decimal BuyerPayAmount { get; set; }

        /// <summary>使用积分宝付款的金额
        /// </summary>
        [PayElement("point_amount", false)]
        public decimal PointAmount { get; set; }

        /// <summary>交易中可给用户开具发票的金额	
        /// </summary>
        [PayElement("invoice_amount", false)]
        public decimal InvoiceAmount { get; set; }


        /// <summary>支付宝卡余额	
        /// </summary>
        [PayElement("card_balance", false)]
        public decimal CardBalance { get; set; }

        /// <summary>发生支付交易的商户门店名称	
        /// </summary>
        [PayElement("store_name", false)]
        public string StoreName { get; set; }

        /// <summary>本交易支付时使用的所有优惠券信息	
        /// </summary>
        [PayElement("voucher_detail_list", false)]
        public string VoucherDetailList { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BarcodeTradePayResponse()
        {

        }
    }
}
