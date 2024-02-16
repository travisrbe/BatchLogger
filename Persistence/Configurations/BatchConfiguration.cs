using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal sealed class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable(nameof(Batch));
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
            builder.Property(batch => batch.Complete)
                .HasDefaultValue(false);
            builder.Property(batch => batch.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(batch => batch.YeastId)
                .HasDefaultValue(null)
                .IsRequired();


            //Relationships
            builder.HasOne(batch => batch.Yeast)
                .WithMany(batch => batch.Batches)
                .HasForeignKey(batch => batch.YeastId);

            builder.HasOne(batch => batch.Creator)
                .WithMany(batch => batch.CreatedBatches)
                .HasForeignKey(batch => batch.CreatorUserId);
        }
    }
}
