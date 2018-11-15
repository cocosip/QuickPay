﻿using Dapper;
using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Util;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QuickPay.WechatPay
{
    public class SqlServerAccessTokenStore : BaseSqlServerStore, IAccessTokenStore
    {
        private string TableName = "QP_AccessTokens";
        public SqlServerAccessTokenStore(QuickPaySqlServerOption option, ILogger<QuickPayLoggerName> logger) : base(option, logger)
        {
        }

        /// <summary>根据AppId获取AccessToken
        /// </summary>
        public async Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var sql = $"SELECT TOP 1 * FROM [{TableName}] WHERE AppId=@AppId";
                    return await connection.QueryFirstOrDefaultAsync<AccessToken>(sql, new { AppId = appId });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(WechatPayUtil.ParseLog($"获取应用AccessToken出错,AppId:{appId}.{ex.Message}"));
                throw;
            }
        }

        /// <summary>创建或者修改AccessToken
        /// </summary>
        public async Task CreateOrUpdateAccessTokenAsync(AccessToken accessToken)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    //先查询AccessToken,是否存在
                    var queryAccessToken = await GetAccessTokenAsync(accessToken.AppId);

                    var sql = "";
                    if (queryAccessToken == null || queryAccessToken.AppId.IsNullOrWhiteSpace())
                    {
                        //创建
                        sql = $"INSERT INTO {TableName} (AppId,Token,ExpiredIn,LastModifiedTime) VALUES (@AppId,@Token,@ExpiredIn,@LastModifiedTime)";
                    }
                    else
                    {
                        //修改
                        sql = $"UPDATE {TableName} SET Token=@Token,ExpiredIn=@ExpiredIn,LastModifiedTime=@LastModifiedTime WHERE AppId=@AppId";
                    }
                    await connection.ExecuteAsync(sql, accessToken);

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(WechatPayUtil.ParseLog($"获取应用AccessToken出错,{accessToken.ToString()}.{ex.Message}"));
                throw;
            }
        }


    }
}
