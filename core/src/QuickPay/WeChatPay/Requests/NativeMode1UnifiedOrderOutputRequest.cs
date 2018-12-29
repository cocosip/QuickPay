using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信扫码支付模式1,统一下单之后返回PrepayId给微信
    /// </summary>
    public class NativeMode1UnifiedOrderOutputRequest : BaseWechatPayRequest<NativeMode1UnifiedOrderOutputResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.Native;

        /// <summary>SUCCESS/FAIL,此字段是通信标识
        /// </summary>
        [PayElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>返回信息，如非空，为错误原因
        /// </summary>
        [PayElement("return_msg")]
        public string ReturnMsg { get; set; }

        /// <summary>SUCCESS/FAIL
        /// </summary>
        [PayElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>错误信息
        /// </summary>
        [PayElement("err_code_des")]
        public string ErrCodeDes { get; set; }

        /// <summary>预支付ID
        /// </summary>
        [PayElement("prepay_id")]
        public string PrepayId { get; set; }


        /// <summary>Ctor
        /// </summary>
        public NativeMode1UnifiedOrderOutputRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="prepayId">预支付ID</param>
        /// <param name="success">是否成功</param>
        public NativeMode1UnifiedOrderOutputRequest(string prepayId, bool success = true)
        {
            PrepayId = prepayId;
            if (success)
            {
                ReturnCode = WeChatPaySettings.ReturnCode.Success;
                ResultCode = WeChatPaySettings.ResultCode.Success;
            }
            else
            {
                ReturnCode = WeChatPaySettings.ReturnCode.Fail;
                ResultCode = WeChatPaySettings.ResultCode.Fail;
            }
        }
    }
}
