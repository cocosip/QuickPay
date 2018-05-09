using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>关闭订单
    /// </summary>
    public class OrderCloseResponse : WechatPayCommonResponse
    {
        [PayElement("appid")]
        public string AppId { get; set; }


        [PayElement("mch_id")]
        public string MchId { get; set; }
    }
}
