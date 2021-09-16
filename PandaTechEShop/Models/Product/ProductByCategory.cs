using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Product
{
    public class ProductByCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string FullImageUrl => AppSettings.ApiUrl + "/" + ImageUrl;
    }
}
