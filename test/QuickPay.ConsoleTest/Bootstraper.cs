using Autofac;
using AutoMapper;
using Castle.Windsor;
using DotCommon.Configurations;

namespace QuickPay.ConsoleTest
{
    using DotCommonConfiguration = DotCommon.Configurations.Configuration;
    public class Bootstraper
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            //var windsorContainer = new WindsorContainer();
            var configuration = DotCommonConfiguration.Create()
                 .UseAutofac(builder)
                 //.UseCastleWindsor(windsorContainer)
                 .RegisterCommonComponent()
                 .UseJson4Net()
                 .UseLog4Net()
                 .UseMemoryCache()
                 .AddQuickPay("QuickPayConfig.xml")
                 .AutofacBuild()
                 .UseQuickPay();

            //AutoMapper映射
            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });

        }
    }
}
