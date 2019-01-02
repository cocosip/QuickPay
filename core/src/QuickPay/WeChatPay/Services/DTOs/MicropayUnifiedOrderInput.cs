﻿using DotCommon.AutoMapper;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WeChatPay.Requests;

namespace QuickPay.WeChatPay.Services.DTOs
{
    /// <summary>刷卡支付
    /// </summary>
    [AutoMapTo(typeof(MicropayUnifiedOrderRequest))]
    public class MicropayUnifiedOrderInput : UniqueIdModel
    {
        /// <summary>商品简单描述，该字段请按照规范传递
        /// </summary>
        public string Body { get; set; }

        /// <summary>商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        public string SpbillCreateIp { get; set; }

        /// <summary>扫码支付授权码，设备读取用户微信中的条码或者二维码信息
        /// 注：用户刷卡条形码规则：18位纯数字，以10、11、12、13、14、15开头
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>Ctor
        /// </summary>
        public MicropayUnifiedOrderInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">订单总金额,单位为分</param>
        /// <param name="spbillCreateIp">APP和网页支付提交用户端ip</param>
        /// <param name="authCode">码支付授权码</param>
        public MicropayUnifiedOrderInput(string body, string outTradeNo, int totalFee, string spbillCreateIp, string authCode)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIp = spbillCreateIp;
            AuthCode = authCode;
        }





        /********************非必须参数********************/
        /// <summary>自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来
        /// </summary>
        [PayElement("detail", false)]
        public string Detail { get; set; }

        /// <summary>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        [PayElement("attach", false)]
        public string Attach { get; set; }

        /// <summary>符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        [PayElement("fee_type", false)]
        public string FeeType { get; set; }

        /// <summary>商品标记，使用代金券或立减优惠功能时需要的参数
        /// </summary>
        [PayElement("goods_tag", false)]
        public string GoodsTag { get; set; }

        /// <summary>上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        [PayElement("limit_pay", false)]
        public string LimitPay { get; set; }
    }
}
