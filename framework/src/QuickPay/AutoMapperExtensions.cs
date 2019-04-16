using AutoMapper;
using DotCommon.AutoMapper;
using System.Collections.Generic;
using System.Reflection;

namespace QuickPay
{
    /// <summary>自动映射
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>创建快捷支付的自动映射
        /// </summary>
        public static void CreateQuickPayMaps(this IMapperConfigurationExpression configuration)
        {
            var assemblies = new List<Assembly>()
            {
                typeof(QuickPayLoggerName).Assembly
            };
            AutoAttributeMapperHelper.CreateAutoAttributeMappings(assemblies, configuration);
        }
    }
}
