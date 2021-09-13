using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Order
{
    public class OrderByUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("orderTotal")]
        public double OrderTotal { get; set; }

        [JsonProperty("orderPlaced")]
        public DateTime OrderPlaced { get; set; }
    }
}
