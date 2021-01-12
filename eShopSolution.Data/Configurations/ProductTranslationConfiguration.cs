using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Details).HasMaxLength(500);
            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
            builder.Property(x => x.SeoDescription).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SeoTitle).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Language).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.LanguageId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.ProductId);
        }
    }
}
