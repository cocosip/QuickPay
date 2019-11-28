﻿using Dapper;
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
        private readonly string _tableName;

        /// <summary>Ctor
        /// </summary>
        public PostgreSqlPaymentStore(ILoggerFactory loggerFactory, QuickPayPostgreSqlOption option) : base(loggerFactory, option)
        {
            _tableName = option.PaymentTableName;
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
                        sql = $"INSERT INTO {_tableName} (\"UniqueId\",\"PayPlatId\",\"AppId\",\"OutTradeNo\",\"TradeType\",\"BusinessCode\",\"TransactionId\",\"Amount\",\"PayStatusId\",\"PayObject\",\"Describe\") VALUES (:UniqueId,:PayPlatId,:AppId,:OutTradeNo,:TradeType,:BusinessCode,:TransactionId,:Amount,:PayStatusId,:PayObject,:Describe)";
                    }
                    else
                    {
                        //修改
                        sql = $"UPDATE {_tableName} SET \"UniqueId\"=:UniqueId,\"PayPlatId\"=:PayPlatId,\"AppId\"=:AppId,\"OutTradeNo\"=:AppId,\"TradeType\"=:TradeType,\"BusinessCode\"=:BusinessCode,\"TransactionId\"=:TransactionId,\"Amount\"=:Amount,\"PayStatusId\"=:PayStatusId,\"PayObject\"=:PayObject,\"Describe\"=:Describe";
                    }
                    await connection.ExecuteAsync(sql, payment);

                }
            }
            catch (NpgsqlException ex)
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
                    var sql = $"SELECT TOP 1 * FROM \"{_tableName}\" WHERE \"PayPlatId\"=:PayPlatId AND \"AppId\"=:AppId AND \"OutTradeNo\"=:OutTradeNo";
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
                    var sql = $"SELECT TOP 1 * FROM \"{_tableName}\" WHERE \"PayPlatId\"=:PayPlatId AND \"AppId\"=:AppId AND \"TransactionId\"=:TransactionId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { PayPlatId = payPlatId, AppId = appId, TransactionId = transactionId });
                }
            }
            catch (NpgsqlException ex)
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
                    var sql = $"SELECT TOP 1 * FROM \"{_tableName}\" WHERE \"UniqueId\"=:UniqueId";
                    return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { UniqueId = uniqueId });
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"根据UniqueId获取支付信息Payment出错,UniqueId:{uniqueId}.{ex.Message}");
                throw;
            }
        }
    }
}
