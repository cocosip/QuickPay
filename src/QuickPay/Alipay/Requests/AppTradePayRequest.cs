using QuickPay.Alipay.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPay.Alipay.Requests
{
    /// <summary>支付宝App支付
    /// </summary>
    public class AppTradePayRequest : BaseAlipayRequest<AppTradePayResponse>
    {
        public override string Method => "";
    }
}
