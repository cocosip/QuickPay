using System;

namespace QuickPay.Infrastructure.RequestData
{
    /// <summary>支付数据特性标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PayElementAttribute: Attribute
    {
        /// <summary>是否为必须的参数
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>实际的参数名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        public PayElementAttribute(string name, bool isRequired = true)
        {
            Name = name;
            IsRequired = isRequired;
        }
    }
}
