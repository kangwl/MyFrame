using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Demo.Common.Redis;
using Foundatio.Queues;

namespace ConsoleApp.Service.Demo.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            //int index = 1;
            ////while (true)
            ////{
            ////    RedisHelper.Publish("order", new Order() { ID = Guid.NewGuid(), Money = 12+(index++) });

            ////    Thread.Sleep(1000);
            ////}

            ////double a = RedisHelper.StringIncrement("UserSub." + 121);
            ////RedisHelper.SetExpire("UserSub." + 121, DateTime.Now.AddMinutes(2));
            ////Console.WriteLine(a==1d);

            //Foundatio.Queues.IQueue<Order> queue = new InMemoryQueue<Order>();
            //ThreadPool.QueueUserWorkItem(d =>
            //{

            //});
            //TcpClient tcpClient = new TcpClient(AddressFamily.InterNetwork);
            //tcpClient.Connect("server.ngrok.cc", 15550);
            //NetworkStream stream = tcpClient.GetStream();

            string a = "sdd";
      

            Console.Read();
        }

 
    }
}
