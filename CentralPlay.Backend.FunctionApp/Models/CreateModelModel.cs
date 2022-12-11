using Newtonsoft.Json;

namespace CentralPlay.Backend.FunctionApp.Models
{
    public class CreateModelModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fileType")]
        public string FileType { get; set; }

        [JsonProperty(PropertyName = "fileSize")]
        public long FileSize { get; set; }
    }
}
