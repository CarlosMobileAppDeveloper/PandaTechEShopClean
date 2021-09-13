using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.ShoppingCart
{
    public class TotalCartItem
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
    }
}
