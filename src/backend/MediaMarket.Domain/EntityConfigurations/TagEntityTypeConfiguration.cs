using MediaMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("images");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(150);

            builder.Property(x => x.Slug).HasMaxLength(150);

            builder.HasIndex(x => x.Slug).IsUnique();
        }
    }
}
