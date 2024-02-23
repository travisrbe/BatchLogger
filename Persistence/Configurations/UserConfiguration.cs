﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(user => user.CollaboratorToken)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");
            builder.Property(user => user.CreatedDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(user => user.UpdateDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
