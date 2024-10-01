using System.Linq.Expressions;
using Shared.Core.Domain.Interfaces;
using Shared.Core.Persistence;

namespace Shared.Persistence.Repositories.EntityFramework;

public class CommandRepositoryBase<TEntity, TContext> : ICommandRepository<TEntity>
    where TEntity : class, IEntity, ISoftDelete
    where TContext : BaseDbContext
{
    protected TContext Context { get; }

    public CommandRepositoryBase(TContext context)
    {
        Context = context;
    }
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await Context.Set<TEntity>()
                     .AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>()
                     .AddRangeAsync(entities, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>()
               .Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>()
               .UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }
    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>()
               .Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>()
               .RemoveRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }
}