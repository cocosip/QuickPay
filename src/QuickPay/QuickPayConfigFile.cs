namespace QuickPay
{
    /// <summary>配置文件
    /// </summary>
    public class QuickPayConfigFile
    {
        public string FileName { get; set; }

        public string Format { get; set; }

        /// <summary>是否从配置文件中读取
        /// </summary>
        public bool IsFromFile { get; set; }
    }
}
