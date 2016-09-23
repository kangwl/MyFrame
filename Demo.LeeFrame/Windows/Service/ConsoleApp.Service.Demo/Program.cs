using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.Redis;

namespace ConsoleApp.Service.Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            RedisHelper.Subscribe<Order>("order", SuscribebBack);
            Console.Read();
        }

        private static void SuscribebBack(Order order)
        {
            Console.WriteLine(order.ID + " " + order.Money);
            // NLogHelper.Debug("SuscribebBack:" + order.ID.ToString());
        }


  
    }
}
