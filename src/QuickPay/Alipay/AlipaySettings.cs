namespace QuickPay.Alipay
{
    public class AlipaySettings
    {
        /// <summary>默认签名字段的名称
        /// </summary>
        public const string DefaultSignFieldName = "sign";

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

        public class ReturnCode
        {
            /// <summary>是否调用成功
            /// </summary>
            public const string Success = "10000";
        }

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

        public class BillType
        {
            public const string Trade = "trade";
            public const string Signcustomer = "signcustomer";
        }

        public class TradeType
        {
            public const string App = "APP";
            public const string Wap = "WAP";
            public const string Page = "PAGE";
            public const string QrcodePay = "QRCODE_PAY";
            public const string BarcodePay = "BARCODE_PAY";
        }

        /// <summary>支付场景 
        /// </summary>
        public class Scene
        {
            //条码支付，取值：bar_code 
            public const string Barcode = "bar_code ";
            //声波支付，取值：wave_code
            public const string Wavecode = "wave_code";
        }
    }
}
