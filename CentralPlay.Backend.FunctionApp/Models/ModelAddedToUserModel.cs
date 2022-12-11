using Newtonsoft.Json;

namespace CentralPlay.Backend.FunctionApp.Models
{
    public class ModelAddedToUserModel
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "amountAdded")]
        public int AmountAdded { get; set; }
    }
}
