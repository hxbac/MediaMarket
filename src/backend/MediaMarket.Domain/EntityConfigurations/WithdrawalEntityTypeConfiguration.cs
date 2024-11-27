using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarket.Domain.EntityConfigurations
{
    public class WithdrawalEntityTypeConfiguration : IEntityTypeConfiguration<Withdrawal>
    {
        public void Configure(EntityTypeBuilder<Withdrawal> builder)
        {
            builder.ToTable("Withdrawals");

            builder.Property(x => x.Amount)
                .HasDefaultValue(0);

            builder.Property(p => p.WithdrawalStatus)
                .HasDefaultValue(WithdrawalStatus.Pending)
                .HasConversion<byte>();

            builder.HasOne(x => x.UserRequest)
                .WithMany(x => x.Withdrawals)
                .HasForeignKey(x => x.CreatedBy);
        }
    }
}
