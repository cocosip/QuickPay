﻿using DotCommon.Configurations;
using DotCommon.Dependency;
using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Middleware;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Middleware;
using System;

namespace QuickPay
{
    public static class ConfigurationExtensions
    {

        public static Configuration AddQuickPay(this Configuration configuration, string file, string format = QuickPaySettings.ConfigFormat.Xml)
        {
            IocManager.GetContainer().Register<QuickPayConfigFile>(new QuickPayConfigFile()
            {
                FileName = file,
                Format = format
            });
            QuickPayRegister.RegisterQuickPay(IocManager.GetContainer(), new AlipayConfig(), new WechatPayConfig());
            return configuration;
        }

        public static Configuration AddQuickPay(this Configuration configuration, Func<AlipayConfig> alipayConfig, Func<WechatPayConfig> wechatPayConfig)
        {
            IocManager.GetContainer().Register<QuickPayConfigFile>(new QuickPayConfigFile()
            {
                FileName = "",
                Format = ""
            });
            QuickPayRegister.RegisterQuickPay(IocManager.GetContainer(), alipayConfig(), wechatPayConfig());
            return configuration;
        }

        public static Configuration UseQuickPay(this Configuration configuration)
        {
            var configFile = IocManager.GetContainer().Resolve<QuickPayConfigFile>();
            if (!configFile.FileName.IsNullOrWhiteSpace() && !configFile.Format.IsNullOrWhiteSpace())
            {
                var configLoader = IocManager.GetContainer().Resolve<QuickPayConfigLoader>();
                var alipayConfig = IocManager.GetContainer().Resolve<AlipayConfig>();
                var wechatPayConfig = IocManager.GetContainer().Resolve<WechatPayConfig>();

                var configWapper = configLoader.LoadConfigWapper(configFile.FileName, configFile.Format);
                if (configWapper != null && configWapper.AlipayConfig != null && configWapper.WechatPayConfig != null)
                {
                    alipayConfig.SelfCopy(configWapper.AlipayConfig);
                    wechatPayConfig.SelfCopy(configWapper.WechatPayConfig);
                }
            }
            //Pipeline
            var pipelineBuilder = IocManager.GetContainer().Resolve<IQuickPayPipelineBuilder>();
            pipelineBuilder.UseMiddleware<SetNecessaryMiddleware>();

            pipelineBuilder.UseMiddleware<AlipayPayDataTransformMiddleware>();
            pipelineBuilder.UseMiddleware<AlipayRequestBuilderMiddleware>();
            pipelineBuilder.UseMiddleware<AlipaySignMiddleware>();
            pipelineBuilder.UseMiddleware<AlipayParseResponseMiddleware>();

            pipelineBuilder.UseMiddleware<WechatPayDataTransformMiddleware>();
            pipelineBuilder.UseMiddleware<WechatPaySignMiddleware>();
            pipelineBuilder.UseMiddleware<WechatPayRequestBuilderMiddleware>();
            pipelineBuilder.UseMiddleware<WechatPayParseResponseMiddleware>();

            return configuration;
        }

    }
}
