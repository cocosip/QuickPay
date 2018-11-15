using Microsoft.Extensions.Logging;
using QuickPay.Configurations;
using System.Data.SqlClient;

namespace QuickPay
{
    public abstract class BaseSqlServerStore
    {
        protected readonly QuickPaySqlServerOption _option;
        protected readonly ILogger _logger;
        public BaseSqlServerStore(QuickPaySqlServerOption option, ILogger<QuickPayLoggerName> logger)
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
