using System.Data.Entity;
using Demo.DataCenter.EF;

namespace Demo.Service.Extend
{
    public class ServiceBaseSimple<T> where T : class
    {
        public TestContext GetContext()
        { 
            var ctx = new TestContext();
            Data = ctx.Set<T>();
            return ctx;
        }

        public DbSet<T> Data { get; set; }
        
    }
}
