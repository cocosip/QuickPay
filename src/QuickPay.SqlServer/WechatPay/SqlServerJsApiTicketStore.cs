using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Util;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QuickPay.WechatPay
{
    public class SqlServerJsApiTicketStore : BaseSqlServerStore, IJsApiTicketStore
    {
        private string TableName = "QP_JsApiTickets";

        public SqlServerJsApiTicketStore(QuickPaySqlServerOption option, ILogger<QuickPayLoggerName> logger) : base(option, logger)
        {
        }

        /// <summary>获取JsApiTicket
        /// </summary>
        public async Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT TOP 1 * FROM [{TableName}] WHERE AppId=@AppId";
                    return await connection.QueryFirstOrDefaultAsync<JsApiTicket>(sql, new { AppId = appId });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(WechatPayUtil.ParseLog($"获取应用JsApiTicket出错,AppId:{appId}.{ex.Message}"));
                throw;
            }
        }

        /// <summary>创建或者修改JsApiTicket
        /// </summary>
        public async Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //先查询JsApiTicket,是否存在
                    var queryJsApiTicket = await GetJsApiTicketAsync(jsApiTicket.AppId);

                    var sql = "";
                    if (queryJsApiTicket == null || queryJsApiTicket.AppId.IsNullOrWhiteSpace())
                    {
                        //创建
                        sql = $"INSERT INTO {TableName} (AppId,Ticket,ExpiredIn,LastModifiedTime) VALUES (@AppId,@Ticket,@ExpiredIn,@LastModifiedTime)";
                    }
                    else
                    {
                        //修改
                        sql = $"UPDATE {TableName} SET Ticket=@Ticket,ExpiredIn=@ExpiredIn,LastModifiedTime=@LastModifiedTime WHERE AppId=@AppId";
                    }
                    await connection.ExecuteAsync(sql, jsApiTicket);

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(WechatPayUtil.ParseLog($"创建或者修改JsApiTicket出错,{jsApiTicket.ToString()}.{ex.Message}"));
                throw;
            }
        }


    }
}
