﻿using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WeChatPay.Responses
{
    /// <summary>关闭订单
    /// </summary>
    public class OrderCloseResponse : WeChatPayCommonResponse
    {
        /// <summary>AppId
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>MchId商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }
    }
}
