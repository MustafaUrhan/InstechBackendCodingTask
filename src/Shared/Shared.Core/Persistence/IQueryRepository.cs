using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Core.Domain.Interfaces;

namespace Shared.Core.Persistence;

public interface IQueryRepository<T> where T : IEntity
{
    /// <summary>
    /// Returns the total count of entities based on the provided criteria.
    /// </summary>
    /// <param name="predicate">The criteria of the query that will return the count</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns bool if any entity exists based on the provided criteria.
    /// <param name="predicate">Optional: A filter condition for the entity.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="filter">Optional: A filter for filter parameters and can be use WhereIf,WhereIfOrderBy Extensions.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the retrieved data.</param>
    /// <param name="ignoreQueryFilters">It disable, !(x.IsDeleted ?? false) And (x.IsActive). It included every query</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A bool,By the provided predicate and filter criteria.</returns>
    Task<bool> GetAnyAsync(Expression<Func<T, bool>>? predicate = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                        Func<IQueryable<T>, IQueryable<T>>? filter = null,
                        bool enableTracking = false,
                        bool ignoreQueryFilters = false,
                        CancellationToken cancellationToken = default);
    /// <summary>
    /// IF YOU WANT TO MAP THE RESULT TO A DTO, USE THIS METHOD.
    /// Retrieves a single DTO by mapping it from an entity, with optional filtering,
    /// including related data, ordering, and other query customization options.
    /// </summary>
    /// <typeparam name="TResult">The type of the DTO to be projected.</typeparam>
    /// <param name="selector">A function to map the entity into the desired DTO type.</param>
    /// <param name="isFirst">Determines whether to retrieve the first or last entity.</param>
    /// <param name="predicate">Optional: A filter condition for the entity.</param>
    /// <param name="include">Optional: A function to include related data.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="filter">Optional: A filter for filter parameters and can be use WhereIf,WhereIfOrderBy Extensions.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the retrieved data.</param>
    /// <param name="enableSplitQuery">Whether to split the query for better performance.</param>
    /// <param name="ignoreQueryFilters">It disable, !(x.IsDeleted ?? false) And (x.IsActive). It included every query</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A single DTO based on the provided selector and first/last criteria.</returns>
    Task<TResult?> GetSingleAsync<TResult>(Expression<Func<T, TResult>> selector,
                                          Expression<Func<T, bool>>? predicate = null,
                                          Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                          Func<IQueryable<T>, IQueryable<T>>? filter = null,
                                          bool isFirst = true,
                                          bool enableTracking = false,
                                          bool ignoreQueryFilters = false,
                                          bool enableSplitQuery = false,
                                          CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single entity, with optional filtering,
    /// including related data, ordering, and other query customization options.
    /// </summary>
    /// <param name="selector">A function to map the entity into the desired DTO type.</param>
    /// <param name="predicate">Optional: A filter condition for the entity.</param>
    /// <param name="include">Optional: A function to include related data.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="filter">Optional: A filter for filter parameters and can be use WhereIf,WhereIfOrderBy Extensions.</param>
    /// <param name="isFirst">Determines whether to retrieve the first or last entity.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the retrieved data.</param>
    /// <param name="enableSplitQuery">Whether to split the query for better performance.</param>
    /// <param name="ignoreQueryFilters">It disable, !(x.IsDeleted ?? false) And (x.IsActive). It included every query</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A single DTO based on the provided selector and first/last criteria.</returns>
    Task<T?> GetSingleAsync(Expression<Func<T, bool>>? predicate = null,
                            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                            Func<IQueryable<T>, IQueryable<T>>? filter = null,
                            bool isFirst = true,
                            bool enableTracking = false,
                            bool ignoreQueryFilters = false,
                            bool enableSplitQuery = false,
                            CancellationToken cancellationToken = default);


    /// <summary>
    /// IF YOU WANT TO MAP THE RESULT TO A DTO, USE THIS METHOD.
    /// Retrieves a list of DTOs by mapping them from entities, with optional filtering,
    /// including related data, ordering, and pagination.
    /// </summary>
    /// <typeparam name="TResult">The type of the DTO to be projected.</typeparam>
    /// <param name="selector">A function to map each entity into the desired DTO type.</param>
    /// <param name="predicate">Optional: A filter condition for the entities.</param>
    /// <param name="include">Optional: A function to include related data.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="filter">Optional: A filter for filter parameters and can be use WhereIf,WhereIfOrderBy Extensions.</param>
    /// <param name="skip">Optional: The number of items to skip.</param>
    /// <param name="take">Optional: The maximum number of items to retrieve.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the retrieved data.</param>
    /// <param name="enableSplitQuery">Whether to split the query for better performance.</param>
    /// <param name="ignoreQueryFilters">It disable, !(x.IsDeleted ?? false) And (x.IsActive). It included every query</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of DTOs based on the provided selector.</returns>
    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector,
                                              Expression<Func<T, bool>>? predicate = null,
                                              Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                              Func<IQueryable<T>, IQueryable<T>>? filter = null,
                                              int? skip = null,
                                              int? take = null,
                                              bool enableTracking = false,
                                              bool ignoreQueryFilters = false,
                                              bool enableSplitQuery = false,
                                              CancellationToken cancellationToken = default);
    /// <summary>
    /// Retrieves a list of entities with optional filtering, including related data,
    /// ordering, and pagination.
    /// </summary>
    /// <param name="predicate">Optional: A filter condition for the entities.</param>
    /// <param name="include">Optional: A function to include related data.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="filter">Optional: A filter for filter parameters and can be use WhereIf,WhereIfOrderBy Extensions.</param>
    /// <param name="skip">Optional: The number of items to skip.</param>
    /// <param name="take">Optional: The maximum number of items to retrieve.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the retrieved data.</param>
    /// <param name="enableSplitQuery">Whether to split the query for better performance.</param>
    /// <param name="ignoreQueryFilters">It disable, !(x.IsDeleted ?? false) And (x.IsActive). It included every query</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of entities based on the provided criteria.</returns>
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                               Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                               Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                               Func<IQueryable<T>, IQueryable<T>>? filter = null,
                               int? skip = null,
                               int? take = null,
                               bool enableTracking = false,
                               bool ignoreQueryFilters = false,
                               bool enableSplitQuery = false,
                               CancellationToken cancellationToken = default);

}