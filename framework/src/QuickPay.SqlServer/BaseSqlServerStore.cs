using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace QuickPay
{
    /// <summary>基础抽象存储
    /// </summary>
    public abstract class BaseSqlServerStore
    {
        /// <summary>SqlServer配置信息
        /// </summary>
        protected QuickPaySqlServerOption Option { get; set; }
        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BaseSqlServerStore(ILoggerFactory loggerFactory, QuickPaySqlServerOption option)
        {
            Option = option;
            Logger = loggerFactory.CreateLogger(QuickPaySettings.LoggerName);
        }

        /// <summary>获取连接
        /// </summary>
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(Option.DbConnectionString);
        }

    }
}
