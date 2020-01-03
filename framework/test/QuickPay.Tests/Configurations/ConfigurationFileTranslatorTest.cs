using DotCommon.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using DotCommon.AutoMapper;
using DotCommon.Utility;
using DotCommon.IO;

namespace QuickPay.Tests.Configurations
{
    public class ConfigurationFileTranslatorTest
    {


        [Fact]
        public void TranslateToText_Test()
        {
            var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper("QuickPayConfig.xml");
            var xml = ConfigurationFileHelper.TranslateToText(wrapper);
            var filePath = "test_quickpay_config.xml";
            File.WriteAllText(filePath, xml);

            var wrapper2 = ConfigurationFileHelper.TranslateToConfigWrapper(filePath);
            Assert.Equal(wrapper.AlipayConfig.WebGateway, wrapper2.AlipayConfig.WebGateway);

            FileHelper.DeleteIfExists(filePath);

            var wrapper3 = new ConfigWrapper(wrapper.AlipayConfig, wrapper.WeChatPayConfig);
            Assert.Equal(wrapper.AlipayConfig, wrapper3.AlipayConfig);
            Assert.Equal(wrapper.WeChatPayConfig, wrapper3.WeChatPayConfig);

        }


    }
}
