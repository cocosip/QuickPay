using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{

    public interface IPayRequest
    {
        string Provider { get; }
        string UniqueId { get; set; }
        string BusinessCode { get; set; }
        string TradeTypeName { get; }
        string SignFieldName { get; }
        string SignTypeName { get; }
        void SetNecessary(QuickPayConfig config, QuickPayApp app);
    }

    public interface IPayRequest<in T> : IPayRequest where T : PayResponse
    {
    }
}
