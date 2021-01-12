using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace eShopSolution.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Caption).IsRequired();

            builder.HasOne(p => p.product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);

        }
    }
}
