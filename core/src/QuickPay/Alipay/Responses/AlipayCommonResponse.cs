using DotCommon.Extensions;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>请求支付宝,通用返回
    /// </summary>
    public class AlipayCommonResponse : BaseAlipayResponse
    {
        /// <summary>网关返回码
        /// </summary>
        [PayElement("code")]
        public string Code { get; set; }

        /// <summary>网关返回码描述
        /// </summary>
        [PayElement("msg")]
        public string Msg { get; set; }

        /// <summary>业务返回码
        /// </summary>
        [PayElement("sub_code", false)]
        public string SubCode { get; set; }

        /// <summary>业务返回码描述
        /// </summary>
        [PayElement("sub_msg", false)]
        public string SubMsg { get; set; }

        ///// <summary>签名
        ///// </summary>
        //[PayElement("sign")]
        //public string Sign { get; set; }

        public virtual bool ReturnSuccess
        {
            get
            {
                if (!Code.IsNullOrWhiteSpace())
                {
                    return Code == AlipaySettings.ReturnCode.Success;
                }
                return false;
            }
        }

    }
}
