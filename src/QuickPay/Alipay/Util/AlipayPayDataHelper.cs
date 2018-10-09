using DotCommon.Serializing;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Collections.Generic;

namespace QuickPay.Alipay.Util
{
    public class AlipayPayDataHelper
    {
        private IJsonSerializer _jsonSerializer;

        public AlipayPayDataHelper(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public PayData FromJson(string json)
        {
            var sortedDict = _jsonSerializer.Deserialize<SortedDictionary<string, object>>(json);
            var newPayData = new PayData(sortedDict);
            return newPayData;
        }

        /// <summary>序列化成Json对象
        /// </summary>
        public string ToJson(PayData payData)
        {
            return _jsonSerializer.Serialize(payData.GetValues());
        }

        /// <summary>支付宝AppId
        /// </summary>
        public string GetAlipayAppId(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "appid").ToString();
        }

        /// <summary>获取交易号
        /// </summary>
        public string GetAlipayOutTradeNo(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
        }


        /// <summary>获取支付宝系统中的流水号
        /// </summary>
        public string GetAlipayTradeNo(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "trade_no").ToString();
        }

        /// <summary>获取支付宝支付金额
        /// </summary>
        public decimal GetTotalAmount(PayData payData)
        {
            return Convert.ToDecimal(payData.GetValue(x => x.Key.ToLower() == "total_amount"));
        }

        /// <summary>获取支付宝交易状态
        /// </summary>
        public string GetTradeStatus(PayData payData)
        {
            return payData.GetValue(x => x.Key.ToLower() == "trade_status").ToString();
        }
    }
}
