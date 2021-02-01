using TShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ShipAddress).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ShipName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ShipEmail).IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.ShipPhoneNumber).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        }
    }
}
