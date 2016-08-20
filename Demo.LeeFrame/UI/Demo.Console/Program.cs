 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Service;
using Demo.Service.Interface;

namespace Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserServ userServ = new UserServ();
            userServ.GetOne(Guid.Empty);
            System.Console.Write("ok");
            System.Console.Read();
        }
    }
}
