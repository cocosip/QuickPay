using Autofac;
using Autofac.Core;
using AutoMapper;
using DotCommon.Configurations;
using DotCommon.Dependency;
using QuickPay.Middleware;
using QuickPay.UnitTest.Middleware.Pipeline;

namespace QuickPay.UnitTest
{
    using DotCommonConfiguration = DotCommon.Configurations.Configuration;
    public class TestBase
    {
        public TestBase()
        {
            var builder = new ContainerBuilder();
            var configuration = DotCommonConfiguration.Create()
                 .UseAutofac(builder)
                 .RegisterCommonComponent()
                 .UseMemoryCache()
                 .UseJson4Net()
                 .UseLog4Net()
                 .AddQuickPay("QuickPayConfig.xml");
            // IocManager.GetContainer().Register(typeof(QuickPayMiddleware));
            // IocManager.GetContainer().Register(typeof(ErrorHandlerMiddleware));
            // builder.RegisterType<ErrorHandlerMiddleware>();
            //     .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(QuickPayExecuteDelegate),
            //(pi, ctx) => pi));


            configuration.AutofacBuild()
                .UseQuickPay();

            //AutoMapper映射
            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });



        }

    }
}
