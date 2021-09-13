using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Order
{
    public class Order
    {
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("OrderTotal")]
        public int OrderTotal { get; set; }

        [JsonProperty("UserId")]
        public int UserId { get; set; }
    }
}
