using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonCity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("idMenu")]
        public string IdMenu { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location")]
        public JsonCityLocation Location { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("onlyOnlinePaymentDays")]
        public object OnlyOnlinePaymentDays { get; set; }
    }
}
