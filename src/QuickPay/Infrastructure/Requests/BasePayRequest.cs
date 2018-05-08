using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{
    public abstract class BasePayRequest<T> : IPayRequest<T> where T : PayResponse
    {
        public abstract string Provider { get; }
        public abstract string SignFieldName { get; }
    }
}
