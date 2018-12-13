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

        /// <summary>PayData转化为Xml格式
        /// </summary>
        public static string ToXml(this PayData payData)
        {
            var values = payData.GetValues();
            if (!values.Any())
            {
                throw new Exception("WechatPayData数据为空!");
            }
            var sb = new StringBuilder();
            sb.Append($"<xml>");
            foreach (var kv in values)
            {
                if (kv.Value == null)
                {
                    throw new Exception($"WechatPayData内部含有值为null的字段,key:{kv.Key}");
                }
                if (kv.Value is int)
                {
                    sb.Append($"<{kv.Key}>{kv.Value}</{kv.Key}>");
                }
                else if (kv.Value is string)
                {
                    if (kv.Key.ToLower() != "sign")
                    {
                        //sb.Append($"<{kv.Key}><![CDATA[{kv.Value}]]></{kv.Key}>");
                        sb.Append($"<{kv.Key}><![CDATA[{kv.Value}]]></{kv.Key}>");
                    }
                }
                else
                {
                    throw new Exception("WechatPayData字段数据类型错误!");
                }
            }
            //包含签名,把签名放到最后面
            var signKv = values.FirstOrDefault(x => x.Key.ToLower() == "sign");
            if (!signKv.Key.IsNullOrWhiteSpace() && !signKv.Value.ToString().IsNullOrWhiteSpace())
            {
                sb.Append($"<{signKv.Key}><![CDATA[{signKv.Value}]]></{signKv.Key}>");
            }
            sb.Append($"</xml>");
            return sb.ToString();
        }

        /// <summary>xml转换为PayData
        /// </summary>
        public static PayData FromXml(this PayData payData, string xml)
        {
            var newPayData = new PayData();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var root = xmlDoc.DocumentElement;
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlElement xe = (XmlElement)node;
                    newPayData.SetValue(xe.Name, xe.InnerText);
                }
            }
            return newPayData;
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
