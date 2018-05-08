using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.WechatPay.Responses
{

    public abstract class BaseWechatPayResponse : PayResponse
    {
        /// <summary>是否返回成功
        /// </summary>
        public virtual bool ReturnSuccess
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ReturnCode))
                {
                    return ReturnCode == WechatPaySettings.ReturnCode.Success;
                }
                return false;
            }
        }

        /// <summary>业务是否执行成功
        /// </summary>
        public virtual bool ResultSuccess
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ResultCode))
                {
                    return ResultCode == WechatPaySettings.ReturnCode.Success;
                }
                return false;
            }
        }

        /// <summary>返回状态码,SUCCESS/FAIL
        /// </summary>
        [PayElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>返回信息
        /// </summary>
        [PayElement("return_msg")]
        public string ReturnMsg { get; set; }

        /// <summary>业务结果,SUCCESS/FAIL
        /// </summary>
        [PayElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>对于业务执行的详细描述,比如,OK
        /// </summary>
        [PayElement("result_msg")]
        public string ResultMsg { get; set; }

        /// <summary>错误代码
        /// </summary>
        [PayElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>	结果信息描述
        /// </summary>
        [PayElement("err_code_des")]
        public string ErrCodeDes { get; set; }
    }
}
