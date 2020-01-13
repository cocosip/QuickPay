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

        /// <summary>GetSchemaPaymentTableName
        /// </summary>
        protected string GetSchemaPaymentTableName()
        {
            return $@"""{Option.Schema}"".""{Option.PaymentTableName}""";
        }


        /// <summary>GetSchemaRefundTableName
        /// </summary>
        protected string GetSchemaRefundTableName()
        {
            return $@"""{Option.Schema}"".""{Option.RefundTableName}""";
        }

        /// <summary>GetSchemaTransferTableName
        /// </summary>
        protected string GetSchemaTransferTableName()
        {
            return $@"""{Option.Schema}"".""{Option.TransferTableName}""";
        }

    }
}
