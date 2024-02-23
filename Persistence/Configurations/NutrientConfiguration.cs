using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class NutrientConfiguration : IEntityTypeConfiguration<Nutrient>
    {
        public void Configure(EntityTypeBuilder<Nutrient> builder)
        {
            builder.ToTable(nameof(Nutrient));
            builder.HasKey(nc => nc.Id);
            builder.Property(nc => nc.Id)
                .ValueGeneratedOnAdd();
            builder.Property(nc => nc.YanPpmPerGram)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(nc => nc.EffectivenessMutiplier)
                .IsRequired()
                .HasDefaultValue(1);
            builder.Property(na => na.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
