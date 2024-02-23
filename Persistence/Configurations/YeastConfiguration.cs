using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class YeastConfiguration : IEntityTypeConfiguration<Yeast>
    {
        public void Configure(EntityTypeBuilder<Yeast> builder)
        {
            builder.ToTable(nameof(Yeast));
            builder.HasKey(yeast => yeast.Id);
            builder.Property(yeast => yeast.Id)
                .ValueGeneratedOnAdd();
            builder.Property(yeast => yeast.NutrientReqMult)
                .IsRequired();
            builder.Property(yeast => yeast.NutrientReqMult)
                .HasDefaultValue((double)0.9);
            builder.Property(yeast => yeast.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
