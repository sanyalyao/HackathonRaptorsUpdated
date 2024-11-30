using NUnit.Framework;

namespace QAHackathon.Core.RunSettings
{
    public class SetUpSettings
    {
        public string releaseEndpoint;
        public string devEndpoint;
        public string email;
        public string environment;

        public SetUpSettings()
        {
            releaseEndpoint = TestContext.Parameters.Get("ReleaseEndpoint");
            devEndpoint = TestContext.Parameters.Get("DevEndpoint");
            email = TestContext.Parameters.Get("Email");
            environment = TestContext.Parameters.Get("Environment");
        }
    }
}