using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>上报
    /// </summary>
    public class ReportRequest : BaseWechatPayRequest<ReportResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.ExtTradeType.Report;

        /// <summary>自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>接口地址
        /// </summary>
        [PayElement("interface_url")]
        public string InterfaceUrl { get; set; }

        /// <summary>接口耗时情况，单位为毫秒
        /// </summary>
        [PayElement("execute_time_")]
        public int ExecuteTime { get; set; }

        /// <summary>此字段是通信标识，非交易标识，交易是否成功需要查看trade_state来判断
        /// </summary>
        [PayElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>返回信息，如非空，为错误原因
        /// </summary>
        [PayElement("return_msg", false)]
        public string ReturnMsg { get; set; }

        /// <summary>业务结果
        /// </summary>
        [PayElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>错误代码
        /// </summary>
        [PayElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>	结果信息描述
        /// </summary>
        [PayElement("err_code_des")]
        public string ErrCodeDes { get; set; }

        /// <summary>商户系统内部的订单号,商户可以在上报时提供相关商户订单号方便微信支付更好的提高服务质量
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>发起接口调用时的机器IP 
        /// </summary>
        [PayElement("user_ip")]
        public string UserIp { get; set; }

        /// <summary>系统时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
        /// </summary>
        [PayElement("time")]
        public string Time { get; set; }

        /// <summary>Ctor
        /// </summary>
        public ReportRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="interfaceUrl">接口Url地址</param>
        /// <param name="executeTime">执行时间</param>
        /// <param name="returnCode">请求代码</param>
        /// <param name="resultCode">结果代码</param>
        /// <param name="userIp">用户IP地址</param>
        public ReportRequest(string interfaceUrl, int executeTime, string returnCode, string resultCode, string userIp)
        {
            InterfaceUrl = interfaceUrl;
            ExecuteTime = executeTime;
            ReturnCode = returnCode;
            ResultCode = resultCode;
            UserIp = userIp;
        }
    }
}
