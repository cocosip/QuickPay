using QuickPay.Alipay.Requests;
using QuickPay.WechatPay.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Infrastructure.Requests
{
    /// <summary>请求类型查询器
    /// </summary>
    public class RequestTypeFinder : IRequestTypeFinder
    {
        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        public List<Type> FindPaymentStoreTypies()
        {
            var typies = new List<Type>
            {
                //支付宝
                typeof(AppTradePayRequest),
                typeof(BarcodeTradePayRequest),
                typeof(PageTradePayRequest),
                typeof(QrcodeTradePayRequest),
                typeof(WapTradePayRequest),

                //微信
                typeof(AppUnifiedOrderRequest),
                typeof(H5UnifiedOrderRequest),
                typeof(JsApiUnifiedOrderRequest),
                typeof(MicropayUnifiedOrderRequest),
                typeof(NativeMode1UnifiedOrderRequest),
                typeof(NativeMode2UnifiedOrderRequest),
                typeof(MiniProgramUnifiedOrderRequest)
            };
            return typies;
        }

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        public List<Type> FindPaymentStoreTypies(Func<Type, bool> selector)
        {
            return FindPaymentStoreTypies().Where(selector).ToList();
        }

        /// <summary>获取需要进行退款存储的
        /// </summary>
        public List<Type> FindRefundStoreTypies()
        {
            var typies = new List<Type>
            {
                //支付宝
                typeof(TradeRefundRequest),

                //微信
                typeof(OrderRefundRequest)
            };
            return typies;
        }

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        public List<Type> FindRefundStoreTypies(Func<Type, bool> selector)
        {
            return FindRefundStoreTypies().Where(selector).ToList();
        }
    }
}
