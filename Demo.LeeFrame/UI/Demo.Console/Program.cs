 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.Common.IOC;
using Demo.Common.Redis;
using Demo.Common.Redis.Queue;
using Demo.Common.Reflection;
using Demo.Common.web; 
using Demo.Model;
using Demo.Repository;
using Demo.Repository.IRepository;
using Demo.Service;
using Demo.Service.Interface;
using Foundatio.Caching;
using Foundatio.Jobs;
using Foundatio.Logging;
using Foundatio.Logging.InMemory;
using Foundatio.Messaging;
using Foundatio.Metrics;
using Foundatio.Queues;
using Foundatio.Serializer;
using Foundatio.Storage;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using ILogger = Foundatio.Logging.ILogger;
using Log = NLog.Fluent.Log;
using LogLevel = Foundatio.Logging.LogLevel;

namespace Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region zs
            // //IUserServ<User> userServ = new UserServ();
            // //Guid uid = Guid.NewGuid();
            // ////userServ.Insert(new User()visua
            // ////{
            // ////    ID = uid,
            // ////    Age = 12,
            // ////    Name = "kwl",
            // ////    QQ = "767474055",
            // ////    Phone = "15869165949",
            // ////    CreateTime = DateTime.Now,
            // ////    UpdateTime = DateTime.Now
            // ////});
            // ////User theUser = userServ.GetOne(uid);
            // //uid = Guid.Parse("ff2e2a7d-e94a-44a6-ae99-82605b4393cf");
            // //User user = userServ.GetOne(uid);
            // //List<User> users = userServ.GetPaged(one => one.QQ == "767474055", one => one.QQ, 1, 10, false);
            // //users.ForEach(one=>System.Console.WriteLine(one.QQ));
            // ////bool success = userServ.Delete(user);
            // //user.Name = "eee";
            // //bool success = userServ.Update(user);
            // //System.Console.Write(success);
            // //DateTime.Parse("",DateTimeFormatInfo.GetInstance(CultureInfo.CreateSpecificCulture("")))
            // //var infos = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

            // //DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local,
            // //    TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            // //System.Console.WriteLine(dt);

            // //var list = new List<string>() {"asd", "12", "saaa"};
            // //string first = list.Find(one => one.IndexOf("a", StringComparison.Ordinal) != -1);
            // //System.Console.WriteLine(first);


            // //var config = new LoggingConfiguration();
            // //var fileTarget = new FileTarget()
            // //{
            // //    Name = "TimeBasedArchive",
            // //    Layout = "${longdate} ${logger} ${message}",
            // //    FileName = "${basedir}/logs/logfile.txt",
            // //    ArchiveFileName = "${basedir}/archives/log.{##}.txt",
            // //    ArchiveEvery = FileArchivePeriod.Day,
            // //    ArchiveNumbering = ArchiveNumberingMode.Rolling,
            // //    MaxArchiveFiles = 10,
            // //    ConcurrentWrites = true,
            // //    KeepFileOpen = false,
            // //    Encoding = Encoding.UTF8
            // //};
            // //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            // //LogManager.Configuration = config;

            //// IOCHepler.Regist<UserRepository,IUserRepository>();
            // var types =
            //     Assembly.Load("Demo.Repository").GetTypes().Where(one => one.Name.EndsWith("Repository")).ToArray();
            // IOCHepler.Regist(types);
            // IOCHepler.Build();


            // //IUserRepository userRepository = new UserRepository();
            // // var user = new User() {ID = Guid.Parse("F1A311A0-9685-4051-8EC6-7302FEF7684F"), Name = "wqqq", Age = 12, QQ = "1234556", CreateTime = DateTime.Now };
            // //userRepository.Insert(user);
            // ////bool ok =
            // ////    userRepository.Insert(new User() {Name = "kwl", Age = 12, QQ = "1234556", CreateTime = DateTime.Now});

            // //List<User> users = userRepository.GetPaged(user, 1, 10, "Name=@Name", "Age ASC");
            // //users.ForEach(one=>System.Console.WriteLine(one.Name));
            // // System.Console.WriteLine(ok); 
            // //userRepository.Insert(user);
            // //User getUser = userRepository.GetOne(user.ID, new User() {ID = user.ID});
            // //userRepository.Delete(user);
            // //List<WhereItem> whereItems = new List<WhereItem>()
            // //{
            // //    new WhereItem() {Field = nameof(user.Name), Signal = "="}
            // //};
            // //OrderByItem orderByItem = new OrderByItem() {OrderByFiled = nameof(user.CreateTime), Asc = false};
            // //List<User> users = userRepository.Paged(1, 10, whereItems, orderByItem, user);

            // //List<User> userlList = new List<User>();
            // //for (int i = 0; i < 3; i++)
            // //{
            // //    userlList.Add(new User() {ID = Guid.NewGuid(), Age = 13, Name = "kwl" + i, CreateTime = DateTime.Now});
            // //}
            // //userRepository.InsertBath(userlList);
            // //User user = new User() {Name = "%kwl%"};
            // //bool exist =
            // //    userRepository.Exist(new List<WhereItem> {new WhereItem(nameof(user.Name), "like")}, user);
            // //int count = userRepository.GetRecordCount(new List<WhereItem> {new WhereItem(nameof(user.Name), "like")},
            // //    user);
            // //System.Console.WriteLine(count);
            // IUserRepository userRepository = IOCHepler.Resolve<IUserRepository>();
            // ICommissionDetailRepository commissionDetailRepository = IOCHepler.Resolve<ICommissionDetailRepository>();
            // //CommissionDetail detail = new CommissionDetail()
            // //{
            // //    ID = Guid.NewGuid(),
            // //    Amt = 15.3455m,
            // //    UserID = Guid.Parse("F1A311A0-9685-4051-8EC6-7302FEF7684F"),
            // //    CreateTime = DateTime.Now
            // //};
            // //bool ok = commissionDetailRepository.Insert(detail);
            // //System.Console.WriteLine(ok);

            // //CommissionDetail detail =
            // //    commissionDetailRepository.GetOne(Guid.Parse("4D2EC216-A793-47BC-8C21-8997188C1583"));
            // //System.Console.WriteLine(detail.Amt);

            // List<CommissionDetail> commissionDetails =
            //     commissionDetailRepository.GetPaged(Guid.Parse("F1A311A0-9685-4051-8EC6-7302FEF7684F"), 1, 10);
            // commissionDetails.ForEach(one => System.Console.WriteLine(one.Amt));
            // //
            // User user = userRepository.GetOne(Guid.Parse("F1A311A0-9685-4051-8EC6-7302FEF7684F"));
            // System.Console.WriteLine(user.Name);

            // TestCache();
            // TestLogger();
            //TestMessageBus();
            //TestJob();
            //TcpClient tcpClient = new TcpClient(AddressFamily.InterNetwork);
            //tcpClient.Connect("server.ngrok.cc", 15550);
            //NetworkStream stream = tcpClient.GetStream(); 
            #endregion
            Enqueue();
            Dequeue();
            System.Console.Read();
        }

        private static string queueKey = "kkwwll";
        private static void Enqueue()
        {
            RedisQueueFactory redisQueueFactory = new RedisQueueFactory(queueKey);
            redisQueueFactory.Enqueue("kwl");
            redisQueueFactory.Enqueue("kwl123");
            redisQueueFactory.Enqueue("kangwl");
            redisQueueFactory.Enqueue("kangwenli");
        }

        private static void Dequeue()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            RedisQueueFactory redisQueueFactory = new RedisQueueFactory(queueKey);
            Task.Factory.StartNew(() =>
            {
                redisQueueFactory.Dequeue<string>(System.Console.WriteLine);
                cancellationTokenSource.Cancel();
            }, cancellationTokenSource.Token);
        }

        private static async void TestCache()
        {
            Foundatio.Caching.ICacheClient cacheClient = new RedisCacheClient(RedisHelper.connection);
            var addOK = await cacheClient.AddAsync("kkk", "kangwl", TimeSpan.FromMinutes(20));
            if (addOK)
            {
                var kkk = await cacheClient.GetAsync<string>("kkk");
                System.Console.WriteLine(kkk);
            }

            System.Console.Read();
        }

        private static async void TestLogger()
        {
            InMemoryLoggerFactory loggerFactory = new InMemoryLoggerFactory();
            ILogger logger = loggerFactory.CreateLogger<Program>();


            loggerFactory.LogEntries.ToList().ForEach(one =>
            {
                switch (one.LogLevel)
                {
                    case LogLevel.Information:
                        Common.NLogHelper.Debug(one.Message);
                        break;
                    case LogLevel.Error:
                        Common.NLogHelper.Error(one.Message);
                        break;

                }
            });
        }

        internal class Custom
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime AddDateTime { get; set; }
        }
        private static async void TestMessageBus()
        {
            //Foundatio.Messaging.IMessageBus messageBus = new InMemoryMessageBus();
            //messageBus.Subscribe<string>(one =>
            //{
            //    System.Console.WriteLine(one);
            //});

            //await messageBus.PublishAsync("hello moto");

            Foundatio.Serializer.ISerializer serializer =
                new JsonNetSerializer(new JsonSerializerSettings() {DateFormatString = "yyyy/MM/dd HH:mm:ss"});
            Custom custom = new Custom() {ID = 1, Name = "sdd", AddDateTime = DateTime.Now};
            byte[] bytes = await serializer.SerializeAsync(custom);
            string str = Encoding.Default.GetString(bytes);

            System.Console.WriteLine(str);

             
        }

        private static async void TestJob()
        {
            IQueue<Custom> queue = new InMemoryQueue<Custom>();
            await queue.EnqueueAsync(new Custom() {ID = 1, Name = "kwl1", AddDateTime = DateTime.Now});
            await queue.EnqueueAsync(new Custom() {ID = 2, Name = "kwl2", AddDateTime = DateTime.Now});
            await queue.EnqueueAsync(new Custom() {ID = 3, Name = "kwl3", AddDateTime = DateTime.Now});
            var job = new HelloWorldJob(queue);
            //var ret = await job.RunAsync();
            await job.RunContinuousAsync(iterationLimit: 3);

            System.Console.WriteLine(job.RunCount);
        }

        public class HelloWorldJob : QueueJobBase<Custom>
        {
            public int RunCount { get; set; }

            public HelloWorldJob(IQueue<Custom> queue, ILoggerFactory loggerFactory = null) : base(queue, loggerFactory)
            {
            }
 
            protected override Task<JobResult> ProcessQueueEntryAsync(QueueEntryContext<Custom> context)
            {
                RunCount++;
                var queueEntry = context.QueueEntry;
                Custom custom = queueEntry.Value;
                System.Console.WriteLine(custom.Name);
                return Task.FromResult(JobResult.Success);
            }
        }
    }

}
