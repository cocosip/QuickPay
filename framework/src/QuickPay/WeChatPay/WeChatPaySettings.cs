namespace QuickPay.WeChatPay
{
    /// <summary>微信支付常量配置
    /// </summary>
    public class WeChatPaySettings
    {
        /// <summary>默认签名字段的名称
        /// </summary>
        public const string DefaultSignFieldName = "sign";
        /// <summary>微信内H5唤起支付签名字段名称
        /// </summary>
        public const string JsApiPaySignFieldName = "paySign";
        /// <summary>微信小程序唤起支付签名字段名
        /// </summary>
        public const string MiniProgramPaySignFieldName = "paySign";

        /// <summary>微信JsApi支付 JsSdk config签名字段名
        /// </summary>
        public const string JsSdkConfigSignFieldName = "signature";

        /// <summary>H5支付场景字段名
        /// </summary>
        public const string H5SceneInfoFieldName = "h5_info";

        /// <summary>状态
        /// </summary>
        public const string AuthenticationState = "QuickPay.WechatPay.AuthenticationState";

        /// <summary>H5场景
        /// </summary>
        public class H5SceneInfoType
        {
            /// <summary>IOS
            /// </summary>
            public const string IOS = "IOS";

            /// <summary>Android
            /// </summary>
            public const string Android = "Android";

            /// <summary>Wap
            /// </summary>
            public const string Wap = "Wap";
        }

        /// <summary>付款到帐号CheckName
        /// </summary>
        public class TransferToAccountCheckName
        {
            /// <summary>不校验真实姓名 
            /// </summary>
            public const string NoCheck = "NO_CHECK";

            /// <summary>强校验真实姓名
            /// </summary>
            public const string ForceCheck = "FORCE_CHECK";
        }

        /// <summary>压缩类型
        /// </summary>
        public class TarType
        {
            /// <summary>GZIP
            /// </summary>
            public const string Gzip = "GZIP";
        }

        /// <summary>订单类型
        /// </summary>
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

        /// <summary>交易类型
        /// </summary>
        public class TradeType
        {
            /// <summary>Native支付
            /// </summary>
            public const string Native = "NATIVE";

            /// <summary>JsApi公众号支付
            /// </summary>
            public const string JsApi = "JSAPI";

            /// <summary>App支付
            /// </summary>
            public const string App = "APP";

            /// <summary>H5支付
            /// </summary>
            public const string H5 = "MWEB";

            /// <summary>扫码支付
            /// </summary>
            public const string Micropay = "MICROPAY";

            /// <summary>小程序传参的时候必须用JSAPI,但是本地记录需要用MINIPROGRAM
            /// </summary>
            public const string MiniProgram = "MINIPROGRAM";

            /// <summary>企业付款到帐号
            /// </summary>
            public const string TransferToAccount = "TRANSFER";

            /// <summary>企业付款到银行卡
            /// </summary>
            public const string TransferToBankCard = "TRANSFERTOBANKCARD";
        }

        /// <summary>扩展交易类型,用于辅助
        /// </summary>
        public class ExtTradeType
        {
            /// <summary>下载订单
            /// </summary>
            public const string DownloadBill = "DOWNLOAD_BILL";
            /// <summary>订单关闭
            /// </summary>
            public const string OrderClose = "ORDER_CLOSE";
            /// <summary>订单查询
            /// </summary>
            public const string OrderQuery = "ORDER_QUERY";
            /// <summary>订单退款
            /// </summary>
            public const string OrderRefund = "ORDER_REFUND";
            /// <summary>订单退款查询
            /// </summary>
            public const string OrderRefundQuery = "ORDER_REFUND_QUERY";
            /// <summary>上报
            /// </summary>
            public const string Report = "REPORT";
        }

        /// <summary>订单查询的状态
        /// </summary>
        public class TradeStatus
        {
            /// <summary>支付成功
            /// </summary>
            public const string Success = "SUCCESS";

            /// <summary>转入退款
            /// </summary>
            public const string Refund = "REFUND";

            /// <summary>未支付
            /// </summary>
            public const string NotPay = "NOTPAY";

            /// <summary>已关闭
            /// </summary>
            public const string Close = "CLOSED";

            /// <summary>已撤销(刷卡支付)
            /// </summary>
            public const string Revoked = "REVOKED";

            /// <summary>用户正在支付中
            /// </summary>
            public const string UserPaying = "USERPAYING";

            /// <summary>支付失败(其他原因,如银行返回失败)
            /// </summary>
            public const string PayError = "PAYERROR";

        }

        /// <summary>授权认证Scope
        /// </summary>
        public class Scope
        {
            /// <summary>snsapi_base
            /// </summary>
            public const string Base = "snsapi_base";

            /// <summary>snsapi_userinfo
            /// </summary>
            public const string UserInfo = "snsapi_userinfo";
        }

        /// <summary>返回结果
        /// </summary>
        public class ReturnCode
        {
            /// <summary>SUCCESS
            /// </summary>
            public const string Success = "SUCCESS";
            /// <summary>FAIL
            /// </summary>
            public const string Fail = "FAIL";
        }

        /// <summary>返回消息
        /// </summary>
        public class ReturnMsg
        {
            /// <summary>OK
            /// </summary>
            public const string Ok = "OK";

        }

        /// <summary>执行结果(业务结果)
        /// </summary>
        public class ResultCode
        {
            /// <summary>SUCCESS
            /// </summary>
            public const string Success = "SUCCESS";
            /// <summary>FAIL
            /// </summary>
            public const string Fail = "FAIL";
        }

        /// <summary>通知
        /// </summary>
        public class NotifyReturn
        {
            /// <summary>SUCCESS
            /// </summary>
            public const string Success = "SUCCESS";
            /// <summary>FAIL
            /// </summary>
            public const string Fail = "FAIL";
        }

        /// <summary>Sign签名类型
        /// </summary>
        public class SignType
        {
            /// <summary>MD5
            /// </summary>
            public const string Md5 = "MD5";
            /// <summary>HMAC-SHA256
            /// </summary>
            public const string HmacSha256 = "HMAC-SHA256";
            /// <summary>SHA1
            /// </summary>
            public const string Sha1 = "SHA1";
        }

    }
}