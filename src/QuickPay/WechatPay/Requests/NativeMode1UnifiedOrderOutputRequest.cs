using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>微信扫码支付模式1,统一下单之后返回PrepayId给微信
    /// </summary>
    public class NativeMode1UnifiedOrderOutputRequest : BaseWechatPayRequest<NativeMode1UnifiedOrderOutputResponse>
    {
        public override string TradeTypeName => WechatPaySettings.TradeType.Native;

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


        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
        }

        public NativeMode1UnifiedOrderOutputRequest()
        {

        }

        public NativeMode1UnifiedOrderOutputRequest(string prepayId, bool success = true)
        {
            PrepayId = prepayId;
            if (success)
            {
                ReturnCode = WechatPaySettings.ReturnCode.Success;
                ResultCode = WechatPaySettings.ResultCode.Success;
            }
            else
            {
                ReturnCode = WechatPaySettings.ReturnCode.Fail;
                ResultCode = WechatPaySettings.ResultCode.Fail;
            }
        }
    }
}
