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
        public BaseSqlServerStore(ILogger<BaseSqlServerStore> logger, QuickPaySqlServerOption option)
        {
            Logger = logger;
            Option = option;
        }

        /// <summary>获取连接
        /// </summary>
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(Option.DbConnectionString);
        }

    }
}
