using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class BalanceHistoryEntityTypeConfiguration : IEntityTypeConfiguration<BalanceHistory>
    {
        public void Configure(EntityTypeBuilder<BalanceHistory> builder)
        {
            builder.ToTable("BalanceHistories");

            builder.Property(x => x.NewBalance)
                .HasDefaultValue(0);

            builder.Property(x => x.PreviousBalance)
                .HasDefaultValue(0);

            builder.Property(p => p.TransactionType)
                .HasDefaultValue(TransactionType.Sell)
                .HasConversion<byte>();

            builder.HasOne(x => x.User)
                .WithMany(x => x.BalanceHistories)
                .HasForeignKey(x => x.UserId);

            builder.HasIndex(x => x.TransactionId);
        }
    }
}
