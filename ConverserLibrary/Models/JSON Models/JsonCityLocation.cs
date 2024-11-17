using System.Text.Json.Serialization;

namespace ConverserLibrary.Models
{
    public class JsonCityLocation
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
