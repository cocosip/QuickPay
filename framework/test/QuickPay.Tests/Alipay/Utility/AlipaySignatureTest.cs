using QuickPay.Alipay.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QuickPay.Tests.Alipay.Utility
{
    public class AlipaySignatureTest
    {
        [Fact]
        public void RSASign_Test()
        {
            var privateKey = @"MIIEowIBAAKCAQEAjOeAPWmVTuGFg/QPgrB1XzmqbfiRRcvR+WtFaP2Ul3ndlp7x751xTX40YWH+TnDxBXfF+uo0lPyIQ6toAcMpp/G2ctDupyycEzujYAZZbRDpwlE41psLcL+Vi+Ng1ORD22RSPpRz3Du4L3zX9ntee0NYyYYX4zJ7e3fZT4YBARBHT7TmP6wr8PgV+pb4ihfoZ27LrXoa1z9nMLFvJ/I7cNkgRHYSeTMMhTP3h1sh/Pso/MTY63oOjufOZcNyS4jYDbY+Uv48nNkRsK/FtKnjqZyPKmq3xWtKNy5jOhJjhh9b2hb48BLRjbX7V2IQKa9rjCXj99GSlfNdjhVWeWUclwIDAQABAoIBAFaQt0mDf1ZJ2SQbIhhRXpqVK+6KAn4V3TdVvvvkppB1LzylA9AJMx2/xmB5uqnoWzrXvcsMbieGChVAzhIfG41xQ3zAfY45Kt3qCtIotHH8LRDTo469DEdFfJPHqqrAXiwAM0L9Iz0Pd3W9RlTIsGAcHQUaG7zaO+C73ccsdZt3vSpxVWIbuaX3G6xD6ZXnoOyxtYfs3qpop+wjc9RoIzN+Pqd29jaLYcsN7D72q81gu8k6wpqxMC24O/wy/AhI4fSaYif7XQBAjjOUe93jrxBHcV1Wy0KOSaSHwoQ1nLL5KBVp6RqYb3TGaAKQE3rgpIrJg/x1X3p93nxmt6JnPnkCgYEA6rRB+NIKtOb9S7udL9zXrCV7MnQk7adRkVNAG8OszfADzH5U1HQ11GpnuS/JZu2DeL2lwmzfsk4ef1AcpjHb/iorr9UeGabM1Zp97Ebte9d+LeNdmuNS9DM0l8/R2SGKQ+EFaIGwg5pVuF8NlB8S0CEnabf/IRlj18q27/AKIfUCgYEAmbByZpzVUU+5Z5cB76DTXXbkaEmJhrCUJqWtzhsRy5tc/BimIW4JEzYvM1fWOqGEqhxz/Wo57SttMxsTReCI3lsFtCLhTnxECAZiRuBR4XJpcnVyKonSHWWfUtNiUN95eSkhV4jrcA4BSMTXPzTftxYJFZ5KMaaxte5eMMO30NsCgYB2IYhbDo0pBGJVLfct0gATu0HI4UB9BYw+kyJfVxuxA69FzAgybtNxOKVARlceoUldCkdWFqp4+mzLM61X0RyjTuJyO9hMnPHYSUw8Em8RuCLgQeIpRWXJV8SO7KD4orMO+0FXmn8XniSrCdyxwvobG7TUtzGInVjtkjCFj9HpyQKBgGAE5iSH3Zp0dcBrjvEYiJV/P0qMjxiQX68ZmdIIBYEwqtJxz/FY3uCa3Lh2K0jsOodRSYJNCK3NkOb6BnuEwd4x/glCNYOkjZh57JKdeWqh4ZF6IP7EpnppUDYeDPG7/Reeg889ouKaTWEaYeSCczbe1IQmJfKJU8P3je9niAM7AoGBANhr1tkbmmMN/LzAOYOYxWZeSBHUDvfBIhLGIPtA83uYuHieRIzfcLfUochgJfTbXHfXDgkIrH2Zlfun6XTMobWUoDTK4buaoB9xzXf2XB6u1w9BbuDM/Z4Nkz4vJHwmgXLJ1WnJxz94cocJytatXWiRvErJVNgof3ykb5xkzGov";
            var signContent = @"app_id=2017061307479603&biz_content={""body"":""test1"",""out_trade_no"":""5e70c0712e467b15785bdf02"",""product_code"":""FAST_INSTANT_TRADE_PAY"",""subject"":""test"",""total_amount"":""0.1""}&charset=utf-8&format=JSON&method=alipay.trade.page.pay&notify_url=http://127.0.0.1/Notify/Alipay&return_url=http://127.0.0.1/Alipay/ReturnUrl&sign_type=RSA2&timestamp=2020-03-17 20:20:01&version=1.0";

            //签名
            var sign1 = AlipaySignature.RSASign(signContent, privateKey, "utf-8", "RSA2");

            var expected1 = @"PI0rQOey52LKXz/1v54KY78h/ogxknC/oZj5rx1do6tb1Zl27M+RoJStl2iwS+ZGIuSIhcoBNlEXrfvZomy6G21T1nO1uRJxDlmIZHII6/N6mQ6Ou4cgQkwcaYvKifOjDCZqZc7+qOPo5VjTLRqo8Mb943t5TCvFB/uLyT+ix96DhoMhGFps6/xiWsd0Zmwn5HB5IHS3/AId5LRHtIhMqYD7S11eXTKq2VSmba1yNFkcAiMO7nx2/Advjlleb2Z1N7hzxOu9xU8A7yYIOCQ/aV7Mfp7OVo78BIAaSBDUtbO7FpqcCnC04wu89rBKIKOPBYzqKH8ESyb1/ZHKDuZmrw==";

            Assert.Equal(expected1, sign1);
        }
    }
}
