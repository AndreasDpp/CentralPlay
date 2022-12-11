using CentralPlay.Backend.Repository.Domain.Entities.Base;

namespace CentralPlay.Backend.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get items given a string SQL query directly.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEnumerableAsync(string query);

        /// <summary>
        /// Gets the queryable for queryable method
        /// </summary>
        /// <returns>IQueryable object of T</returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Get items given a specification.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>List of T</returns>
        Task<IEnumerable<T>> GetEnumerableAsync(IQueryable<T> queryable);

        /// <summary>
        ///     Get the count on items that match the specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>Count</returns>
        Task<int> GetCountAsync(IQueryable<T> specification);

  
        Task<T> GetAsync(string id, string partitionKey);
        Task AddAsync(T item);
        Task UpdateAsync(string id, T item);
        Task DeleteAsync(string id, string partitionKey);
    }
}
