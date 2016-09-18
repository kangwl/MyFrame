using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Demo.Common.IOC
{
    /*
     * 步骤：
     1.Regist
     2.Build
     3.Resolve
         */
    public static class IOCHepler
    {
        private static IContainer container { get; set; }

        private static readonly ContainerBuilder builder;
        static IOCHepler()
        {
            if (builder == null)
            {
                builder = new ContainerBuilder();
            }
        }

        public static void Build()
        {
            container = builder.Build();
        }

        public static void Regist<T>(string name="")
        {
            if (string.IsNullOrEmpty(name))
            {
                builder.RegisterType<T>();
            }
            else
            {
                builder.RegisterType<T>().Named<T>(name);
            }
        }


        public static void RegistSingleton<T>()
        {
            builder.RegisterType<T>().SingleInstance();
        }

        public static void Regist<T, IT>()
        {
            builder.RegisterType<T>().As<IT>().AsImplementedInterfaces().PropertiesAutowired();
        }

        public static void Regist<T, IT>(string name)
        {
            builder.RegisterType<T>().Named<IT>(name).As<IT>().AsImplementedInterfaces().PropertiesAutowired();
        }

        public static void Regist(params Type[] types)
        {
            if (!types.Any()) return;
            builder.RegisterTypes(types).AsImplementedInterfaces().PropertiesAutowired();
        }

        public static void RegistSingleton<T, IT>()
        {
            builder.RegisterType<T>().As<IT>().SingleInstance();
        }

        public static TIT Resolve<TIT>(string name = "")
        {
            using (var scop = container.BeginLifetimeScope())
            { 
                var tIt = string.IsNullOrEmpty(name) ? scop.Resolve<TIT>() : scop.ResolveNamed<TIT>(name);
                return tIt;
            }
        }

        #region MVC
        //mvc
        public static void RegisterControllers(Assembly controllAssem, params Type[] types)
        {
            //根据类型注册
            //var builder = new ContainerBuilder();
            builder.RegisterTypes(types).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterControllers(controllAssem).PropertiesAutowired();
            //var container = builder.Build();

        }
        //mvc的话 最后要加上这句
        public static void RegisterControllerEnd()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        } 
        #endregion
    }
}
