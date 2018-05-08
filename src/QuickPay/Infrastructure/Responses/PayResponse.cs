using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Infrastructure.Responses
{
    public abstract class PayResponse
    {
        public PayData PayData { get; set; }
    }
}
