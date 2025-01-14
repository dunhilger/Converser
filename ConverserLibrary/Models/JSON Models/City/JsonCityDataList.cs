using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonCityDataList
    {
        [JsonProperty("list")]
        public List<JsonCity> CityList { get; set; }
    }
}
