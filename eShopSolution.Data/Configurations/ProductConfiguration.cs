using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Description).HasColumnType("ntext");

            builder.Property(x => x.SeoAlias).HasMaxLength(200).IsRequired(false);

            builder.Property(x => x.Details).HasMaxLength(500).IsRequired(false);

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.OriginalPrice);

            builder.Property(x => x.Stock).HasDefaultValue(0);

            builder.Property(x => x.ViewCount).HasDefaultValue(0);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
