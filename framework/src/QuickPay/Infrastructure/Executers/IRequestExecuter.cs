using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using System.Threading.Tasks;

namespace QuickPay.Infrastructure.Executers
{
    /// <summary>请求执行器
    /// </summary>
    public interface IRequestExecuter
    {
        /// <summary>请求执行器执行
        /// </summary>
        Task<T> ExecuteAsync<T>(IPayRequest<T> request,QuickPayConfig config, QuickPayApp app) where T : PayResponse;

        /// <summary>请求签名
        /// </summary>
        Task<T> SignRequest<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app) where T : PayResponse;

    }
}
