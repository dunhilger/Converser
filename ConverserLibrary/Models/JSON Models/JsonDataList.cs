using ConverserLibrary.Dto;
using System.Text.Json.Serialization;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonDataList
    {
        [JsonPropertyName("list")]
        public List<JsonCity> List { get; set; }
    }
}
