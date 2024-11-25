using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.Status)
                .HasDefaultValue(OrderStatus.Pending)
                .HasConversion<byte>();

            builder.HasOne(x => x.Buyer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CreatedBy);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProductId);

        }
    }
}
