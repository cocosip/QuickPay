using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class PostgreSqlPaymentStore : BasePostgreSqlStore, IPaymentStore
    {
        /// <summary>Ctor
        /// </summary>
        public PostgreSqlPaymentStore(ILogger<BasePostgreSqlStore> logger, QuickPayPostgreSqlOption option) : base(logger, option)
        {
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //根据UniqueId查询支付信息
                    var queryPayment = await GetByUniqueIdAsync(payment.UniqueId);

                    var sql = "";
                    if (queryPayment == null || queryPayment.AppId.IsNullOrWhiteSpace())
                    {
                        //创建
                        sql = $@"INSERT INTO {GetSchemaPaymentTableName()} (""uniqueid"",""pay_platid"",""appid"",""out_tradeno"",""trade_type"",""business_code"",""transactionid"",""amount"",""pay_statusid"",""pay_object"",""describe"") VALUES (@UniqueId,@PayPlatId,@AppId,@OutTradeNo,@TradeType,@BusinessCode,@TransactionId,@Amount,@PayStatusId,@PayObject,@Describe)";
                    }
                    else
                    {
                        //修改
                        sql = $@"UPDATE {GetSchemaPaymentTableName()} SET ""uniqueid""=@UniqueId,""pay_platid""=@PayPlatId,""appid""=@AppId,""out_tradeno""=@AppId,""trade_type""=@TradeType,""business_code""=@BusinessCode,""transactionid""=@TransactionId,""amount""=@Amount,""pay_statusid""=@PayStatusId,""pay_object""=@PayObject,""describe""=@Describe";
                    }
                    await connection.ExecuteAsync(sql, payment);

                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"创建或者修改Payment出错,UniqueId@{payment.UniqueId} {ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaPaymentTableName()} WHERE ""pay_platid""=@PayPlatId AND ""appid""=@AppId AND ""out_tradeno""=@OutTradeNo";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { PayPlatId = payPlatId, AppId = appId, OutTradeNo = outTradeNo });
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"获取支付信息Payment出错,PayPlatId:{payPlatId},AppId:{appId},OutTradeNo:{outTradeNo}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        public async Task<Payment> GetByTransactionId(int payPlatId, string appId, string transactionId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaPaymentTableName()} WHERE ""pay_platid""=@PayPlatId AND ""appid""=@AppId AND ""transactionid""=@TransactionId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { PayPlatId = payPlatId, AppId = appId, TransactionId = transactionId });
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"获取支付信息Payment出错,PayPlatId:{payPlatId},AppId:{appId},TransactionId@{transactionId}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaPaymentTableName()} WHERE ""uniqueid""=@UniqueId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { UniqueId = uniqueId });
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"根据UniqueId获取支付信息Payment出错,UniqueId [{uniqueId}].{ex.Message}");
                throw;
            }
        }
    }
}
