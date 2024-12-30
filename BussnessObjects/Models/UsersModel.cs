using Newtonsoft.Json;

namespace QAHackathon.BussinesObjects.Models
{
    /// <summary>
    /// Users structure from response
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UsersModel
    {
        [JsonProperty("meta")]
        public Meta? Meta { get; set; }

        [JsonProperty("users")]
        public ICollection<UserModel>? Users { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class Meta
    {
        [JsonProperty("total")]
        public int? Total { get; set; }
    }
}
