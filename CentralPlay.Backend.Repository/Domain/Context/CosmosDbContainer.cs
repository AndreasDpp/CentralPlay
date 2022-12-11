using CentralPlay.Backend.Repository.Domain.Context.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CentralPlay.Backend.Repository.Domain.Context
{
    public class CosmosDbContainer : ICosmosDbContainer
    {
        public Container _container { get; }

        //Container ICosmosDbContainer._container => _container;

        public CosmosDbContainer(CosmosClient cosmosClient,
                                 string databaseName,
                                 string containerName)
        {
            this._container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}
