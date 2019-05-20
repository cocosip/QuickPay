using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>转账到银行卡
    /// </summary>
    public class TransferToBankCardRequest : BaseWeChatPayRequest<TransferToBankCardResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.TransferToBankCard;

        /// <summary>商户订单号，需保持唯一（只允许数字[0~9]或字母[A~Z]和[a~z]，最短8位，最长32位）
        /// </summary>
        [PayElement("partner_trade_no")]
        public string PartnerTradeNo { get; set; }

        /// <summary>收款方银行卡号（采用标准RSA算法，公钥由微信侧提供）
        /// </summary>
        [PayElement("enc_bank_no")]
        public string EncBankNo { get; set; }

        /// <summary>收款方用户名（采用标准RSA算法，公钥由微信侧提供）
        /// </summary>
        [PayElement("enc_true_name")]
        public string EncTrueName { get; set; }

        /// <summary>银行卡所在开户行编号
        /// </summary>
        [PayElement("bank_code")]
        public string BankCode { get; set; }

        /// <summary>付款金额,RMB分
        /// </summary>
        [PayElement("amount")]
        public int Amount { get; set; }

        /// <summary>企业付款到银行卡付款说明,即订单备注
        /// </summary>
        [PayElement("desc", false)]
        public string Description { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TransferToBankCardRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public TransferToBankCardRequest(string partnerTradeNo, string encBankNo, string encTrueName, string bankCode, int amount)
        {
            PartnerTradeNo = partnerTradeNo;
            EncBankNo = encBankNo;
            EncTrueName = encTrueName;
            BankCode = bankCode;
            Amount = amount;
        }

        /// <summary>
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
        }
    }
}
