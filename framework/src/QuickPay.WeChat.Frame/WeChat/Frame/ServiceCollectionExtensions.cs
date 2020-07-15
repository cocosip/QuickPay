using Microsoft.Extensions.DependencyInjection;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Service;

namespace QuickPay.WeChat.Frame
{
    /// <summary>依赖注入
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>使用微信基础框架
        /// </summary>
        public static IServiceCollection AddWeChatFrame(this IServiceCollection services)
        {
            //Json结果格式化
            services
                .AddTransient<IWeChatAccessTokenStore, WeChatAccessTokenMemoryStore>()
                .AddTransient<IWeChatSdkTicketStore, WeChatSdkTicketMemoryStore>()
                .AddTransient<IWeChatAccessTokenService, WeChatAccessTokenService>()
                .AddTransient<IWeChatSdkTicketService, WeChatSdkTicketService>();

            return services;
        }
    }
}
