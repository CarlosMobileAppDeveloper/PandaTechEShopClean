using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.ShoppingCart
{
    public class ShoppingCartItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
