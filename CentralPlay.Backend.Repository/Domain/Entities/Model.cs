using CentralPlay.Backend.Repository.Domain.Entities.Base;
using CentralPlay.Backend.Repository.Enums;
using Newtonsoft.Json;

namespace CentralPlay.Backend.Repository.Domain.Entities
{
    public class Model: BaseEntity
    {
        [JsonProperty(PropertyName = "userId")]
        public virtual string UserId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }

        [JsonProperty(PropertyName = "fileSize")]
        public virtual long FileSize { get; set; }

        [JsonProperty(PropertyName = "fileType")]
        public virtual string FileType { get; set; }

        [JsonProperty(PropertyName = "validFile")]
        public virtual ModelValidationEnum ValidFile { get; set; }

    }
}
