using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.WeiXin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string accesstoken = XK.WeiXin.Author.AccessToken.Instance.Value;
            ViewBag.AccessToken = accesstoken;
            return View();
        }
    }
}