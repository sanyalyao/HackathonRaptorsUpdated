using Newtonsoft.Json;
using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.BussinesObjects.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class UserModel
    {
        private LoggingBL loggingBL = LoggingBL.Instance;

        [JsonProperty("avatar_url")]
        public int AvatarUrl { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        public void Show()
        {
            loggingBL.Info($"User: " +
                $"Avatar URL:{AvatarUrl}," +
                $"Email:{Email}," +
                $"Name:{Name}," +
                $"Nickname:{Nickname}," +
                $"UUID:{Uuid}");
        }
    }
}