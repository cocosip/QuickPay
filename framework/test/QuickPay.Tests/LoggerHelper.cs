using Microsoft.Extensions.Logging;

namespace QuickPay.Tests
{
    public static class LoggerHelper
    {
        public static ILogger<T> GetLogger<T>()
        {
            return new EmptyLogger<T>();
        }
    }
}
