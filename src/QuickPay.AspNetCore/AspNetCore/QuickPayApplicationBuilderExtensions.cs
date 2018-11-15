using Microsoft.AspNetCore.Builder;

namespace QuickPay.AspNetCore
{
    public static class QuickPayApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseQuickPay(this IApplicationBuilder app)
        {
            app.ApplicationServices.UseQuickPay();
            return app;
        }
    }
}
