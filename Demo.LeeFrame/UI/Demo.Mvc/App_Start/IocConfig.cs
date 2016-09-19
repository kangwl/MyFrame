using System.Linq;
using System.Reflection;
using Demo.Common.IOC;

namespace Demo.Mvc
{
    public class IocConfig
    {
        public static void Regist()
        {

            //IOCHepler.Regist(Assembly.Load("Demo.Repository").GetTypes().Where(one=>one.Name.EndsWith("Repository")).ToArray());
            IOCHepler.RegisterControllers(Assembly.Load("Demo.Mvc"),
                Assembly.Load("Demo.Repository").GetTypes().Where(one => one.Name.EndsWith("Repository")).ToArray());
            IOCHepler.Build();
            IOCHepler.ResolveControllerBuild();

        }
    }
}