using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>退款存储
    /// </summary>
    public class SqlServerRefundStore : BaseSqlServerStore, IRefundStore
    {

        /// <summary>Ctor
        /// </summary>
        public SqlServerRefundStore(ILogger<BaseSqlServerStore> logger, QuickPaySqlServerOption option) : base(logger, option)
        {

        }

        /// <summary>创建或者修改退款信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Refund refund)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //根据UniqueId查询退款信息
                    var queryRefund = await GetByUniqueIdAsync(refund.UniqueId);

                    var sql = "";
                    if (queryRefund == null || queryRefund.AppId.IsNullOrWhiteSpace())
                    {
                        //创建
                        sql = $@"INSERT INTO {GetSchemaRefundTableName()} ([UniqueId],[PayPlatId],[AppId],[OutTradeNo],[TransactionId],[OutRefundNo],[RefundAmount],[RefundId],[PayObject],[Describe]) VALUES (@UniqueId,@PayPlatId,@AppId,@OutTradeNo,@TransactionId,@OutRefundNo,@RefundAmount,@RefundId,@PayObject,@Describe)";
                    }
                    else
                    {
                        //修改
                        sql = $@"UPDATE {GetSchemaRefundTableName()} SET [UniqueId]=@UniqueId,[PayPlatId]=@PayPlatId,[AppId]=@AppId,[OutTradeNo]=@OutTradeNo,[TransactionId]=@TransactionId,[OutRefundNo]=@OutRefundNo,[RefundAmount]=@RefundAmount,[RefundId]=@RefundId,[PayObject]=@PayObject,[Describe]=@Describe";
                    }
                    await connection.ExecuteAsync(sql, refund);

                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"创建或者修改Refund出错,UniqueId:{refund.UniqueId} {ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,退款交易号,获取退款信息
        /// </summary>
        public async Task<Refund> GetAsync(int payPlatId, string appId, string outRefundNo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaRefundTableName()} WHERE [PayPlatId]=@PayPlatId AND [AppId]=@AppId AND [OutRefundNo]=@outRefundNo";
                    return await connection.QueryFirstOrDefaultAsync<Refund>(sql, new { PayPlatId = payPlatId, AppId = appId, OutRefundNo = outRefundNo });
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"获取退款信息Refund出错,PayPlatId:{payPlatId},AppId:{appId},OutRefundNo:{outRefundNo}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        public async Task<Refund> GetByTransactionId(int payPlatId, string appId, string transactionId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaRefundTableName()} WHERE [PayPlatId]=@PayPlatId AND [AppId]=@AppId AND [TransactionId]=@TransactionId";
                    return await connection.QueryFirstOrDefaultAsync<Refund>(sql, new { PayPlatId = payPlatId, AppId = appId, TransactionId = transactionId });
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"获取退款信息Refund出错,PayPlatId:{payPlatId},AppId:{appId},TransactionId:{transactionId}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据UniqueId获取退款信息
        /// </summary>
        public async Task<Refund> GetByUniqueIdAsync(string uniqueId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT TOP 1 * FROM {GetSchemaRefundTableName()} WHERE [UniqueId]=@UniqueId";
                    return await connection.QueryFirstOrDefaultAsync<Refund>(sql, new { UniqueId = uniqueId });
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"根据UniqueId获取退款信息Refund出错,UniqueId:{uniqueId}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据交易号获取全部的退款订单
        /// </summary>
        public async Task<List<Refund>> GetRefundsAsync(int payPlatId, string appId, string outTradeNo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $@"SELECT * FROM {GetSchemaRefundTableName()} WHERE [PayPlatId]=@PayPlatId AND [AppId]=@AppId AND [OutTradeNo]=@OutTradeNo";
                    return (await connection.QueryAsync<Refund>(sql, new { PayPlatId = payPlatId, AppId = appId, OutTradeNo = outTradeNo })).ToList();
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError($"根据交易号获取全部的退款订单出错,PayPlatId:{payPlatId},AppId:{appId},OutTradeNo:{outTradeNo}.{ex.Message}");
                throw;
            }
        }
    }
}
