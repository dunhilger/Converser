using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models.Product
{
    public class JsonProductCategory
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("slugTitle")]
        public string SlugTitle { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
