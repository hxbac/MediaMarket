using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("products");

            entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

            entity.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property(p => p.Price)
                .HasDefaultValue(0);

            entity.Property(p => p.SellerId)
                .HasDefaultValue(0);

            entity.Property(p => p.ProductType)
                .HasDefaultValue(ProductType.Video)
                .HasConversion<byte>();

            entity.Property(p => p.ProductStatus)
                .HasDefaultValue(ProductStatus.Draft)
                .HasConversion<byte>();

            entity.Property(p => p.ProductContentStatus)
                .HasDefaultValue(ProductContentStatus.Waiting)
                .HasConversion<byte>();
        }
    }
}
