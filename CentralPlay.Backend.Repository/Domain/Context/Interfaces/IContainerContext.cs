using CentralPlay.Backend.Repository.Domain.Entities.Base;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralPlay.Backend.Repository.Domain.Context.Interfaces
{
    public interface IContainerContext<T> where T : BaseEntity
    {
        /// <summary>
        /// Cosmos Container Name
        /// </summary>
        string ContainerName { get; }

        /// <summary>
        /// Generates the entity Id
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>ID</returns>
        string GenerateId(T entity);

        /// <summary>
        /// Resolves the partition key
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Partition Key</returns>
        PartitionKey ResolvePartitionKey(T entity);
    }
}
