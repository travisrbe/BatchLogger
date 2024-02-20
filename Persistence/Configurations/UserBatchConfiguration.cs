using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class UserBatchConfiguration : IEntityTypeConfiguration<UserBatch>
    {
        public void Configure(EntityTypeBuilder <UserBatch> builder)
        {
            builder.ToTable(nameof(UserBatch));
            builder.HasKey(ub => ub.Id);
            builder.Property(ub => ub.UserId)
                .IsRequired();
            builder.Property(ub => ub.BatchId)
                .IsRequired();

            builder.HasOne(ub => ub.User)
                .WithMany(ub => ub.UserBatches)
                .HasForeignKey(ub => ub.UserId);
            builder.HasOne(ub => ub.Batch)
                .WithMany(ub => ub.UserBatches)
                .HasForeignKey(ub => ub.BatchId);
        }
    }
}
