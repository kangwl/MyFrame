using System.Data.Entity.ModelConfiguration;
using Demo.Model;

namespace Demo.DataCenter.EF.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(one => one.ID); 
            this.Property(one => one.Age).IsOptional();
            this.Property(one => one.Name).IsRequired().HasMaxLength(20);
            this.Property(one => one.QQ).IsOptional().HasMaxLength(15);
            this.Property(one => one.Phone).IsOptional().HasMaxLength(20);
            this.Property(one => one.CreateTime).IsRequired();
            this.Property(one => one.UpdateTime).IsOptional();
        }
    }
}
