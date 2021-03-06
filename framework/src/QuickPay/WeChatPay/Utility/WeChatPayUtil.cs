﻿using DotCommon.Encrypt;
using DotCommon.Utility;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Apps;
using System;

namespace QuickPay.WeChatPay.Utility
{
    /// <summary>微信支付工具类
    /// </summary>
    public class WeChatPayUtil
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

            var encrypted = SHAUtil.GetHex16StringSHA1Hash(str);
            return encrypted;
        }


        /// <summary>生成MD5签名
        /// </summary>
        public static string Md5Sign(PayData payData, WeChatPayApp app)
        {
            //转url格式
            string str = $"{payData.ToUrl()}&key={app.Key}";
            return MD5Helper.GetMD5(str);
        }

        /// <summary>签名验证
        /// </summary>
        public static bool VerifySign(PayData payData, WeChatPayApp app)
        {
            if (!payData.IsSet("sign"))
            {
                throw new Exception("WeChatPayData签名不存在");
            }
            if (payData.GetValue("sign") == null || payData.GetValue("sign").ToString() == "")
            {
                throw new Exception("WeChatPayData签名存在但是为空");
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
