namespace QuickPay
{
    /// <summary>QuickPay常量配置信息
    /// </summary>
    public class QuickPaySettings
    {

        /// <summary>默认业务代码
        /// </summary>
        public const string DefaultBusinessCode = "Default";

        /// <summary>支付通道,微信,支付宝,银联等
        /// </summary>
        public class Provider
        {
            /// <summary>支付宝
            /// </summary>
            public const string Alipay = "QuickPay.Alipay";

            /// <summary>微信支付
            /// </summary>
            public const string WeChatPay = "QuickPay.WeChatPay";
        }

        /// <summary>请求操作
        /// </summary>
        public class RequestHandler
        {
            /// <summary>Execute执行
            /// </summary>
            public const string Execute = "ExecuteRequest";

            /// <summary>签名
            /// </summary>
            public const string Sign = "SignRequest";
        }

        /// <summary>微信支付异步通知UrlFragments
        /// </summary>
        public class WeChatPayNotifyUrlFragments
        {
            /// <summary>微信支付的相关通知
            /// </summary>
            public const string PaymentUrlFragments = "/QuickPay/WeChatPay/PaymentNotify";

        }

        /// <summary>支付宝异步通知UrlFragments
        /// </summary>
        public class AlipayNotifyUrlFragments
        {
            /// <summary>支付宝支付相关通知
            /// </summary>
            public const string PaymentUrlFragments = "/QuickPay/Alipay/PaymentNotify";
        }

        /// <summary>Content-Type 类型
        /// </summary>
        public class ContentTypes
        {
            /// <summary>Json格式数据
            /// </summary>
            public const string ApplicationJson = "application/json";

            /// <summary>Xml
            /// </summary>
            public const string ApplicationXml = "application/xml";
        }

    }
}
