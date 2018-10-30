using Microsoft.AspNetCore.Builder;

namespace QuickPay.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseQuickPay(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.UseQuickPay();
            return builder;
        }
    }
}
