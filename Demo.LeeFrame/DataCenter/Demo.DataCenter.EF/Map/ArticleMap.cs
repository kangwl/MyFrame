using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Demo.Model;

namespace Demo.DataCenter.EF.Map
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.ToTable("Article");
            this.Property(one => one.Title).IsRequired().HasMaxLength(100);
            this.Property(one => one.Content).IsUnicode().IsMaxLength();
            this.Property(one => one.CreateTime).IsRequired();
        }
    }
}
