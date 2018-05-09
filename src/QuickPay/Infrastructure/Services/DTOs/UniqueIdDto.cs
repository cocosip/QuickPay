namespace QuickPay.Infrastructure.Services.DTOs
{
    public abstract class UniqueIdDto
    {
        public string UniqueId { get; set; }

        /// <summary>业务代码,默认为Default
        /// </summary>
        public string BusinessCode { get; set; } = QuickPaySettings.DefaultBusinessCode;
    }
}
