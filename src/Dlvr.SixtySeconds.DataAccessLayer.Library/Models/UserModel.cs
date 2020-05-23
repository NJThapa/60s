using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class UserModel
    {

        [JsonProperty("given_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("family_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ReportsTo { get; set; }
        public string PictureUrl { get; set; }
        public string CompanyName { get; set; }        
    }

    public class UserMetadata
    {
        [JsonProperty("reports_to")]
        public string ReportsTo { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }
    }
}
