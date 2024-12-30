using Newtonsoft.Json;
using RestSharp;

namespace QAHackathon.BussnessObjects.Models
{
    /// <summary>
    /// Error structure from response
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorModel
    {
        [JsonProperty("code")]
        public int? Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        public ErrorModel() { }

        /// <summary>
        /// Get error structure
        /// </summary>
        /// <param name="response">Response</param>
        /// <returns></returns>
        public ErrorModel GetError(RestResponse response) => JsonConvert.DeserializeObject<ErrorModel>(response.Content);
    }
}