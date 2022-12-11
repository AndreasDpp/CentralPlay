using Microsoft.Azure.Cosmos;

namespace CentralPlay.Backend.Repository.Domain.Context.Interfaces
{
    public interface ICosmosDbContainer
    {
        /// <summary>
        /// Azure Cosmos DB Container
        /// </summary>
        Container _container { get; }
    }
}
