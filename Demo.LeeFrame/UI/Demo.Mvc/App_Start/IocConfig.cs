using System.Reflection;
using Demo.Common.IOC;

namespace Demo.Mvc
{
    public class IocConfig
    {
        public static void Regist()
        {
            IOCMvcHelper.RegisterControllersByAssembly(Assembly.Load("Demo.Mvc"), Assembly.Load("Demo.Service.Dapper"),
                "Demo.Service.Dapper", "Serv");
        }
    }
}