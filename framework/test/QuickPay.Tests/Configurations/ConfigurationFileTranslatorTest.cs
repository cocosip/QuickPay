using DotCommon.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace QuickPay.Tests.Configurations
{
    public class ConfigurationFileTranslatorTest
    {
        static IServiceProvider _provider;
        static ConfigurationFileTranslatorTest()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddDotCommon()
                .AddQuickPay(o => { });

            _provider = services.BuildServiceProvider();

        }

        [Fact]
        public void TranslateToText_Test()
        {
            var translator = _provider.GetService<IConfigurationFileTranslator>();

            var configWapper = translator.TranslateToConfigWapper("QuickPayConfig.xml", QuickPaySettings.ConfigFormat.Xml);
            var xml = translator.TranslateToText(configWapper, QuickPaySettings.ConfigFormat.Xml);

            var fileName = "test_quickpay_config.xml";
            File.WriteAllText(fileName, xml);

            Assert.True(File.Exists(fileName));

            var configWapper2 = translator.TranslateToConfigWapper(fileName, QuickPaySettings.ConfigFormat.Xml);
            Assert.Equal(configWapper.AlipayConfig.WebGateway, configWapper2.AlipayConfig.WebGateway);

        }


    }
}
