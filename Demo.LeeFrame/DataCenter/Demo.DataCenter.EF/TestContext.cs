using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Demo.DataCenter.EF.Map;

namespace Demo.DataCenter.EF
{
    public class TestContext : DbContext
    {
        public TestContext() : base("name=mysqlConn")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TestContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<TestContext>());
           // Database.SetInitializer(new NullDatabaseInitializer<TestContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Assembly assembly = Assembly.Load("Demo.DataCenter.EF");
            var types = assembly.GetTypes().Where(one => one.Namespace == "Demo.DataCenter.EF.Map").ToList();
            types.ForEach(one =>
            {
              var obj=  Activator.CreateInstance(one);
                modelBuilder.Configurations.Add(obj);
            }); 
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CommissionDetailMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<User> User { get; set; } 
        //public DbSet<Commission> Commission { get; set; } 

    }
}
