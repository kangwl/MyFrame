using System.Reflection;
using Demo.Common.Mvc.IOCMvc;

namespace Demo.Mvc
{
    public class IocConfig
    {
        public static void Regist()
        {
            IOC.RegisterControllersByAssembly(Assembly.Load("Demo.Mvc"), Assembly.Load("Demo.Service"),
                "Demo.Service", "Serv");
        }
    }
}