﻿using QuickPay.Infrastructure.Responses;

namespace QuickPay.Infrastructure.Requests
{

    public interface IPayRequest
    {
        string Provider { get; }
        string UniqueId { get; set; }
        string BusinessCode { get; set; }
        string SignFieldName { get; }
        string SignTypeName { get; }
    }

    public interface IPayRequest<in T> : IPayRequest where T : PayResponse
    {
    }
}
