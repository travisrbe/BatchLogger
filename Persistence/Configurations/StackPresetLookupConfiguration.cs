using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class StackPresetLookupConfiguration
    {
        public void Configure(EntityTypeBuilder<StackPresetLookup> builder)
        {
            builder.ToTable(nameof(StackPresetLookup));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(lookup => lookup.StackPreset)
                .WithMany(preset => preset.StackPresetLookups)
                .HasForeignKey(preset => preset.StackPresetId);

            builder.HasOne(lookup => lookup.Nutrient)
                .WithMany(n => n.StackPresetLookups)
                .HasForeignKey(lookup => lookup.NutrientId);
        }
    }
}
