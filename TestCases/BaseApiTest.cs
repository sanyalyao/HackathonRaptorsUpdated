using NUnit.Framework;
using QAHackathon.Core.RestCore;
using QAHackathon.Core.RunSettings;

namespace QAHackathon.TestCases
{
    public class BaseApiTest : BaseSettings
    {
        protected BaseApiClient apiClient;

        [OneTimeSetUp]
        public void Initial()
        {
            apiClient = new BaseApiClient(baseEndpoint);
        }
    }
}
