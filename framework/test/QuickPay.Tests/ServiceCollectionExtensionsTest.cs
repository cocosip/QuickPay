using DotCommon.AutoMapper;
using DotCommon.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Configurations;
using QuickPay.Exceptions;
using Xunit;

namespace QuickPay.Tests
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddQuickPay_Test()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddDotCommon()
                .AddDotCommonAutoMapper()
                ;
            services.AddQuickPay(o =>
            {
                o.ConfigSourceType = ConfigSourceType.FromClass;
            });
        }

    }
}
