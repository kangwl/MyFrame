using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Demo.Common.IOC
{
    /*
    IOCMvcHelper.RegisterControllersByAssembly(Assembly.Load("Demo.Mvc"), Assembly.Load("Demo.Service.Dapper"),
        "Demo.Service.Dapper", "Serv");
     */

    public class IOCMvcHelper
    { 
        /// <summary>
        /// 根据程序集和服务的命名空间进行 MVC IOC 的注册
        /// </summary>
        /// <param name="controllAssem"></param>
        /// <param name="serviceAssem"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="serviceNameEndsWith"></param>
        public static void RegisterControllersByAssembly(Assembly controllAssem, Assembly serviceAssem,
            string serviceNamespace,
            string serviceNameEndsWith = "")
        {
            Func<Type, bool> whereFunc = t => t.Namespace == serviceNamespace;
            if (!string.IsNullOrEmpty(serviceNameEndsWith))
            {
                whereFunc += t => t.Name.EndsWith(serviceNameEndsWith);
            }
            var types = serviceAssem.GetTypes().Where(whereFunc);
            RegisterControllers(controllAssem, types.ToArray());
        }

        /// <summary>
        /// 根据类型注册 MVC IOC
        /// </summary>
        /// <param name="controllAssem"></param>
        /// <param name="types"></param>
        private static void RegisterControllers(Assembly controllAssem, params Type[] types)
        {
            //根据类型注册
            var builder = new ContainerBuilder();
            builder.RegisterTypes(types).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterControllers(controllAssem).PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
