﻿using Microsoft.Extensions.Logging;
using Npgsql;

namespace QuickPay
{
    /// <summary>基础抽象存储
    /// </summary>
    public abstract class BasePostgreSqlStore
    {
        /// <summary>SqlServer配置信息
        /// </summary>
        protected QuickPayPostgreSqlOption Option { get; set; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BasePostgreSqlStore(ILogger<BasePostgreSqlStore> logger, QuickPayPostgreSqlOption option)
        {
            Logger = logger;
            Option = option;
        }

        /// <summary>获取连接
        /// </summary>
        protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(Option.DbConnectionString);
        }

    }
}
