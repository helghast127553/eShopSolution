using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Description).HasColumnType("ntext");

            builder.Property(x => x.SeoAlias).HasMaxLength(200).IsRequired(false);

            builder.Property(x => x.SeoDescription).HasColumnType("ntext").IsRequired(false);

            builder.Property(x => x.SeoTitle).HasMaxLength(200).IsRequired(false);
        }
    }
}
