 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataCenter.Dapper;
using Demo.Model;
using Demo.Service;
using Demo.Service.Interface;

namespace Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //IUserServ<User> userServ = new UserServ();
            //Guid uid = Guid.NewGuid();
            ////userServ.Insert(new User()
            ////{
            ////    ID = uid,
            ////    Age = 12,
            ////    Name = "kwl",
            ////    QQ = "767474055",
            ////    Phone = "15869165949",
            ////    CreateTime = DateTime.Now,
            ////    UpdateTime = DateTime.Now
            ////});
            ////User theUser = userServ.GetOne(uid);
            //uid = Guid.Parse("ff2e2a7d-e94a-44a6-ae99-82605b4393cf");
            //User user = userServ.GetOne(uid);
            //List<User> users = userServ.GetPaged(one => one.QQ == "767474055", one => one.QQ, 1, 10, false);
            //users.ForEach(one=>System.Console.WriteLine(one.QQ));
            ////bool success = userServ.Delete(user);
            //user.Name = "eee";
            //bool success = userServ.Update(user);
            //System.Console.Write(success);

            System.Console.Read();
        }
    }
}
