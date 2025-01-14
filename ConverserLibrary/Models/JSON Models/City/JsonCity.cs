using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonCity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idMenu")]
        public string IdMenu { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public JsonCityLocation Location { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("onlyOnlinePaymentDays")]
        public object OnlyOnlinePaymentDays { get; set; }

        [JsonProperty("isNewLoyaltyProgram")]
        public bool IsNewLoyaltyProgram { get; set; }
    }
}
