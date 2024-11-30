using Newtonsoft.Json;

namespace QAHackathon.BussnessObjects.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
