using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CitiesResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("jsonRoot")]
        public JsonRoot JsonRoot { get; set; }
    }
}
