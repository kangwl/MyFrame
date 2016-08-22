using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Model;
using Demo.Service.Interface;

namespace Demo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IUserServ<User> UserServ { get; set; } 
        // GET: Home
        public ActionResult Index()
        {
            var user = UserServ.GetOne(Guid.Parse("ff2e2a7d-e94a-44a6-ae99-82605b4393cf"));
            return View(user);
        }
    }
}