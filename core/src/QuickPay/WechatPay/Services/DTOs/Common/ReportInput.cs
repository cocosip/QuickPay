using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>上报
    /// </summary>
    [AutoMapTo(typeof(ReportRequest))]
    public class ReportInput : UniqueIdDto
    {
        /// <summary>自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>接口地址
        /// </summary>
        public string InterfaceUrl { get; set; }

        /// <summary>接口耗时情况，单位为毫秒
        /// </summary>
        public int ExecuteTime { get; set; }

        /// <summary>此字段是通信标识，非交易标识，交易是否成功需要查看trade_state来判断
        /// </summary>
        public string ReturnCode { get; set; }

        /// <summary>返回信息，如非空，为错误原因
        /// </summary>
        public string ReturnMsg { get; set; }

        /// <summary>业务结果
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>错误代码
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>	结果信息描述
        /// </summary>
        public string ErrCodeDes { get; set; }

        /// <summary>商户系统内部的订单号,商户可以在上报时提供相关商户订单号方便微信支付更好的提高服务质量
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>发起接口调用时的机器IP 
        /// </summary>
        public string UserIp { get; set; }

        /// <summary>系统时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
        /// </summary>
        public string Time { get; set; }

        public ReportInput()
        {

        }


        public ReportInput(string interfaceUrl, int executeTime, string returnCode, string resultCode, string userIp)
        {
            InterfaceUrl = interfaceUrl;
            ExecuteTime = executeTime;
            ReturnCode = returnCode;
            ResultCode = resultCode;
            UserIp = userIp;
        }
    }
}
