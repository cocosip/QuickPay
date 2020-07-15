using System.Collections.Generic;

namespace QuickPay.Infrastructure.Executers
{
    /// <summary>支付请求
    /// </summary>
    public interface IPayRequest
    {
        /// <summary>管道名
        /// </summary>
        string Provider { get; }

        /// <summary>唯一Id
        /// </summary>
        string UniqueId { get; set; }

        /// <summary>业务代码
        /// </summary>
        string BusinessCode { get; set; }

        /// <summary>交易类型名称
        /// </summary>
        string TradeTypeName { get; }

        /// <summary>签名字段名称
        /// </summary>
        string SignFieldName { get; }

        /// <summary>签名类型名称
        /// </summary>
        string SignTypeName { get; set; }

        /// <summary>扩展数据
        /// </summary>
        Dictionary<string, object> Extras { get; set; }

        /// <summary>设置必要参数
        /// </summary>
        void SetNecessary(AppInfo app);
    }

    /// <summary>泛型PayRequest
    /// </summary>
    public interface IPayRequest<in T> : IPayRequest where T : PayResponse
    {

    }
}
