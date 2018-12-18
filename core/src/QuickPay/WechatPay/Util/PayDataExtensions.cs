using DotCommon.Extensions;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuickPay.WechatPay.Util
{
    /// <summary>微信支付中PayData扩展
    /// </summary>
    public static class PayDataExtensions
    {
        /// <summary>PayData转化为Url地址
        /// </summary>
        public static string ToUrl(this PayData payData)
        {
            var sb = new StringBuilder();
            foreach (var pair in payData.GetValues())
            {
                if (pair.Value == null)
                {
                    throw new Exception("WechatPayData内部含有值为null的字段!");
                }
                if (pair.Key.ToLower() != "sign" && !pair.Value.ToString().IsNullOrWhiteSpace())
                {
                    sb.Append($"{pair.Key}={pair.Value}&");
                }
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }


        /// <summary>微信如果返回失败,就获取MessageCode
        /// </summary>
        public static string GetMsgIfReturnFail(this PayData payData)
        {
            var returnCode = payData.GetValue("return_code");
            if (returnCode != null && returnCode.ToString() == "FAIL")
            {
                if (payData.GetValue("return_msg") != null)
                {
                    return payData.GetValue("return_msg").ToString();
                }
            }
            return "";
        }


    }

}
