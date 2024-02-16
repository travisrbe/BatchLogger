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
            builder.Property(nc => nc.Name)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(nc => nc.Manufacturer)
                .HasMaxLength(128);
            builder.Property(nc => nc.YanPpmPerGram)
                .IsRequired();
            builder.Property(nc => nc.EffectivenessMutiplier)
                .IsRequired()
                .HasDefaultValue(1);
        }
    }
}
