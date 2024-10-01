

using System.Linq.Expressions;
using Shared.Core.Domain.Interfaces;

namespace Shared.Core.Persistence;

public interface ICommandRepository<T> where T : IEntity
{
    /// <summary>
    /// Add entity to the database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Add range of entities to the database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity in the database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update range of entities in the database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete entity from the database
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete range of entities from the database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}