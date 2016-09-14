using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IocConfig.Regist();
            //
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        //Global文件的EndRequest监听Response状态码
        protected void Application_EndRequest()
        {
            var statusCode = Context.Response.StatusCode;
            var routingData = Context.Request.RequestContext.RouteData;
            if (statusCode == 404 || statusCode == 500)
            {
                Response.Clear();
                Response.RedirectToRoute("Default", new { controller = "Error", action = "Index" });
            }
        }
    }
}
