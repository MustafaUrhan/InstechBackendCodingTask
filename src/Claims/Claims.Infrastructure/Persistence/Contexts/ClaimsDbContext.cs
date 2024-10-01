using Arch.Infrastructure.Configurations;
using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Interfaces;
using Shared.Persistence.Extensions;
using Shared.Persistence.Repositories.EntityFramework;

namespace Claims.Infrastructure.Persistence.Contexts;

public class ClaimsDbContext : BaseDbContext
{
    public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options)
    {
    }

    // // Just use for Migration
    // public ClaimsDbContext() : base()
    // {
    // }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Server=localhost,14330;Database=ClaimDb;User Id=sa;Password=P@ssw0rd!;Encrypt=False");
    // }

    public DbSet<Claim> Claims { get; set; }
    public DbSet<Cover> Covers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                SoftDeleteQueryExtension.AddSoftDeleteQueryFilter(entityType);
            }
        }
        new ClaimTypeConfiguration().Configure(modelBuilder.Entity<Claim>());
        new CoverTypeConfiguration().Configure(modelBuilder.Entity<Cover>());
    }
}