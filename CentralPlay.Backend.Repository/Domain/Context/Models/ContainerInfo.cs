namespace CentralPlay.Backend.Repository.Domain.Context.Models
{
    public class ContainerInfo
    {
        /// <summary>
        /// Container Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  Container partition Key
        /// </summary>
        public string PartitionKey { get; set; }
    }
}
