namespace QuickPay.Infrastructure.Requests
{
    public static class RequestExtensions
    {
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
