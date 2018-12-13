using Microsoft.AspNetCore.Builder;

namespace QuickPay.AspNetCore
{
    /// <summary>
    /// </summary>
    public static class QuickPayApplicationBuilderExtensions
    {
        /// <summary>配置QuickPay
        /// </summary>
        public static IApplicationBuilder ConfigureQuickPay(this IApplicationBuilder app)
        {
            app.ApplicationServices.ConfigureQuickPay();
            return app;
        }
    }
}
