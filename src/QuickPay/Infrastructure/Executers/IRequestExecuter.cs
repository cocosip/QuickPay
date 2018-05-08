using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using System.Threading.Tasks;

namespace QuickPay.Infrastructure.Executers
{
    public interface IRequestExecuter
    {
        Task<T> ExecuteAsync<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse;

        Task<T> SignRequest<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse;

    }
}
