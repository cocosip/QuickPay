using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WeChatPay.Requests;

namespace QuickPay.WeChatPay.Services.DTOs
{
    /// <summary>企业付款到帐号
    /// </summary>
    [AutoMapTo(typeof(TransferToAccountRequest))]
    public class TransferToAccountInput : UniqueIdModel
    {

        /// <summary>商户订单号，需保持唯一性(只能是字母或者数字，不能包含有其他字符)
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>商户appid下,某用户的openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>NO_CHECK：不校验真实姓名 FORCE_CHECK：强校验真实姓名
        /// </summary>
        public string CheckName { get; set; } = WeChatPaySettings.TransferToAccountCheckName.ForceCheck;

        /// <summary>订单总金额，单位为分
        /// </summary>
        public int Amount { get; set; }

        /// <summary>企业付款备注，必填。注意：备注中的敏感词会被转成字符*
        /// </summary>
        public string Desc { get; set; }

        /****************非必须参数***************/


        /// <summary>微信支付分配的终端设备号
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>收款用户真实姓名。 如果check_name设置为FORCE_CHECK，则必填用户真实姓名
        /// </summary>
        public string ReUserName { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TransferToAccountInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="tradeNo">商户订单号</param>
        /// <param name="openId">商户appid下,某用户的openid</param>
        /// <param name="amount">金额</param>
        /// <param name="desc">企业付款备注</param>
        public TransferToAccountInput(string tradeNo, string openId, int amount, string desc)
        {
            TradeNo = tradeNo;
            OpenId = openId;
            Amount = amount;
            Desc = desc;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="tradeNo">商户订单号</param>
        /// <param name="openId">商户appid下,某用户的openid</param>
        /// <param name="amount">金额</param>
        /// <param name="desc">企业付款备注</param>
        /// <param name="checkName">校验用户姓名选项</param>
        /// <param name="reUserName">收款用户姓名</param>
        public TransferToAccountInput(string tradeNo, string openId, int amount, string desc, string checkName, string reUserName)
        {
            TradeNo = tradeNo;
            OpenId = openId;
            Amount = amount;
            Desc = desc;
            CheckName = checkName;
            ReUserName = reUserName;
        }


    }
}
