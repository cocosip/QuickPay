using AutoMapper;
using DotCommon.AutoMapper;
using QuickPay.PayAux;
using System.Collections.Generic;
using System.Reflection;

namespace QuickPay
{
    public static class AutoMapperExtensions
    {
        /// <summary>创建快捷支付Abp扩展自动映射
        /// </summary>
        public static void CreateQuickPayAbpMaps(this IMapperConfigurationExpression configuration)
        {
            var assemblies = new List<Assembly>()
            {
                typeof(AbpPayment).Assembly
            };
            AutoAttributeMapperHelper.CreateAutoAttributeMappings(assemblies, configuration);
        }
    }
}
