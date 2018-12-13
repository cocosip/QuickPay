using DotCommon.Alg;
using DotCommon.Encrypt;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using System;
using System.Security.Cryptography;

namespace QuickPay.WechatPay.Util
{
    /// <summary>微信支付工具类
    /// </summary>
    public class WechatPayUtil
    {
        /// <summary>生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>生成随机串，随机串包含字母或数字
        /// </summary>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }


        /// <summary>Sha1签名
        /// </summary>
        public static string Sha1Sign(PayData payData)
        {
            //转url格式
            string str = $"{payData.ToUrl()}";
            //var encrypted = BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(str))).Replace("-", "");
            var encrypted = Sha1Alg.GetStringSha1(str);
            return encrypted;
        }


        /// <summary>生成MD5签名
        /// </summary>
        public static string Md5Sign(PayData payData, WechatPayApp app)
        {
            //转url格式
            string str = $"{payData.ToUrl()}&key={app.Key}";
            var encrypted = Md5Encryptor.GetMd5(str).ToUpper();
            return encrypted;
        }

        /// <summary>签名验证
        /// </summary>
        public static bool VerifySign(PayData payData, WechatPayApp app)
        {
            if (!payData.IsSet("sign"))
            {
                throw new Exception("WechatPayData签名不存在");
            }
            if (payData.GetValue("sign") == null || payData.GetValue("sign").ToString() == "")
            {
                throw new Exception("WechatPayData签名存在但是为空");
            }
            //返回的签名
            var returnSign = payData.GetValue("sign").ToString();
            var localSign = Md5Sign(payData, app);
            return returnSign == localSign;
        }

        /// <summary>格式化日志
        /// </summary>
        public static string ParseLog(string text)
        {
            return $"【微信支付】:[{text}]";
        }

    }
}
