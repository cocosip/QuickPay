using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{
    public abstract class BasePayRequest<T> : IPayRequest<T> where T : PayResponse
    {
        public abstract string Provider { get; }

        public string UniqueId { get; set; }

        public string BusinessCode { get; set; } = QuickPaySettings.DefaultBusinessCode;

        public abstract string SignFieldName { get; }
        public abstract string SignTypeName { get; }
    }
}
