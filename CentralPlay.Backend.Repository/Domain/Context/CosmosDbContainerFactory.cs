using CentralPlay.Backend.Repository.Domain.Context.Interfaces;
using CentralPlay.Backend.Repository.Domain.Context.Models;
using Microsoft.Azure.Cosmos;

namespace CentralPlay.Backend.Repository.Domain.Context
{
    public class CosmosDbContainerFactory : ICosmosDbContainerFactory
    {
        #region Fields 

        /// <summary>
        /// Azure Cosmos DB Client
        /// </summary>
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly List<ContainerInfo> _containers;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cosmosClient"></param>
        /// <param name="databaseName"></param>
        /// <param name="containers"></param>
        public CosmosDbContainerFactory(CosmosClient cosmosClient,
                                   string databaseName,
                                   List<ContainerInfo> containers)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _containers = containers ?? throw new ArgumentNullException(nameof(containers));
            _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        }

        #endregion

        public ICosmosDbContainer GetContainer(string containerName)
        {
            if (_containers.Where(x => x.Name == containerName) == null)
            {
                throw new ArgumentException($"Unable to find container: {containerName}");
            }

            return new CosmosDbContainer(_cosmosClient, _databaseName, containerName);
        }

        public async Task EnsureDbSetupAsync()
        {
            Microsoft.Azure.Cosmos.DatabaseResponse database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);

            foreach (ContainerInfo container in _containers)
            {
                await database.Database.CreateContainerIfNotExistsAsync(container.Name, $"{container.PartitionKey}");
            }
        }
    }
}
