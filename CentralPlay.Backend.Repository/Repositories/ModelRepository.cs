using CentralPlay.Backend.Repository.Domain.Context.Interfaces;
using CentralPlay.Backend.Repository.Domain.Entities;
using CentralPlay.Backend.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CentralPlay.Backend.Repository.Repositories
{
    public class ModelRepository : CosmosDbRepository<Model>, IModelRepository
    {
        /// <summary>
        ///  CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "models";

        /// <summary>
        ///  Generate Id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(Model entity) => $"{Guid.NewGuid()}";

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(Model entity) => new PartitionKey(entity.UserId);

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="factory"></param>
        public ModelRepository(ICosmosDbContainerFactory factory) : base(factory)
        { 
        }

        #endregion
    }
}

