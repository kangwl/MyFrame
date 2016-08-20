using System.Data.Entity;
using Demo.DataCenter.EF;

namespace Demo.Service.Extend
{
    public class ServiceBase<T> where T : class
    {
        public ServiceBase()
        {
            Context = new TestContext();
            Data = Context.Set<T>();
        }  
        public TestContext Context { get; set; }
        public DbSet<T> Data { get; set; }
    }
}
