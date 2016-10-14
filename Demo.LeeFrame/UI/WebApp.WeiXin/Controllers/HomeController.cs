using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFilms.Core;

namespace WebApp.WeiXin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //string accesstoken = XK.WeiXin.Author.AccessToken.Instance.Value;
            //ViewBag.AccessToken = accesstoken;
           
            return View();
        }

        public ActionResult Film()
        {
            string url = FilmAnalyze.GetVipFilms();
            return Redirect("/film.html"); 
        }
    }
}