using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.ShoppingCart
{
    public class AddToCart
    {
        [JsonProperty("Price")]
        public int Price { get; set; }

        [JsonProperty("Qty")]
        public int Qty { get; set; }

        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }
    }
}
