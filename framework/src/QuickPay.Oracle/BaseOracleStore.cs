using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using QuickPay.Configurations;


namespace QuickPay
{
    /// <summary>基础抽象存储
    /// </summary>
    public abstract class BaseOracleStore
    {
        /// <summary>SqlServer配置信息
        /// </summary>
        protected QuickPayOracleOption Option { get; set; }
        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BaseOracleStore(QuickPayOracleOption option, ILogger<QuickPayLoggerName> logger)
        {
            Option = option;
            Logger = logger;
        }

        /// <summary>获取连接
        /// </summary>
        protected OracleConnection GetConnection()
        {
            return new OracleConnection(Option.DbConnectionString);
        }

    }
}
