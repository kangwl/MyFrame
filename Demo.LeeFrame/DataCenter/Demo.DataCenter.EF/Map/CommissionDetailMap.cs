using System.Data.Entity.ModelConfiguration;
using Demo.Model;

namespace Demo.DataCenter.EF.Map
{
    public class CommissionDetailMap : EntityTypeConfiguration<CommissionDetail>
    {
        public CommissionDetailMap()
        {
            this.ToTable("CommissionDetail");
            this.HasKey(one => one.ID);
            this.Property(one => one.Amt).IsRequired().HasPrecision(20, 2);
            this.Property(one => one.UserID).IsRequired();
        }
    }
}
