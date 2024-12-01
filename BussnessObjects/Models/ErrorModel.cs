using Newtonsoft.Json;
using RestSharp;

namespace QAHackathon.BussnessObjects.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorModel() { }

        public ErrorModel GetError(RestResponse response) => JsonConvert.DeserializeObject<ErrorModel>(response.Content);
    }
}
