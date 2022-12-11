using CentralPlay.Backend.Repository.Domain.Entities.Base;
using Newtonsoft.Json;

namespace CentralPlay.Backend.Repository.Domain.Entities
{
    public class User: BaseEntity
    {
        [JsonProperty(PropertyName = "amountOfModels")]
        public virtual int AmountOfModels { get; set; }
    }
}
