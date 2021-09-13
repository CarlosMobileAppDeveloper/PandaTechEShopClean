using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.ShoppingCart
{
    public class CartSubTotal
    {
        [JsonProperty("SubTotal")]
        public double SubTotal { get; set; }
    }
}
