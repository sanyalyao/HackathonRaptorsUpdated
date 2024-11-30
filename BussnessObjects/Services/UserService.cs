using Allure.NUnit.Attributes;
using Newtonsoft.Json;
using QAHackathon.BussinesObjects.Models;
using QAHackathon.BussnessObjects.Models;
using QAHackathon.Core.BussnessLogic;
using QAHackathon.Core.RestCore;
using RestSharp;
using static QAHackathon.Core.BussnessLogic.StepsBL;
using static QAHackathon.Core.RestCore.HttpStatusCodes;

namespace QAHackathon.BussinesObjects.Services
{
    public class UserService : BaseService
    {
        public UserService(BaseApiClient apiClient) : base(apiClient) { }

        private readonly string taskIdGetAllUsersV1 = "api-6";
        private readonly string taskIdGetAllUsersV2 = "api-21";
        private readonly string taskIdGetUserByUuid = "api-23";
        private readonly string taskIdCreateUser = "api-3";
        private readonly string taskIdDeleteUser = "api-1";

        private static string endpointAllUsers = baseEndpoint + "users";
        private string endpointUserByUuid = endpointAllUsers + "/{uuid}";

        [AllureStep("Get list of users")]
        public UsersModel GetUsers()
        {
            loggingBL.Info("Getting list of users");

            var resuest = new RestRequest(endpointAllUsers);

            apiClient.AddOrUpdateXTaskId(resuest, taskIdGetAllUsersV1);
            loggingBL.Info($"Endpoint - {resuest.Resource}");

            var response = apiClient.Execute(resuest);

            return JsonConvert.DeserializeObject<UsersModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public UserModel GetUserByUuid(string uuid)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var resuest = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(resuest, taskIdGetUserByUuid);
            loggingBL.Info($"Endpoint - {apiClient.GetAbsoluteUri(resuest)}");

            var response = apiClient.Execute(resuest);

            return JsonConvert.DeserializeObject<UserModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public void GetUserByUuidWithoutException(string uuid)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var resuest = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(resuest, taskIdGetUserByUuid);
            loggingBL.Info($"Endpoint - {apiClient.GetAbsoluteUri(resuest)}");

            var response = apiClient.ExecuteWithoutException(resuest);
            var error = JsonConvert.DeserializeObject<ErrorModel>(response.Content);

            AssertBL.AreEqual((int)StatusCodes.NotFound, (int)response.StatusCode);

            loggingBL.Info($"Status code: {(int)response.StatusCode} - {response.StatusCode}");
            loggingBL.Info("User does not exist");
        }

        [AllureStep("Create a new user")]
        public UserModel CreateNewUser(User newUser)
        {
            loggingBL.Info($"Creating a new user");

            var resuest = new RestRequest(endpointAllUsers, Method.Post);

            apiClient.AddOrUpdateXTaskId(resuest, taskIdCreateUser);
            apiClient.AddBody(resuest, newUser);
            loggingBL.Info($"Endpoint - {apiClient.GetAbsoluteUri(resuest)}");

            var response = apiClient.Execute(resuest);
            var createdUser = JsonConvert.DeserializeObject<UserModel>(response.Content);

            Step("Checking the new user exists", () => 
            {
                var user = GetUserByUuid(createdUser.Uuid);

                CompareUsers(newUser, user);

                loggingBL.Info("User exists");
            });

            return createdUser;
        }

        [AllureStep("Delete user")]
        public void DeleteUser(string uuid)
        {
            loggingBL.Info($"Deleting user with UUID - {uuid}");

            var resuest = new RestRequest(endpointUserByUuid, Method.Delete).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(resuest, taskIdDeleteUser);
            loggingBL.Info($"Endpoint - {apiClient.GetAbsoluteUri(resuest)}");

            var response = apiClient.ExecuteWithoutException(resuest);

            AssertBL.AreEqual((int)StatusCodes.NoContent, (int)response.StatusCode);

            loggingBL.Info($"User \"{uuid}\" is deleted");
        }

        [AllureStep("Compare users")]
        private void CompareUsers(User expectedUser, UserModel currentUser)
        {
            Step($"Comparing two users. Name: {expectedUser.Name} with Name: {currentUser.Name}", () => 
            {
                AssertBL.AreEqual(expectedUser.Email, currentUser.Email);
                AssertBL.AreEqual(expectedUser.Name, currentUser.Name);
                AssertBL.AreEqual(expectedUser.Nickname, currentUser.Nickname);
            });
        }
    }
}