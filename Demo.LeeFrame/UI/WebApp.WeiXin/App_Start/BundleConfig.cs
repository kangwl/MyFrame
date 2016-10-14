using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebApp.WeiXin.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundle)
        {
            bundle.Add(new StyleBundle("~/css") {Orderer=new AsIsBundleOrderer()}.Include(
                "~/Content/bootstrap*"));
            bundle.Add(new ScriptBundle("~/js") {Orderer=new AsIsBundleOrderer()}.Include(
                "~/scripts/jquery-1.10.2*",
                "~/scripts/bootstrap*"
                ));
        }
 
    }
    internal class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}