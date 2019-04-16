namespace QuickPay.Infrastructure.Services.DTOs
{
    /// <summary>UniqueIdDto
    /// </summary>
    public abstract class UniqueIdModel
    {
        /// <summary>唯一Id
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>业务代码,默认为Default
        /// </summary>
        public string BusinessCode { get; set; } = QuickPaySettings.DefaultBusinessCode;
    }
}
