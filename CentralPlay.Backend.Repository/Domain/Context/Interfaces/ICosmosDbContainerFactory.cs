namespace CentralPlay.Backend.Repository.Domain.Context.Interfaces
{
    public interface ICosmosDbContainerFactory
    {
        /// <summary>
        /// Returns a CosmosDbContainer wrapper
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        ICosmosDbContainer GetContainer(string containerName);
    }
}
