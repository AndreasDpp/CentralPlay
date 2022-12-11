using CentralPlay.Backend.Repository.Domain.Context.Interfaces;
using CentralPlay.Backend.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CentralPlay.Backend.Repository.Repositories
{
    public class UserRepository : CosmosDbRepository<Domain.Entities.User>, IUserRepository
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="factory"></param>
        public UserRepository(ICosmosDbContainerFactory factory) : base(factory)
        {
        }

        #endregion

        /// <summary>
        ///  CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "users";

        /// <summary>
        ///  Generate Id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(Domain.Entities.User entity) => entity.Id;

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(Domain.Entities.User entity) => new PartitionKey(entity.Id);


    }
}
