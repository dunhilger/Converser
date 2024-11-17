using System.Text.Json.Serialization;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonRoot
    {
        [JsonPropertyName("data")]
        public JsonDataList Data { get; set; }
    }
}
