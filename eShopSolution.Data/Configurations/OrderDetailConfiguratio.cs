using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class OrderDetailConfiguratio : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(od => new { od.OrderId, od.ProductId });

            
            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od=>od.OrderId);
            builder.HasOne(od => od.Product).WithMany(p => p.OrderDetails).HasForeignKey(od => od.ProductId);
        }
    }
}
