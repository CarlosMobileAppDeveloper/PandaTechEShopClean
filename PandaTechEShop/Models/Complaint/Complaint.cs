using System;
using Newtonsoft.Json;

namespace PandaTechEShop.Models.Complaint
{
    public class Complaint
    {
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
