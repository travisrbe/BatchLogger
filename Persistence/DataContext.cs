using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Persistence.Configurations;
//using Persistence.Configurations;

namespace Persistence
{
    public sealed class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            //Applies configuration from all IEntityTypeConfiguration<TEntity>
            //instances that are defined in provided assembly.
            //No need to use `modelBuilder.ApplyConfiguration(new TypeConfiguration());`
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Yeast> Yeasts { get; set; }
        public DbSet<BatchLogEntry> LogEntries { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<NutrientAddition> NutrientAdditions { get; set; }
        public DbSet<UserBatch> UserBatches { get; set; }

    }
}
