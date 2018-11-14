using Microsoft.Extensions.Logging;
using QuickPay.Configurations;
using System.Data.SqlClient;

namespace QuickPay
{
    public abstract class BaseSqlServerStore
    {
        protected readonly QuickPayConfigurationOption _option;
        protected readonly ILogger _logger;
        public BaseSqlServerStore(QuickPayConfigurationOption option, ILogger<QuickPayLoggerName> logger)
        {
            _option = option;
            _logger = logger;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_option.DbConnectionString);
        }

    }
}
