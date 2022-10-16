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
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            builder.ToTable("ProductRating");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Review).HasColumnType("ntext");

            builder.HasOne(x => x.Product)
               .WithMany(x => x.ProductRatings)
               .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.AppUser)
              .WithMany(x => x.ProductRatings)
              .HasForeignKey(x => x.UserId);
        }
    }
}
