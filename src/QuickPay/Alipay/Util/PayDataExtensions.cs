using DotCommon.Dependency;
using DotCommon.Serializing;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Collections.Generic;

namespace QuickPay.Alipay.Util
{
    public static class PayDataExtensions
    {
        public static PayData FromJson(this PayData payData, string json)
        {
            var sortedDict = IocManager.GetContainer().Resolve<IJsonSerializer>()
                .Deserialize<SortedDictionary<string, object>>(json);
            var newPayData = new PayData(sortedDict);
            return newPayData;
        }

        /// <summary>序列化成Json对象
        /// </summary>
        public static string ToJson(this PayData payData)
        {
            var jsonSerializer = IocManager.GetContainer().Resolve<IJsonSerializer>();
            return jsonSerializer.Serialize(payData.GetValues());
        }

        /// <summary>支付宝AppId
        /// </summary>
        public static string GetAlipayAppId(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "appid").ToString();
        }

        /// <summary>获取交易号
        /// </summary>
        public static string GetAlipayOutTradeNo(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
        }


        /// <summary>获取支付宝系统中的流水号
        /// </summary>
        public static string GetAlipayTradeNo(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "trade_no").ToString();
        }

        /// <summary>获取支付宝支付金额
        /// </summary>
        public static decimal GetTotalAmount(this PayData payData)
        {
            return Convert.ToDecimal(payData.GetValue(x => x.Key.ToLower() == "total_amount"));
        }

        /// <summary>获取支付宝交易状态
        /// </summary>
        public static string GetTradeStatus(this PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "trade_status").ToString();
        }

    }
}
