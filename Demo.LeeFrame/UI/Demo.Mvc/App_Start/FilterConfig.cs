using System.Web.Mvc;
using Demo.Mvc.Attribute;

namespace Demo.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}