using MediaMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class ProductDiscountEntityTypeConfiguration : IEntityTypeConfiguration<ProductDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.HasOne<Product>()
                .WithMany(x => x.ProductDiscounts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
