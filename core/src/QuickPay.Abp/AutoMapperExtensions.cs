using AutoMapper;
using DotCommon.AutoMapper;
using QuickPay.Assist;
using System.Collections.Generic;
using System.Reflection;

namespace QuickPay
{
    /// <summary>Abp扩展存储时,AutoMapper映射
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>创建快捷支付Abp扩展自动映射
        /// </summary>
        public static void CreateQuickPayAbpExtensionMaps(this IMapperConfigurationExpression configuration)
        {
            var assemblies = new List<Assembly>()
            {
                typeof(AbpPayment).Assembly
            };
            AutoAttributeMapperHelper.CreateAutoAttributeMappings(assemblies, configuration);
        }
    }
}
