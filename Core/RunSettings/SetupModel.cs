using Newtonsoft.Json;

namespace QAHackathon.Core.RunSettings
{
    public class SetupModel
    {
        [JsonProperty("setupEnvironmentRelease")]
        public Setup SetupEnvironmentRelease { get; set; }

        [JsonProperty("SetupEnvironmentDev")]
        public Setup SetupEnvironmentDev { get; set; }
    }

    public class Setup
    {
        [JsonProperty("setup")]
        public bool IsSetup { get; set; }
    }
}