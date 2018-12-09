using Microsoft.AspNetCore.Builder;

namespace QuickPay.AspNetCore
{
    public static class QuickPayApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureQuickPay(this IApplicationBuilder app)
        {
            app.ApplicationServices.ConfigureQuickPay();
            return app;
        }
    }
}
