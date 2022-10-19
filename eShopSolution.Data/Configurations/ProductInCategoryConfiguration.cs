using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration: IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(t => t.Product).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Category).WithMany(pc => pc.ProductInCategories)
              .HasForeignKey(pc => pc.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
