using QAHackathon.Core.RestCore;
using Allure.NUnit.Attributes;
using RestSharp;
using static QAHackathon.Core.BussnessLogic.StepsBL;

namespace QAHackathon.BussnessObjects.Services
{
    public class SetupService : BaseService
    {
        private static string endpointSetup = baseEndpoint + "setup";

        public SetupService(BaseApiClient apiClient) : base(apiClient) { }

        [AllureStep("Sets up API, cleans DB and populates with sample data")]
        public void Setup()
        {
            Step("Setting up API, cleaning DB and populating with sample data", () =>
            {
                var request = new RestRequest(endpointSetup, Method.Post);

                apiClient.Execute(request);
            });
        }

        [AllureStep("Returns current statuses")]
        public void GetStatus()
        {
        }
    }
}
