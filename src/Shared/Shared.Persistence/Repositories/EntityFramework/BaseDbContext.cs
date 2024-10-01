using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Interfaces;
using Shared.Persistence.Extensions;

namespace Shared.Persistence.Repositories.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
        }
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //     {
        //         //other automated configurations left out
        //         if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
        //         {
        //             SoftDeleteQueryExtension.AddSoftDeleteQueryFilter(entityType);
        //         }
        //     }
        // }
        //Override SaveChanges to soft delete, and modify the state of the entity to modified and CreatedAt and UpdatedAt
        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Added);
            foreach (var entry in addedEntities)
            {
                entry.CurrentValues["CreatedAt"] = DateTime.Now;
            }
            var modifiedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Modified);
            foreach (var entry in modifiedEntities)
            {
                entry.CurrentValues["ModifiedAt"] = DateTime.Now;
            }

            var deletedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Deleted);
            foreach (var entry in deletedEntities)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
                entry.CurrentValues["IsActive"] = false;
                entry.CurrentValues["ModifiedAt"] = DateTime.Now;
            }
            return base.SaveChanges();
        }

        //Override SaveChangesAsync to soft delete, and modify the state of the entity to modified and CreatedAt and UpdatedAt
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            var addedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Added);
            foreach (var entry in addedEntities)
            {
                entry.CurrentValues["CreatedAt"] = DateTime.Now;
            }
            var modifiedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Modified);
            foreach (var entry in modifiedEntities)
            {
                entry.CurrentValues["ModifiedAt"] = DateTime.Now;
            }

            var deletedEntities = ChangeTracker.Entries<IEntity>().Where(c => c.State is EntityState.Deleted);
            foreach (var entry in deletedEntities)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
                entry.CurrentValues["IsActive"] = false;
                entry.CurrentValues["ModifiedAt"] = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}