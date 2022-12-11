using CentralPlay.Backend.Repository.Domain.Context.Interfaces;
using CentralPlay.Backend.Repository.Domain.Entities.Base;
using CentralPlay.Backend.Repository.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CentralPlay.Backend.Repository.Repositories
{
    public abstract class CosmosDbRepository<T> : IGenericRepository<T>, IContainerContext<T> where T : BaseEntity
    {
        /// <summary>
        /// Name of the CosmosDB container
        /// </summary>
        public abstract string ContainerName { get; }

        /// <summary>
        /// Generate id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract string GenerateId(T entity);

        /// <summary>
        /// Resolve the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public abstract PartitionKey ResolvePartitionKey(T entity);

        #region Fields

        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;

        private readonly Microsoft.Azure.Cosmos.Container _container;

        #endregion

        #region

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cosmosDbContainerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            this._cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
            this._container = this._cosmosDbContainerFactory.GetContainer(ContainerName)._container;
        }

        #endregion

        public async Task AddAsync(T item)
        {
            item.Id = GenerateId(item);
            await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item));
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
        }

        public async Task<T> GetAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetEnumerableAsync(string queryString)
        {
            FeedIterator<T> resultSetIterator = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<T> response = await resultSetIterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
        public IQueryable<T> GetQueryable()
        {
            return _container.GetItemLinqQueryable<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetEnumerableAsync(IQueryable<T> queryable)
        {
            FeedIterator<T> iterator = queryable.ToFeedIterator<T>();

            List<T> results = new List<T>();
            while (iterator.HasMoreResults)
            {
                FeedResponse<T> response = await iterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<int> GetCountAsync(IQueryable<T> queryable)
        {
            return await queryable.CountAsync();
        }

        public async Task UpdateAsync(string id, T item)
        {
            await this._container.UpsertItemAsync<T>(item, ResolvePartitionKey(item));
        }
    }
}
