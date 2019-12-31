using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

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
        public BaseOracleStore(ILogger<BaseOracleStore> logger, QuickPayOracleOption option)
        {
            Logger = logger;
            Option = option;
        }

        /// <summary>获取连接
        /// </summary>
        protected OracleConnection GetConnection()
        {
            return new OracleConnection(Option.DbConnectionString);
        }

    }
}
