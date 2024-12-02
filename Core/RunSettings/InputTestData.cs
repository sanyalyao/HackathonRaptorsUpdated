using Newtonsoft.Json;

namespace QAHackathon.Core.RunSettings
{
    public class InputTestData
    {
        [JsonProperty("api")]
        public Api Api { get; set; }
    }
    public class Api
    {
        [JsonProperty("users")]
        public Users Users { get; set; }
    }

    public class Users
    {
        [JsonProperty("getallusers")]
        public List<string> GetAllUsers { get; set; }

        [JsonProperty("createuser")]
        public List<string> CreateUser { get; set; }

        [JsonProperty("updateuser")]
        public List<string> UpdateUser { get; set; }

        [JsonProperty("getuserbyuuid")]
        public string GetUserByUuid { get; set; }

        [JsonProperty("deleteuser")]
        public string DeleteUser { get; set; }

        [JsonProperty("getuserbypassandemail")]
        public string GetUserByPassAndEmail { get; set; }
    }
}