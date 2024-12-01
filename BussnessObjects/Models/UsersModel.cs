using Newtonsoft.Json;

namespace QAHackathon.BussinesObjects.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UsersModel
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("users")]
        public ICollection<UserModel> Users { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class Meta
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    //[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    //public partial class UsersData
    //{
    //    private LoggingBL loggingBL = LoggingBL.Instance;

    //    [JsonProperty("avatar_url")]
    //    public int AvatarUrl { get; set; }

    //    [JsonProperty("email")]
    //    public string Email { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("nickname")]
    //    public string Nickname { get; set; }

    //    [JsonProperty("uuid")]
    //    public string Uuid { get; set; }

    //    public void Show()
    //    {
    //        loggingBL.Info($"User: " +
    //            $"Avatar URL:{AvatarUrl}," +
    //            $"Email:{Email}," +
    //            $"Name:{Name}," +
    //            $"Nickname:{Nickname}," +
    //            $"UUID:{Uuid}");
    //    }
    //}
}
