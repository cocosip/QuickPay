using DotCommon.AutoMapper;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信企业付款到账户
    /// </summary>
    public class WeChatTransferService : BaseWeChatPayService, IWeChatTransferService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatTransferService(IServiceProvider provider) : base(provider)
        {
        }


        /// <summary>企业付款到帐号
        /// </summary>
        public async Task<TransferToAccountResponse> TransferToAccount(TransferToAccountInput input)
        {
            var request = ObjectMapper.Map<TransferToAccountRequest>(input);
            var response = await Executer.ExecuteAsync<TransferToAccountResponse>(request,Config, App);
            if (response.ReturnSuccess)
            {
                if (response.ResultSuccess)
                {
                    //需要把付款到帐号的记录保存到数据库
                }
            }
            throw new Exception(response.ErrCodeDes);
        }
    }
}
