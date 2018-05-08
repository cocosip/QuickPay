using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{

    public interface IPayRequest
    {
        string Provider { get; }
    }

    public interface IPayRequest<in T> : IPayRequest where T : PayResponse
    {
    }
}
