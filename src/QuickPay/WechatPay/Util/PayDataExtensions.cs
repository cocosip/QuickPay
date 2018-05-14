﻿using DotCommon.Extensions;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuickPay.WechatPay.Util
{
    public static class PayDataExtensions
    {

        public static string ToUrl(this PayData payData)
        {
            var sb = new StringBuilder();
            foreach (var pair in payData.GetValues())
            {
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
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

        public static string ToXml(this PayData payData)
        {
            var values = payData.GetValues();
            if (!values.Any())
            {
                throw new Exception("WxPayData数据为空!");
            }
            var sb = new StringBuilder();
            sb.Append($"<xml>");
            foreach (var kv in values)
            {
                if (kv.Value == null)
                {
                    throw new Exception($"WxPayData内部含有值为null的字段,key:{kv.Key}");
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
                    throw new Exception("WxPayData字段数据类型错误!");
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

        /// <summary>微信AppId
        /// </summary>
        public static string GetWechatAppId(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "appid").ToString();
        }

        /// <summary>微信交易号
        /// </summary>
        public static string GetWechatOutTradeNo(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
        }

        /// <summary>获取微信支付订单号(微信系统中的)
        /// </summary>
        public static string GetTransactionId(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "transaction_id").ToString();
        }

        /// <summary>微信支付总金额
        /// </summary>
        public static decimal GetTotalFeeYuan(this PayData payData)
        {
            return Convert.ToInt32(payData.GetValue(x => x.Key.ToLower() == "total_fee")) / 100M;
        }
    }

}