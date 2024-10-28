using MediaMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class PreviewEntityTypeConfiguration : IEntityTypeConfiguration<Preview>
    {
        public void Configure(EntityTypeBuilder<Preview> builder)
        {
            builder.ToTable("previews");

            builder.Property(x => x.WatermarkEnabled)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.PreviewInfo)
                .HasColumnType("text");

            builder.HasOne(x => x.Product)
                .WithOne(p => p.Preview)
                .HasForeignKey<Preview>(p => p.ProductId);
        }
    }
}
