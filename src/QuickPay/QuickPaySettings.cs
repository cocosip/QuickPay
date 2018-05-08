namespace QuickPay
{
    public class QuickPaySettings
    {
        /// <summary>日志名称
        /// </summary>
        public const string LoggerName = "QuickPay";

        /// <summary>当前程序集名称
        /// </summary>
        public const string AssemblyName = "QuickPay";

        /// <summary>默认业务代码
        /// </summary>
        public const string DefaultBusinessCode = "Default";

        public class ConfigFormat
        {
            public const string Json = "JSON";
            public const string Xml = "XML";
        }

        /// <summary>支付通道,微信,支付宝,银联等
        /// </summary>
        public class Provider
        {
            public const string Alipay = "QuickPay.Alipay";
            public const string WechatPay = "QuickPay.WechatPay";
        }

        public class RequestHandler
        {
            public const string Execute = "ExecuteRequest";
            public const string Sign = "SignRequest";
        }

    }
}
