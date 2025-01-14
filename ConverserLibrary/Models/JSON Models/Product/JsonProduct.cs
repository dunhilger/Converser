using ConverserLibrary.Models.JSON_Models.Product;
using Newtonsoft.Json;

namespace ConverserLibrary.Models.JSON_Models
{
    public class JsonProduct
    {
        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("constructorComposition")]
        public int ConstructorComposition { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("availableForOrder")]
        public bool AvailableForOrder { get; set; }

        [JsonProperty("category")]
        public JsonProductCategory Category { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; } 

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("itemsCount")]
        public int ItemsCount { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("productCategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("excise")]
        public int Excise { get; set; }

        [JsonProperty("images")]
        public string[] Images { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
