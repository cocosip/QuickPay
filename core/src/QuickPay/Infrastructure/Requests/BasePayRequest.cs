using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{
    public abstract class BasePayRequest<T> : IPayRequest<T> where T : PayResponse
    {
        public abstract string Provider { get; }

        public string UniqueId { get; set; }

        public string BusinessCode { get; set; } = QuickPaySettings.DefaultBusinessCode;
        public abstract string TradeTypeName { get; }
        public abstract string SignFieldName { get; }
        public abstract string SignTypeName { get; set; }

        public virtual void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {

        }
    }
}
