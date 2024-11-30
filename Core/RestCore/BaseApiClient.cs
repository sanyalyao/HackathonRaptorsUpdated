using QAHackathon.BussinesObjects.Models;
using QAHackathon.Core.BussnessLogic;
using QAHackathon.Core.LoggingLogic;
using QAHackathon.Core.RunSettings;
using RestSharp;

namespace QAHackathon.Core.RestCore
{
    public class BaseApiClient : BaseSettings
    {
        private RestClient restClient;
        private LoggingBL loggingBL;

        public BaseApiClient(string url)
        {
            var option = new RestClientOptions(url)
            {
                MaxTimeout = 10000,
                ThrowOnAnyError = true,
            };

            restClient = new(url);
            loggingBL = LoggingBL.Instance;
            restClient.AddDefaultHeader("Content-Type", "application/json");
            restClient.AddDefaultHeader("Authorization", $"Bearer qahack2024:{Settings.email}");
        }

        public string GetAbsoluteUri(RestRequest restRequest) => restClient.BuildUri(restRequest).AbsoluteUri;

        public RestResponse Execute(RestRequest request)
        {
            loggingBL.Info("Processing request");

            var response = restClient.Execute(request);

            loggingBL.Trace(response.Content);

            CheckSuccessResponse(response);

            return response;
        }

        public RestResponse ExecuteWithoutException(RestRequest request)
        {
            loggingBL.Info("Processing request");

            var response = restClient.Execute(request);

            if (!string.IsNullOrEmpty(response.Content))
            {
                loggingBL.Trace(response.Content);
            }

            return response;
        }

        public void AddOrUpdateXTaskId(RestRequest request, string xTaskId)
        {
            AddOrUpdateHeader(request, "X-Task-Id", xTaskId);
        }

        public void AddBody(RestRequest request, User user)
        {
            request.AddBody(user);
        }

        public void AddOrUpdateHeader(RestRequest request, string name, string value)
        {
            request.AddOrUpdateHeader(name, value);
        }

        private void CheckSuccessResponse(RestResponse response)
        {
            loggingBL.Info("Checking response");
            AssertBL.IsTrue(response.IsSuccessful);
            AssertBL.IsNotEmpty(response.Content);
            AssertBL.IsNotNull(response);

            loggingBL.Info("Response is not empty");
            loggingBL.Info($"Success Status Code: {(int)response.StatusCode} - {response.StatusCode}");
        }
    }
}