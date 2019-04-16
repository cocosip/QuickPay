namespace QuickPay.Infrastructure.Requests
{
    /// <summary>请求扩展
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>日志格式化
        /// </summary>
        public static string GetLogFormat(this IPayRequest request, string message)
        {
            var providerName = "微信";
            if (request.Provider == QuickPaySettings.Provider.Alipay)
            {
                providerName = "支付宝";
            }

            return $"【{providerName}】:[Provider:{request.Provider},Request:{request.GetType()}],信息[{message}]";
        }
    }
}
