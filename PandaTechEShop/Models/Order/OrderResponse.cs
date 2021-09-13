using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Order
{
    public class OrderResponse
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }
    }
}
