using NUnit.Framework;
using Newtonsoft.Json;

namespace QAHackathon.Core.RunSettings
{
    public class SetUpSettings
    {
        public string releaseEndpoint;
        public string devEndpoint;
        public string email;
        public string environment;
        public bool setupEnvironmentRelease;
        public bool setupEnvironmentDev;

        public SetUpSettings()
        {
            releaseEndpoint = TestContext.Parameters.Get("ReleaseEndpoint");
            devEndpoint = TestContext.Parameters.Get("DevEndpoint");
            email = TestContext.Parameters.Get("Email");
            environment = TestContext.Parameters.Get("Environment");
        }

        public SetupModel GetSetupSettings()
        {
            var config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/RunSettings", "config.json"));

            return JsonConvert.DeserializeObject<SetupModel>(config);
        }

        public void ChangeSetupConfig(SetupModel setupModel)
        {
            var newConfig = JsonConvert.SerializeObject(setupModel, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/RunSettings", "config.json"), newConfig);
        }
    }
}