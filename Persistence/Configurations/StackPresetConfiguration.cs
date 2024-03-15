using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class StackPresetConfiguration
    {
        public void Configure(EntityTypeBuilder<StackPreset> builder)
        {
            builder.ToTable(nameof(StackPreset));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
