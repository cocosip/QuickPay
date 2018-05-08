using DotCommon.Dependency;
using DotCommon.Serializing;
using QuickPay.Infrastructure.RequestData;
using System.Collections.Generic;

namespace QuickPay.Alipay.Util
{
    public static class PayDataExtensions
    {
        public static PayData FromJson(this PayData payData, string json)
        {
            var sortedDict = IocManager.GetContainer().Resolve<IJsonSerializer>()
                .Deserialize<SortedDictionary<string, object>>(json);
            var newPayData = new PayData(sortedDict);
            return newPayData;
        }

        /// <summary>序列化成Json对象
        /// </summary>
        public static string ToJson(this PayData payData)
        {
            var jsonSerializer = IocManager.GetContainer().Resolve<IJsonSerializer>();
            return jsonSerializer.Serialize(payData.GetValues());
        }
    }
}
