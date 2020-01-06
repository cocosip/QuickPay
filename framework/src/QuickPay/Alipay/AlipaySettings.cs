namespace QuickPay.Alipay
{
    /// <summary>支付宝支付相关常量配置
    /// </summary>
    public class AlipaySettings
    {
        /// <summary>默认签名字段的名称
        /// </summary>
        public const string DefaultSignFieldName = "sign";

        /// <summary>扩展数据名称
        /// </summary>
        public class ExtraNames
        {
            /// <summary>支付宝BizContent
            /// </summary>
            public const string BizContentRequest = "BIZ_CONTENT";
        }

        /// <summary>产品码
        /// </summary>
        public class ProductCode
        {
            /// <summary>App支付的产品码
            /// </summary>
            public const string App = " QUICK_MSECURITY_PAY";

            /// <summary>Wap支付产品码
            /// </summary>
            public const string Wap = "QUICK_WAP_PAY";

            /// <summary>网站支付产品码
            /// </summary>
            public const string Page = "FAST_INSTANT_TRADE_PAY";

            /// <summary>面对面支付
            /// </summary>
            public const string FaceToFace = "FACE_TO_FACE_PAYMENT";
        }

        /// <summary>地址
        /// </summary>
        public class Urls
        {
            /// <summary>真实的基地址
            /// </summary>
            public const string Gateway = "https://openapi.alipay.com/gateway.do";

            /// <summary>沙盒基地址
            /// </summary>
            public const string SandboxGateway = "https://openapi.alipaydev.com/gateway.do";

        }

        /// <summary>调用结果
        /// </summary>
        public class ReturnCode
        {
            /// <summary>是否调用成功
            /// </summary>
            public const string Success = "10000";
        }

        /// <summary>通知
        /// </summary>
        public class NotifyReturn
        {
            /// <summary>成功
            /// </summary>
            public const string Success = "Success";

            /// <summary>失败
            /// </summary>
            public const string Fail = "Fail";

        }

        /// <summary>交易状态
        /// </summary>
        public class TradeStatus
        {
            /// <summary>交易创建，等待买家付款
            /// </summary>
            public const string WaitBuyerPay = "WAIT_BUYER_PAY";

            /// <summary>未付款交易超时关闭，或支付完成后全额退款
            /// </summary>
            public const string TradeClosed = "TRADE_CLOSED";

            /// <summary>交易支付成功
            /// </summary>
            public const string TradeSuccess = "TRADE_SUCCESS";

            /// <summary>交易结束，不可退款
            /// </summary>
            public const string TradeFinished = "TRADE_FINISHED";
        }

        /// <summary>支付宝账单类型
        /// </summary>
        public class BillType
        {
            /// <summary>交易
            /// </summary>
            public const string Trade = "trade";
            /// <summary>signcustomer
            /// </summary>
            public const string Signcustomer = "signcustomer";
        }

        /// <summary>交易类型
        /// </summary>
        public class TradeType
        {
            /// <summary>APP支付
            /// </summary>
            public const string App = "APP";
            /// <summary>手机端网页支付
            /// </summary>
            public const string Wap = "WAP";
            /// <summary>PC网站支付
            /// </summary>
            public const string Page = "PAGE";
            /// <summary>二维码支付
            /// </summary>
            public const string QrcodePay = "QRCODE_PAY";
            /// <summary>条码支付
            /// </summary>
            public const string BarcodePay = "BARCODE_PAY";
        }

        /// <summary>支付宝交易辅助类型
        /// </summary>
        public class ExtTradeType
        {
            /// <summary>下载订单
            /// </summary>
            public const string TradeBillDownload = "TRADE_BILL_DOWNLOAD";
            /// <summary>交易关闭
            /// </summary>
            public const string TradeClose = "TRADE_CLOSE";
            /// <summary>交易查询
            /// </summary>
            public const string TradeQuery = "TRADE_QUERY";
            /// <summary>交易退款
            /// </summary>
            public const string TradeRefund = "TRADE_REFUND";
            /// <summary>交易退款查询
            /// </summary>
            public const string TradeRefundQuery = "TRADE_REFUND_QUERY";
            /// <summary>交易取消
            /// </summary>
            public const string TradeCancel = "TRADE_CANCEL";

        }

        /// <summary>支付场景 
        /// </summary>
        public class Scene
        {
            /// <summary>条码支付，取值：bar_code 
            /// </summary>
            public const string Barcode = "bar_code ";

            /// <summary>声波支付，取值：wave_code
            /// </summary>
            public const string Wavecode = "wave_code";
        }
    }
}