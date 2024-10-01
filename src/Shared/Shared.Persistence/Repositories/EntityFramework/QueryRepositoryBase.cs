using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Core.Persistence;
using Shared.Core.Domain.Interfaces;

namespace Shared.Persistence.Repositories.EntityFramework;


public class QueryRepositoryBase<TEntity, TContext> : IQueryRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : BaseDbContext
{
    protected TContext Context { get; }
    public QueryRepositoryBase(TContext context)
    {
        Context = context;
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().CountAsync(predicate, cancellationToken);
    }

    public async Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
                                     bool enableTracking = false,
                                     bool ignoreQueryFilters = false,
                                     CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (ignoreQueryFilters) queryable = queryable.IgnoreQueryFilters();
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);
        if (filter != null) queryable = filter(queryable);

        return await queryable.AnyAsync(cancellationToken);
    }
    public async Task<TResult?> GetSingleAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                       Expression<Func<TEntity, bool>>? predicate = null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                       Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
                                                       bool isFirst = true,
                                                       bool enableTracking = false,
                                                       bool ignoreQueryFilters = false,
                                                       bool enableSplitQuery = false,
                                                       CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (ignoreQueryFilters) queryable = queryable.IgnoreQueryFilters();
        if (enableSplitQuery) queryable = queryable.AsSplitQuery();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);
        if (filter != null) queryable = filter(queryable);

        IQueryable<TResult> projectedQueryable = queryable.Select(selector);
        if (isFirst)
        {
            return await projectedQueryable.FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return await projectedQueryable.LastOrDefaultAsync(cancellationToken);
        }
    }
    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                 Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
                                                 bool isFirst = true,
                                                 bool enableTracking = false,
                                                 bool ignoreQueryFilters = false,
                                                 bool enableSplitQuery = false,
                                                 CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (ignoreQueryFilters) queryable = queryable.IgnoreQueryFilters();
        if (enableSplitQuery) queryable = queryable.AsSplitQuery();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);
        if (filter != null) queryable = filter(queryable);
        if (isFirst)
        {
            return await queryable.FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return await queryable.LastOrDefaultAsync(cancellationToken);
        }
    }
    public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                           Expression<Func<TEntity, bool>>? predicate = null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                           Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
                                                           int? skip = null,
                                                           int? take = null,
                                                           bool enableTracking = false,
                                                           bool ignoreQueryFilters = false,
                                                           bool enableSplitQuery = false,
                                                           CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (ignoreQueryFilters) queryable = queryable.IgnoreQueryFilters();
        if (enableSplitQuery) queryable = queryable.AsSplitQuery();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);

        IQueryable<TResult> projectedQueryable = queryable.Select(selector);

        if (skip.HasValue && take.HasValue)
        {
            projectedQueryable = projectedQueryable.Skip(skip.Value).Take(take.Value);
        }

        return await projectedQueryable.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                  Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
                                                  int? skip = null,
                                                  int? take = null,
                                                  bool enableTracking = false,
                                                  bool ignoreQueryFilters = false,
                                                  bool enableSplitQuery = false,
                                                  CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (ignoreQueryFilters) queryable = queryable.IgnoreQueryFilters();
        if (enableSplitQuery) queryable = queryable.AsSplitQuery();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);
        if (filter != null) queryable = filter(queryable);

        if (skip.HasValue && take.HasValue)
        {
            queryable = queryable.Skip(skip.Value).Take(take.Value);
        }

        return await queryable.ToListAsync(cancellationToken);
    }
    
}