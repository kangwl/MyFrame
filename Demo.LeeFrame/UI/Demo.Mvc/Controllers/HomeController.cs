using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Common.DB;
using Demo.Common.IOC;
using Demo.Model;
using Exceptionless;
using Demo.Repository.IRepository;

namespace Demo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        //public IUserServ<User> UserServ { get; set; }
        public IUserRepository UserRepository { get; set; }
        // GET: Home
        public ActionResult Index()
        {
         
            try
            {
                // int.Parse("sdd");
                //var user = UserServ.GetOne(Guid.Parse("ff2e2a7d-e94a-44a6-ae99-82605b4393cf"));
                //Model.User user = new User()
                //{
                //    ID = Guid.NewGuid(),
                //    Name = "kwl",
                //    Age = 13,
                //    QQ = "767474055",
                //    CreateTime = DateTime.Now,
                //    UpdateTime = DateTime.Now
                //};
                
                //bool ok = UserRepository.Insert(user);

                Model.User user = new User();
                List<WhereItem> whereItems = new List<WhereItem>()
                {

                };
                var users = UserRepository.GetPaged(1, 10, new List<string>(),
                    new OrderByItem(nameof(user.CreateTime), false),
                    whereItems, user);

            }
            catch (System.Exception ex)
            {

                
               // ex.ToExceptionless().AddTags("HomeController-Index").Submit();
            }
          
            return View();
        }
    }
}