using Newtonsoft.Json;

namespace CentralPlay.Backend.Repository.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
    }
}
