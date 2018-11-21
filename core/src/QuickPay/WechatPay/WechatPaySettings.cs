namespace QuickPay.WechatPay
{
    public class WechatPaySettings
    {
        /// <summary>默认签名字段的名称
        /// </summary>
        public const string DefaultSignFieldName = "sign";
        /// <summary>微信内H5唤起支付签名字段名称
        /// </summary>
        public const string JsApiPaySignFieldName = "paySign";

        /// <summary>微信JsApi支付 JsSdk config签名字段名
        /// </summary>
        public const string JsSdkConfigSignFieldName = "signature";

        /// <summary>H5支付场景字段名
        /// </summary>
        public const string H5SceneInfoFieldName = "h5_info";

        /// <summary>状态
        /// </summary>
        public const string AuthenticationState = "QuickPay.WechatPay.AuthenticationState";

        public class H5SceneInfoType
        {
            public const string IOS = "IOS";
            public const string Android = "Android";
            public const string Wap = "Wap";
        }

        /// <summary>压缩类型
        /// </summary>
        public class TarType
        {
            public const string Gzip = "GZIP";
        }

        public class BillType
        {
            /// <summary>返回当日所有订单信息，默认值
            /// </summary>
            public const string All = "ALL";
            /// <summary>返回当日成功支付的订单
            /// </summary>
            public const string Success = "SUCCESS";

            /// <summary>返回当日退款订单
            /// </summary>
            public const string Refund = "REFUND";

            /// <summary>返回当日充值退款订单（相比其他对账单多一栏“返还手续费”）
            /// </summary>
            public const string RechargeRefund = "RECHARGE_REFUND";
        }

        public class TradeType
        {
            public const string Native = "NATIVE";

            public const string JsApi = "JSAPI";

            public const string App = "APP";

            public const string H5 = "MWEB";

            public const string Micropay = "MICROPAY";

            public const string MiniProgram = "MINIPROGRAM";
        }

        public class Scope
        {
            public const string Base = "snsapi_base";

            public const string UserInfo = "snsapi_userinfo";
        }

        public class ReturnCode
        {
            public const string Success = "SUCCESS";
            public const string Fail = "FAIL";
        }

        public class ResultCode
        {
            public const string Success = "SUCCESS";
            public const string Fail = "FAIL";
        }

        public class SignType
        {
            public const string Md5 = "MD5";
            public const string Sha1 = "SHA1";
        }

    }
}
