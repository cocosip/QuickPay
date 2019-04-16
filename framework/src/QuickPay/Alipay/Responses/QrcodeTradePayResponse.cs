using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>条码支付生成预订单
    /// </summary>
    public class QrcodeTradePayResponse : AlipayCommonResponse
    {
        /// <summary>交易号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>当前预下单请求生成的二维码码串，可以用二维码生成工具根据该码串值生成对应的二维码
        /// </summary>
        [PayElement("qr_code")]
        public string QrCode { get; set; }

        /// <summary>Ctor
        /// </summary>
        public QrcodeTradePayResponse()
        {

        }
    }
}
