using Autofac;
using Autofac.Core;
using AutoMapper;
using DotCommon.Configurations;
using QuickPay.Middleware;
using QuickPay.UnitTest.Middleware.Pipeline;

namespace QuickPay.UnitTest
{
    public class TestBase
    {
        static TestBase()
        {
            //var builder = new ContainerBuilder();
            //var configuration = DotCommonConfiguration.Create()
            //     .UseAutofac(builder)
            //     .RegisterCommonComponent()
            //     .UseMemoryCache()
            //     .UseJson4Net()
            //     .UseLog4Net()
            //     .AddQuickPay("QuickPayConfig.xml")
            //     .AutofacBuild()
            //     .UseQuickPay();

            ////AutoMapper映射
            //Mapper.Initialize(config =>
            //{
            //    config.CreateQuickPayMaps();
            //});



        }

    }
}
