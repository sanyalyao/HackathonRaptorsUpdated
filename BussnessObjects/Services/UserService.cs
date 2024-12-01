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
        private readonly string taskIdUpdateUserV1 = "api-4";
        private readonly string taskIdUpdateUserV2 = "api-24";

        private static string endpointAllUsers = baseEndpoint + "users";
        private string endpointUserByUuid = endpointAllUsers + "/{uuid}";

        [AllureStep("Get list of users")]
        public UsersModel GetUsers()
        {
            loggingBL.Info("Getting list of users");

            var request = new RestRequest(endpointAllUsers);

            apiClient.AddOrUpdateXTaskId(request, taskIdGetAllUsersV1);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UsersModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public UserModel GetUserByUuid(string uuid)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var request = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskIdGetUserByUuid);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UserModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public void GetUserByUuidWithoutException(string uuid)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var request = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskIdGetUserByUuid);

            var response = apiClient.ExecuteWithoutException(request);
            var error = JsonConvert.DeserializeObject<ErrorModel>(response.Content);

            AssertBL.AreEqual((int)StatusCodes.NotFound, (int)response.StatusCode);

            loggingBL.Info($"Status code: {(int)response.StatusCode} - {response.StatusCode}");
            loggingBL.Info("User does not exist");
        }

        [AllureStep("Create a new user")]
        public UserModel CreateNewUser(User newUser)
        {
            loggingBL.Info($"Creating a new user");

            var request = new RestRequest(endpointAllUsers, Method.Post);

            apiClient.AddOrUpdateXTaskId(request, taskIdCreateUser);
            apiClient.AddBody(request, newUser);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UserModel>(response.Content);
        }

        [AllureStep("Delete user")]
        public void DeleteUser(string uuid)
        {
            loggingBL.Info($"Deleting user with UUID - {uuid}");

            var request = new RestRequest(endpointUserByUuid, Method.Delete).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskIdDeleteUser);

            var response = apiClient.ExecuteWithoutException(request);

            AssertBL.AreEqual((int)StatusCodes.NoContent, (int)response.StatusCode);

            loggingBL.Info($"User \"{uuid}\" is deleted");
        }

        [AllureStep("Update user")]
        public UserModel UpdateUser(UserModel currentUser, Dictionary<string,string> sameUserWithChanges)
        {
            return Step($"Updating user with UUID - {currentUser.Uuid}", () => 
            {
                var request = new RestRequest(endpointUserByUuid, Method.Patch).AddUrlSegment("uuid", currentUser.Uuid);

                apiClient.AddOrUpdateXTaskId(request, taskIdUpdateUserV1);
                apiClient.AddBody(request, sameUserWithChanges);

                var response = apiClient.Execute(request);

                return JsonConvert.DeserializeObject<UserModel>(response.Content);
            });
        }
    }
}