using Newtonsoft.Json;

namespace CentralPlay.Backend.FunctionApp.Models
{
    public class FileValidationModel
    {
        [JsonProperty(PropertyName = "modelId")]
        public string ModelId { get; set; }

        [JsonProperty(PropertyName = "isFileValid")]
        public bool IsFileValid { get; set; }
    }
}
