using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信付款到帐号
    /// </summary>
    public class TransferToAccountRequest : BaseWeChatPayRequest<TransferToAccountResponse>
    {
        /// <summary>AppId
        /// </summary>
        [PayElement("mch_appid")]
        public new string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mchid")]
        public new string MchId { get; set; }

        /// <summary>商户订单号，需保持唯一性(只能是字母或者数字，不能包含有其他字符)
        /// </summary>
        [PayElement("partner_trade_no")]
        public string TradeNo { get; set; }

        /// <summary>商户appid下,某用户的openid
        /// </summary>
        [PayElement("openid")]
        public string OpenId { get; set; }

        /// <summary>NO_CHECK：不校验真实姓名 FORCE_CHECK：强校验真实姓名
        /// </summary>
        [PayElement("check_name")]
        public string CheckName { get; set; } = WeChatPaySettings.TransferToAccountCheckName.ForceCheck;

        /// <summary>订单总金额，单位为分
        /// </summary>
        [PayElement("amount")]
        public int Amount { get; set; }

        /// <summary>企业付款备注，必填。注意：备注中的敏感词会被转成字符*
        /// </summary>
        [PayElement("desc")]
        public string Desc { get; set; }

        /// <summary>IP地址,该IP同在商户平台设置的IP白名单中的IP没有关联，该IP可传用户端或者服务端的IP。
        /// </summary>
        [PayElement("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }


        /**************非必须**************/

        /// <summary>微信支付分配的终端设备号
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>收款用户真实姓名。 如果check_name设置为FORCE_CHECK，则必填用户真实姓名
        /// </summary>
        [PayElement("re_user_name", false)]
        public string ReUserName { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TransferToAccountRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="tradeNo">商户订单号</param>
        /// <param name="openId">商户appid下,某用户的openid</param>
        /// <param name="amount">金额</param>
        /// <param name="desc">企业付款备注</param>
        public TransferToAccountRequest(string tradeNo, string openId, int amount, string desc)
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
        public TransferToAccountRequest(string tradeNo, string openId, int amount, string desc, string checkName, string reUserName)
        {
            TradeNo = tradeNo;
            OpenId = openId;
            Amount = amount;
            Desc = desc;
            CheckName = checkName;
            ReUserName = reUserName;
        }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            var weChatPayConfig = (WeChatPayConfig)config;
            var weChatPayApp = (WeChatPayApp)app;
            this.AppId = weChatPayApp.AppId;
            this.MchId = weChatPayApp.MchId;
            //设置IP地址
            SpbillCreateIp = weChatPayConfig.LocalAddress;
        }
    }
}
