using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>转账信息存储
    /// </summary>
    public class OracleTransferStore : BaseOracleStore, ITransferStore
    {
        private string _tableName;

        /// <summary>Ctor
        /// </summary>
        public OracleTransferStore(ILoggerFactory loggerFactory, QuickPayOracleOption option) : base(loggerFactory, option)
        {
            _tableName = option.TransferTableName;
        }

        /// <summary>创建或者修改转账信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Transfer transfer)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //根据UniqueId查询转账信息
                    var queryTransfer = await GetByUniqueIdAsync(transfer.UniqueId);

                    var sql = "";
                    if (queryTransfer == null || queryTransfer.AppId.IsNullOrWhiteSpace())
                    {
                        //创建
                        sql = $"INSERT INTO {_tableName} (UniqueId,PayPlatId,AppId,OutTradeNo,TradeType,BusinessCode,TransferNo,Amount,TransferStatusId,TransferTime,PayObject,Describe) VALUES (:UniqueId,:PayPlatId,:AppId,:OutTradeNo,:TradeType,:BusinessCode,:TransferNo,:Amount,:TransferStatusId,:TransferTime,:PayObject,:Describe)";
                    }
                    else
                    {
                        //修改
                        sql = $"UPDATE {_tableName} SET UniqueId=:UniqueId,PayPlatId=:PayPlatId,AppId=:AppId,OutTradeNo=:AppId,TradeType=:TradeType,BusinessCode=:BusinessCode,TransferNo=:TransferNo,Amount=:Amount,TransferStatusId=:TransferStatusId,TransferTime=:TransferTime,PayObject=:PayObject,Describe=:Describe";
                    }
                    await connection.ExecuteAsync(sql, transfer);

                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"创建或者修改Transfer出错,UniqueId:{transfer.UniqueId} {ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT TOP 1 * FROM [{_tableName}] WHERE PayPlatId=:PayPlatId AND AppId=:AppId AND OutTradeNo=:OutTradeNo";
                    return await connection.QueryFirstOrDefaultAsync<Transfer>(sql, new { PayPlatId = payPlatId, AppId = appId, OutTradeNo = outTradeNo });
                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"获取转账信息Transfer出错,PayPlatId:{payPlatId},AppId:{appId},OutTradeNo:{outTradeNo}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据平台Id,AppId,微信支付宝返回的交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetByTransferNo(int payPlatId, string appId, string transferNo)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT TOP 1 * FROM [{_tableName}] WHERE PayPlatId=:PayPlatId AND AppId=:AppId AND TransferNo=:TransferNo";
                    return await connection.QueryFirstOrDefaultAsync<Transfer>(sql, new { PayPlatId = payPlatId, AppId = appId, TransferNo = transferNo });
                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"获取转账信息Transfer出错,PayPlatId:{payPlatId},AppId:{appId},TransferNo:{transferNo}.{ex.Message}");
                throw;
            }
        }

        /// <summary>根据UniqueId获取转账信息
        /// </summary>
        public async Task<Transfer> GetByUniqueIdAsync(string uniqueId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT TOP 1 * FROM [{_tableName}] WHERE UniqueId=:UniqueId";
                    return await connection.QueryFirstOrDefaultAsync<Transfer>(sql, new { UniqueId = uniqueId });
                }
            }
            catch (OracleException ex)
            {
                Logger.LogError($"根据UniqueId获取转账信息Payment出错,UniqueId:{uniqueId}.{ex.Message}");
                throw;
            }
        }
    }
}
