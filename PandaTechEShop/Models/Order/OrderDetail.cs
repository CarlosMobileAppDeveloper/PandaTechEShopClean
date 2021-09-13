using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Order
{
    public class OrderDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("subTotal")]
        public double SubTotal { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productPrice")]
        public double ProductPrice { get; set; }
    }
}
