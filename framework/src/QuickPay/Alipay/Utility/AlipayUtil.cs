using DotCommon.Encrypt;
using DotCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuickPay.Alipay.Utility
{
    /// <summary>支付宝工具类
    /// </summary>
    public class AlipayUtil
    {
        /// <summary>生成时间戳，发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public static string GenerateTimeStamp()
        {
            return DateTime.Now.ToString($"yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>组装普通文本请求参数
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <param name="charset">编码</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, object> parameters, string charset)
        {
            StringBuilder postData = new StringBuilder();
            foreach (var kv in parameters)
            {
                if (!kv.Key.IsNullOrWhiteSpace() && !kv.Value.ToString().IsNullOrWhiteSpace())
                {
                    postData.Append($"&{kv.Key}={WebUtility.UrlEncode(kv.Value.ToString())}");
                }
            }
            return postData.ToString();
        }

        #region Aes加密解密

        /// <summary>加密
        /// </summary>
        public static string AesEncrypt(string encryptKey, string text, string charset)
        {
            var keyBytes = Convert.FromBase64String(encryptKey);
            var aesEncrypter = new AesEncryptor(keyBytes, InitIv(16));
            return aesEncrypter.Encrypt(text, charset);
        }

        /// <summary>解密
        /// </summary>
        public static string AesDecrypt(string encryptKey, string encryptedText, string charset)
        {
            var keyBytes = Convert.FromBase64String(encryptKey);
            var aesEncrypter = new AesEncryptor(keyBytes, InitIv(16));
            return aesEncrypter.Decrypt(encryptedText, charset);
        }

        /// <summary>初始化AES加密解密向量
        /// </summary>
        private static byte[] InitIv(int blockSize)
        {
            byte[] iv = new byte[blockSize];
            for (int i = 0; i < blockSize; i++)
            {
                iv[i] = (byte)0x0;
            }
            return iv;
        }
        #endregion

        /// <summary>格式化日志
        /// </summary>
        public static string ParseLog(string text)
        {
            return $"【支付宝】:[{text}]";
        }

    }
}
