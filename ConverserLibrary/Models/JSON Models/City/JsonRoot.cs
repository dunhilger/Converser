using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonRoot
    {
        [JsonProperty("data")]
        public JsonCityDataList JsonCityDataList { get; set; }
    }
}
