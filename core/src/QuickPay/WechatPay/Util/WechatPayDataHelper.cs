﻿using DotCommon.Extensions;
using DotCommon.Serializing;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuickPay.WechatPay.Util
{
    /// <summary>微信支付PayData操作帮主类
    /// </summary>
    public class WechatPayDataHelper
    {
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>Ctor
        /// </summary>
        public WechatPayDataHelper(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>xml转换为PayData
        /// </summary>
        public PayData FromXml(string xml)
        {
            var payData = new PayData();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var root = xmlDoc.DocumentElement;
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlElement xe = (XmlElement)node;
                    payData.SetValue(xe.Name, xe.InnerText);
                }
            }
            return payData;
        }


        /// <summary>将PayData转换成Xml
        /// </summary>
        public string ToXml(PayData payData)
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

        /// <summary>微信AppId
        /// </summary>
        public string GetWechatAppId(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "appid").ToString();
        }

        /// <summary>微信交易号
        /// </summary>
        public string GetWechatOutTradeNo(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
        }

        /// <summary>获取微信支付订单号(微信系统中的)
        /// </summary>
        public string GetTransactionId(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "transaction_id").ToString();
        }

        /// <summary>微信支付总金额
        /// </summary>
        public decimal GetTotalFeeYuan(PayData payData)
        {
            return Convert.ToInt32(payData.GetValue(x => x.Key.ToLower() == "total_fee")) / 100M;
        }

        /// <summary>字典转换成Json
        /// </summary>
        public string DictToJson(Dictionary<string, object> dict)
        {
            return _jsonSerializer.Serialize(dict);
        }
    }
}
