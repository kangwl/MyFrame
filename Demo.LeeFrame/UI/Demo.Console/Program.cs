 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.Common.IOC;
using Demo.Common.Reflection;
using Demo.Common.web; 
using Demo.Model;
using Demo.Repository;
using Demo.Repository.IRepository;
using Demo.Service;
using Demo.Service.Interface;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //IUserServ<User> userServ = new UserServ();
            //Guid uid = Guid.NewGuid();
            ////userServ.Insert(new User()visua
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
            //DateTime.Parse("",DateTimeFormatInfo.GetInstance(CultureInfo.CreateSpecificCulture("")))
            //var infos = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

            //DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local,
            //    TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            //System.Console.WriteLine(dt);

            //var list = new List<string>() {"asd", "12", "saaa"};
            //string first = list.Find(one => one.IndexOf("a", StringComparison.Ordinal) != -1);
            //System.Console.WriteLine(first);

 
            //var config = new LoggingConfiguration();
            //var fileTarget = new FileTarget()
            //{
            //    Name = "TimeBasedArchive",
            //    Layout = "${longdate} ${logger} ${message}",
            //    FileName = "${basedir}/logs/logfile.txt",
            //    ArchiveFileName = "${basedir}/archives/log.{##}.txt",
            //    ArchiveEvery = FileArchivePeriod.Day,
            //    ArchiveNumbering = ArchiveNumberingMode.Rolling,
            //    MaxArchiveFiles = 10,
            //    ConcurrentWrites = true,
            //    KeepFileOpen = false,
            //    Encoding = Encoding.UTF8
            //};
            //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            //LogManager.Configuration = config;

            IOCHepler.Regist<UserRepository,IUserRepository>();
            IOCHepler.Build();

            IUserRepository userRepository = IOCHepler.Resolve<IUserRepository>();
            //IUserRepository userRepository = new UserRepository();
            // var user = new User() {ID = Guid.Parse("F1A311A0-9685-4051-8EC6-7302FEF7684F"), Name = "wqqq", Age = 12, QQ = "1234556", CreateTime = DateTime.Now };
            //userRepository.Insert(user);
            ////bool ok =
            ////    userRepository.Insert(new User() {Name = "kwl", Age = 12, QQ = "1234556", CreateTime = DateTime.Now});

            //List<User> users = userRepository.GetPaged(user, 1, 10, "Name=@Name", "Age ASC");
            //users.ForEach(one=>System.Console.WriteLine(one.Name));
            // System.Console.WriteLine(ok); 
            //userRepository.Insert(user);
            //User getUser = userRepository.GetOne(user.ID, new User() {ID = user.ID});
            //userRepository.Delete(user);
            //List<WhereItem> whereItems = new List<WhereItem>()
            //{
            //    new WhereItem() {Field = nameof(user.Name), Signal = "="}
            //};
            //OrderByItem orderByItem = new OrderByItem() {OrderByFiled = nameof(user.CreateTime), Asc = false};
            //List<User> users = userRepository.Paged(1, 10, whereItems, orderByItem, user);

            //List<User> userlList = new List<User>();
            //for (int i = 0; i < 3; i++)
            //{
            //    userlList.Add(new User() {ID = Guid.NewGuid(), Age = 13, Name = "kwl" + i, CreateTime = DateTime.Now});
            //}
            //userRepository.InsertBath(userlList);
            User user = new User() {Name = "%kwl%"};
            bool exist =
                userRepository.Exist(new List<WhereItem>() {new WhereItem() {Field = nameof(user.Name), Signal = "like"}},
                    user);
            int count = userRepository.GetRecordCount(new List<WhereItem>() { new WhereItem() { Field = nameof(user.Name), Signal = "like" } },
                    user);
            System.Console.WriteLine(count);
            System.Console.Read();
        }
    }

}
