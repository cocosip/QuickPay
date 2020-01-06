﻿using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace QuickPay
{
    /// <summary>基础抽象存储
    /// </summary>
    public abstract class BaseMySqlStore
    {
        /// <summary>SqlServer配置信息
        /// </summary>
        protected QuickPayMySqlOption Option { get; set; }
        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BaseMySqlStore(ILogger<BaseMySqlStore> logger, QuickPayMySqlOption option)
        {
            Logger = logger;
            Option = option;
        }

        /// <summary>获取连接
        /// </summary>
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(Option.DbConnectionString);
        }

    }
}
