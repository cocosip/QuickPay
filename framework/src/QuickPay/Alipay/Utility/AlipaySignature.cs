using DotCommon.Encrypt;
using System.Collections.Generic;
using System.Text;
using DotCommon.Extensions;
using System.Net;

namespace QuickPay.Alipay.Utility
{
    /// <summary>支付宝签名工具类
    /// </summary>
    public static class AlipaySignature
    {
        private static readonly Dictionary<string, string> _signTypeDict = new Dictionary<string, string>()
        {
            {"RSA","SHA1" },
            {"RSA2","SHA256" }
        };

        /// <summary>获取签名的内容
        /// </summary>
        public static string GetSignContent(IDictionary<string, object> parameters)
        {
            // 第一步：把字典按Key的字母顺序排序
            var sortedParams = new SortedDictionary<string, object>(parameters);
            IEnumerator<KeyValuePair<string, object>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value.ToString();
                if (!key.IsNullOrWhiteSpace() && !value.IsNullOrWhiteSpace())
                {
                    //Url编码
                    //query.Append(key).Append("=").Append(value).Append("&");
                    query.Append(WebUtility.UrlEncode(key)).Append("=").Append(WebUtility.UrlEncode(value)).Append("&");
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);
            return content;
        }

        /// <summary>RSA字符串签名
        /// </summary>
        public static string RSASign(string data, string privateKeyPem, string charset, string signType)
        {
            return RSASignCharSet(data, privateKeyPem, charset, signType);
        }

        /// <summary>RSA字符串签名
        /// </summary>
        public static string RSASign(IDictionary<string, object> parameters, string privateKeyPem, string charset, string signType)
        {
            string signContent = GetSignContent(parameters);
            return RSASignCharSet(signContent, privateKeyPem, charset, signType);
        }

        /// <summary>签名验证版本V1
        /// </summary>
        public static bool RSACheckV1(IDictionary<string, object> parameters, string publicKeyPem, string charset, string signType)
        {
            string sign = parameters["sign"].ToString();

            parameters.Remove("sign");
            parameters.Remove("sign_type");
            string signContent = GetSignContent(parameters);
            return RSACheckContent(signContent, sign, publicKeyPem, charset, signType);
        }

        /// <summary>签名验证V2
        /// </summary>
        public static bool RSACheckV2(IDictionary<string, object> parameters, string publicKeyPem, string charset, string signType)
        {
            string sign = parameters["sign"].ToString();
            parameters.Remove("sign");
            string signContent = GetSignContent(parameters);
            return RSACheckContent(signContent, sign, publicKeyPem, charset, signType);
        }


        /// <summary>RSA签名
        /// </summary>
        public static string RSASignCharSet(string data, string privateKeyPem, string charset, string signType)
        {
            var hashAlgorithmName = GetHashAlgorithmName(signType);
            return RSAHelper.SignDataAsBase64(data, privateKeyPem, hashAlgorithmName: hashAlgorithmName, encode: charset);
        }



        /// <summary>RSA签名验证
        /// </summary>
        public static bool RSACheckContent(string signContent, string sign, string publicKeyPem, string charset, string signType)
        {

            try
            {
                var hashAlgorithmName = GetHashAlgorithmName(signType);
                return RSAHelper.VerifyBase64Data(signContent, sign, publicKeyPem, hashAlgorithmName: hashAlgorithmName, encode: charset);
            }
            catch
            {
                return false;
            }

        }

        /// <summary>根据SignType获取HashAlgorithmName
        /// </summary>
        public static string GetHashAlgorithmName(string signType)
        {
            return _signTypeDict[signType];
        }

    }
}
