using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{
    /// <summary>泛型抽象类请求
    /// </summary>
    public abstract class BasePayRequest<T> : IPayRequest<T> where T : PayResponse
    {
        /// <summary>管道名
        /// </summary>
        public abstract string Provider { get; }

        /// <summary>唯一Id
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>业务代码
        /// </summary>
        public string BusinessCode { get; set; } = QuickPaySettings.DefaultBusinessCode;

        /// <summary>交易类型名称
        /// </summary>
        public abstract string TradeTypeName { get; }

        /// <summary>签名字段名称
        /// </summary>
        public abstract string SignFieldName { get; }

        /// <summary>签名类型名称
        /// </summary>
        public abstract string SignTypeName { get; set; }

        /// <summary>
        /// </summary>
        public virtual void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {

        }
    }
}
