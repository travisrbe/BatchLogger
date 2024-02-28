using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class NutrientAdditionConfiguration : IEntityTypeConfiguration<NutrientAddition>
    {
        public void Configure(EntityTypeBuilder<NutrientAddition> builder)
        {
            //UNIQUE COMPOSITE CONSTRAINT for BatchId & Priority:
            //EF Core will not do this out of the box without custom annotations.
            //See: https://stackoverflow.com/questions/49526370/is-there-a-data-annotation-for-unique-constraint-in-ef-core-code-first
            //If this comes up again, consider doing this. For now, did it manually in migration NutrientAddition_UniqueComposite

            builder.ToTable(nameof(NutrientAddition));
            builder.HasKey(x => x.Id);
            
            builder.Property(na => na.MaxGramsPerLiterOverride)
                .HasDefaultValue(null);
            builder.Property(na => na.YanPpmPerGramOverride)
                .HasDefaultValue(null);
            builder.Property(na => na.EffectivenessMutiplierOverride)
                .HasDefaultValue(null);
            builder.Property(na => na.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(na => na.Priority);

            builder.HasOne(na => na.Batch)
                .WithMany(na => na.NutrientAdditions)
                .HasForeignKey(na => na.BatchId);

            builder.HasOne(na => na.Nutrient)
                .WithMany(na => na.NutrientAdditions)
                .HasForeignKey(na => na.NutrientId);
        }
    }
}