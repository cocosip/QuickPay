using QuickPay.WechatPay.Requests;
using System;

namespace QuickPay.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var request = new AppUnifiedOrderRequest();

            var t = request.GetType();


            Console.WriteLine(t);
            Console.ReadLine();

        }
    }
}
