using MediaMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class VideoSolutionEntityTypeConfiguration : IEntityTypeConfiguration<VideoSolution>
    {
        public void Configure(EntityTypeBuilder<VideoSolution> builder)
        {
            builder.ToTable(nameof(VideoSolution));

            builder.HasOne(x => x.Product)
                .WithMany(x => x.VideoSolutions)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
