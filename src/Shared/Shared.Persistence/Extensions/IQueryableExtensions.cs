using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Shared.Persistence.Extensions;

public static class IQueryableExtentions
{
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    public static IQueryable<TEntity> WhereIfInclude<TEntity>(this IQueryable<TEntity> query, bool condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include)
    {
        if (condition)
        {
            return include(query);
        }
        else
        {
            return query;
        }
    }
    public static IQueryable<TEntity> WhereIfOrderBy<TEntity>(this IQueryable<TEntity> query,
                                                              bool condition,
                                                              string propertyName,
                                                              bool isDesc = false)
    {
        if (!condition && string.IsNullOrEmpty(propertyName)) return query;
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);
        var method = isDesc ? "OrderByDescending" : "OrderBy";
        var orderByExpression = Expression.Call(typeof(Queryable), method, new[] { typeof(TEntity), property.Type }, query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<TEntity>(orderByExpression);
    }
    public static IQueryable<TEntity> WhereIfOrderByQueryable<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, object>>? orderBy, string? direction)
    {
        if (orderBy != null && condition)
        {
            if (direction == "desc")
            {
                query = query.OrderByDescending(orderBy);
            }
            else
            {
                query = query.OrderBy(orderBy);
            }
        }
        return query;
    }
}