using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configrations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(n => n.Name).HasMaxLength(100);
            builder.Property(n => n.Description).HasMaxLength(500);
            builder.Property(n => n.PictureUrl).HasMaxLength(200);
            builder.Property(n => n.Price).HasPrecision(18,2);

            builder.HasOne(p=>p.ProductBrand).WithMany().HasForeignKey(p=>p.BrandId);
            builder.HasOne(p=>p.ProductType).WithMany().HasForeignKey(p=>p.TypeId);
        }
    }
}
