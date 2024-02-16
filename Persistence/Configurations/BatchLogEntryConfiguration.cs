using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal sealed class BatchLogEntryConfiguration : IEntityTypeConfiguration<BatchLogEntry>
    {
        public void Configure(EntityTypeBuilder<BatchLogEntry> builder)
        {
            builder.ToTable(nameof(BatchLogEntry));
            builder.HasKey(log => log.Id);
            builder.Property(log => log.Id).ValueGeneratedOnAdd();
            builder.Property(log => log.LogText).HasMaxLength(2048);
            builder.Property(log => log.BatchId).IsRequired();
            builder.Property(log => log.UserId).IsRequired();

            //Relationships
            builder.HasOne(log => log.Batch)
                .WithMany(log => log.LogEntries)
                .HasForeignKey(log => log.BatchId);

            builder.HasOne(log => log.User)
                .WithMany(log => log.LogEntries)
                .HasForeignKey(log => log.UserId);
        }
    }
}
