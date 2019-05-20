using QuickPay.Alipay.Requests;
using QuickPay.WeChatPay.Requests;
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
        public List<Type> FindPaymentStoreTypes()
        {
            var types = new List<Type>
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
            return types;
        }

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        public List<Type> FindPaymentStoreTypes(Func<Type, bool> selector)
        {
            return FindPaymentStoreTypes().Where(selector).ToList();
        }

        /// <summary>获取需要进行退款存储的
        /// </summary>
        public List<Type> FindRefundStoreTypes()
        {
            var types = new List<Type>
            {
                //支付宝
                typeof(TradeRefundRequest),

                //微信
                typeof(OrderRefundRequest)
            };
            return types;
        }

        /// <summary>获取需要进行支付存储的类型
        /// </summary>
        public List<Type> FindRefundStoreTypes(Func<Type, bool> selector)
        {
            return FindRefundStoreTypes().Where(selector).ToList();
        }


        /// <summary>获取需要进行转账存储的类型
        /// </summary>
        public List<Type> FindTransferTypes()
        {
            var typies = new List<Type>
            {
                //支付宝
                typeof(TransferToAccountRequest),

                //微信
                typeof(TransferToAccountRequest),
                typeof(TransferToBankCardRequest)
            };
            return typies;
        }

        /// <summary>获取需要进行转账存储的类型
        /// </summary>
        public List<Type> FindTransferTypes(Func<Type, bool> selector)
        {
            return FindTransferTypes().Where(selector).ToList();
        }
    }
}
