﻿using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>查询账单下载地址BizContent
    /// </summary>
    public class TradeBillDownloadUrlBizContentRequest : BaseBizContentRequest
    {
        /// <summary>账单类型，商户通过接口或商户经开放平台授权后其所属服务商通过接口可以获取以下账单类型：trade、signcustomer；trade指商户基于支付宝交易收单的业务账单；signcustomer是指基于商户支付宝余额收入及支出等资金变动的帐务账单；
        /// </summary>
        [PayElement("bill_type")]
        public string BillType { get; set; }

        /// <summary>账单时间：日账单格式为yyyy-MM-dd，月账单格式为yyyy-MM。
        /// </summary>
        [PayElement("bill_date")]
        public string BillDate { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeBillDownloadUrlBizContentRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="billType">订单类型</param>
        /// <param name="billDate">订单日期</param>
        public TradeBillDownloadUrlBizContentRequest(string billType, string billDate)
        {
            BillType = billType;
            BillDate = billDate;
        }
    }
}
