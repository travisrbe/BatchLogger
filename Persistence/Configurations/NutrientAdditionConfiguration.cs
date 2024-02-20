using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class NutrientAdditionConfiguration : IEntityTypeConfiguration<NutrientAddition>
    {
        public void Configure(EntityTypeBuilder<NutrientAddition> builder)
        {
            builder.ToTable(nameof(NutrientAddition));
            builder.HasKey(x => x.Id);
            builder.Property(na => na.MaxGramsPerLiterOverride).HasDefaultValue(null);
            builder.Property(na => na.YanPpmPerGramOverride).HasDefaultValue(null);
            builder.Property(na => na.EffectivenessMutiplierOverride).HasDefaultValue(null);

            builder.HasOne(na => na.Batch)
                .WithMany(na => na.NutrientAdditions)
                .HasForeignKey(na => na.BatchId);

            builder.HasOne(na => na.Nutrient)
                .WithMany(na => na.NutrientAdditions)
                .HasForeignKey(na => na.NutrientId);
        }
    }
}
