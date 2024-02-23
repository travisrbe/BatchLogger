using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable(nameof(Batch), b => b.HasTrigger("Batch_INSERT"));
            builder.HasKey(batch => batch.Id);
            builder.Property(batch => batch.Id)
                .ValueGeneratedOnAdd();
            builder.Property(batch => batch.SpecificGravity)
                .HasDefaultValue(1.0)
                .IsRequired();
            builder.Property(batch => batch.OffsetYanPpm)
                .HasDefaultValue(10)
                .IsRequired();
            builder.Property(batch => batch.VolumeLiters)
                .HasDefaultValue(19)
                .IsRequired();
            builder.Property(batch => batch.IsComplete)
                .HasDefaultValue(false);
            builder.Property(batch => batch.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(batch => batch.IsNutrientLocked)
                .HasDefaultValue(false);
            builder.Property(batch => batch.YeastId)
                .HasDefaultValue(null)
                .IsRequired();

            //Relationships
            builder.HasOne(batch => batch.Yeast)
                .WithMany(batch => batch.Batches)
                .HasForeignKey(batch => batch.YeastId);

            builder.HasOne(batch => batch.Owner)
                .WithMany(batch => batch.CreatedBatches)
                .HasForeignKey(batch => batch.OwnerUserId);
        }
    }
}
