using Newtonsoft.Json;

namespace CentralPlay.Backend.FunctionApp.Models
{
    public class BlobFileTriggerModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fileLength")]
        public long FileLength { get; set; }

        [JsonProperty(PropertyName = "filePath")]
        public string FilePath { get; set; }
    }
}
