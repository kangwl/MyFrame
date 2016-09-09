using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Common;
using XK.WeiXin;
using XK.WeiXin.Author;

namespace WebApp.WeiXin.Controllers
{
    public class WeiXinController : Controller
    {
        // GET: WeiXin
        [HttpGet]
        [ActionName("Index")]
        public void Get()
        {
            try
            {
                XK.WeiXin.Enter enter = new Enter(AppConfig.Instance.Token, HttpContext.Request, HttpContext.Response);
                enter.StartWeiXin();
            }
            catch (Exception ex)
            {
                Log log = new Log();
                log.WriteLog("Get:" + ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public void Post()
        {
            try
            {
                XK.WeiXin.Enter enter = new Enter(AppConfig.Instance.Token, HttpContext.Request, HttpContext.Response);
                enter.StartWeiXin();
            }
            catch (Exception ex)
            {
                Log log = new Log();
                log.WriteLog("Post:" + ex.Message + ex.StackTrace);
            }
        }


    }
}