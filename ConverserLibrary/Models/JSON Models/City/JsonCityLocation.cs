using Newtonsoft.Json;

namespace ConverserLibrary.Models
{
    public class JsonCityLocation
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
