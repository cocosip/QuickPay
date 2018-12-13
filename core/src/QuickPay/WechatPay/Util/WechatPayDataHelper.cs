using DotCommon.Serializing;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Collections.Generic;

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
