using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class OraclePaymentStore : BaseOracleStore, IPaymentStore
    {

        /// <summary>Ctor
        /// </summary>
        public OraclePaymentStore(ILogger<BaseOracleStore> logger, QuickPayOracleOption option) : base(logger, option)
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
                        sql = $@"INSERT INTO {GetSchemaPaymentTableName()} (""UNIQUEID"",""PAY_PLATID"",""APPID"",""OUT_TRADENO"",""TRADE_TYPE"",""BUSINESS_CODE"",""TRANSACTIONID"",""AMOUNT"",""PAY_STATUSID"",""PAY_OBJECT"",""DESCRIBE"") VALUES (:UniqueId,:PayPlatId,:AppId,:OutTradeNo,:TradeType,:BusinessCode,:TransactionId,:Amount,:PayStatusId,:PayObject,:Describe)";
                    }
                    else
                    {
                        //修改
                        sql = $@"UPDATE {GetSchemaPaymentTableName()} SET ""UNIQUEID""=:UniqueId,""PAY_PLATID""=:PayPlatId,""APPID""=:AppId,""OUT_TRADENO""=:AppId,""TRADE_TYPE""=:TradeType,""BUSINESS_CODE""=:BusinessCode,""TRANSACTIONID""=:TransactionId,""AMOUNT""=:Amount,""PAY_STATUSID""=:PayStatusId,""PAY_OBJECT""=:PayObject,""Describe""=:Describe";
                    }
                    await connection.ExecuteAsync(sql, payment);

                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"创建或者修改Payment出错,UniqueId:{payment.UniqueId} {ex.Message}");
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
                    var sql = $@"SELECT TOP 1 * FROM  {GetSchemaPaymentTableName()} WHERE ""PAY_PLATID""=:PayPlatId AND ""APPID""=:AppId AND ""OUT_TRADENO""=:OutTradeNo";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { PayPlatId = payPlatId, AppId = appId, OutTradeNo = outTradeNo });
                }
            }
            catch (OracleException ex)
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
                    var sql = $@"SELECT TOP 1 * FROM  {GetSchemaPaymentTableName()} WHERE ""PAY_PLATID""=:PayPlatId AND ""APPID""=:AppId AND ""TRANSACTIONID""=:TransactionId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { PayPlatId = payPlatId, AppId = appId, TransactionId = transactionId });
                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"获取支付信息Payment出错,PayPlatId:{payPlatId},AppId:{appId},TransactionId:{transactionId}.{ex.Message}");
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
                    var sql = $@"SELECT TOP 1 * FROM  {GetSchemaPaymentTableName()} WHERE ""UNIQUEID""=:UniqueId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { UniqueId = uniqueId });
                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"根据UniqueId获取支付信息Payment出错,UniqueId:{uniqueId}.{ex.Message}");
                throw;
            }
        }
    }
}
