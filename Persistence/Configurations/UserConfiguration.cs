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
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.Property(user => user.FirstName).HasMaxLength(128);
            builder.Property(user => user.LastName).HasMaxLength(128);
            builder.Property(user => user.ChosenName).HasMaxLength(128);
            builder.Property(user => user.CollaboratorToken).ValueGeneratedOnAdd();
        }
    }
}
