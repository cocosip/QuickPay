using Microsoft.AspNetCore.Builder;

namespace QuickPay.AspNetCore.Mvc
{
    /// <summary>ApplicationBuilderExtensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>使用QuickPay
        /// </summary>
        public static IApplicationBuilder UseQuickPay(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.ConfigureQuickPay();
            return builder.UseQuickPayNotify();
        }


        /// <summary>使用通知
        /// </summary>
        private static IApplicationBuilder UseQuickPayNotify(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotifyMiddleware>();
        }


    }
}
