using DotCommon.Dependency;
using DotCommon.Serializing;
using System.Collections.Generic;

namespace QuickPay.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToJson(this Dictionary<string, object> dictionary)
        {
            return IocManager.GetContainer().Resolve<IJsonSerializer>().Serialize(dictionary);
        }
    }
}
