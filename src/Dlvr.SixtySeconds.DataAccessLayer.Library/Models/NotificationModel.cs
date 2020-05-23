using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class NotificationModel
    {
        [JsonProperty("to")]
        public string[] To { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }

        [JsonProperty("badge")]
        public string Badge { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("Data")]
        public JObject Data { get; set; }
    }
}
