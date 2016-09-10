 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.Reflection;
using Demo.Common.web;
using Demo.DataCenter.Dapper;
using Demo.Model;
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

            NLogHelper.Error("some wrong");
            System.Console.Read();
        }
    }

}
