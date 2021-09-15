using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Category
{
    public class CategoryInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string FullImageUrl => AppSettings.ApiUrl + "/" + ImageUrl;
    }
}
