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

        private static string endpointAllUsers = baseEndpoint + "users";
        private static string endpointAllUsersWithLimit = endpointAllUsers;
        private string endpointUserByUuid = endpointAllUsers + "/{uuid}";
        private string endpointUserLogin = endpointAllUsers + "/login";        

        [AllureStep("Get list of users")]
        public UsersModel GetUsers(string taskId)
        {
            loggingBL.Info("Getting list of users");

            var request = new RestRequest(endpointAllUsers);

            apiClient.AddOrUpdateXTaskId(request, taskId);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UsersModel>(response.Content);
        }

        [AllureStep("Get list of users with limit")]
        public UsersModel GetUsers(int limit, string taskId)
        {
            loggingBL.Info($"Getting list of users with limit = {limit}");

            var request = new RestRequest(endpointAllUsersWithLimit).AddParameter("limit", limit);

            apiClient.AddOrUpdateXTaskId(request, taskId);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UsersModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public UserModel GetUserByUuid(string uuid, string taskId)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var request = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskId);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UserModel>(response.Content);
        }

        [AllureStep("Get a user by uuid")]
        public void GetUserByUuidWithoutException(string uuid, string taskId)
        {
            loggingBL.Info($"Getting a user by uuid - {uuid}");

            var request = new RestRequest(endpointUserByUuid).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskId);

            var response = apiClient.ExecuteWithoutException(request);
            var error = new ErrorModel().GetError(response);

            AssertBL.AreEqual((int)StatusCodes.NotFound, (int)response.StatusCode);
            AssertBL.AreEqual(error.Code, (int)response.StatusCode);

            loggingBL.Info($"Status code: {(int)response.StatusCode} - {response.StatusCode}");
            loggingBL.Info(error.Message);
        }

        [AllureStep("Create a new user")]
        public UserModel CreateNewUser(User newUser, string taskId)
        {
            loggingBL.Info("Creating a new user");

            var request = new RestRequest(endpointAllUsers, Method.Post);

            apiClient.AddOrUpdateXTaskId(request, taskId);
            apiClient.AddBody(request, newUser);

            var response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<UserModel>(response.Content);
        }

        [AllureStep("Create a new user")]
        public RestResponse CreateNewUserWithoutException(User newUser, string taskId)
        {
            return Step("Creating a new user", () =>
            {
                var request = new RestRequest(endpointAllUsers, Method.Post);

                apiClient.AddOrUpdateXTaskId(request, taskId);
                apiClient.AddBody(request, newUser);

                var response = apiClient.ExecuteWithoutException(request);

                return response;
            });
        }

        [AllureStep("Delete user")]
        public void DeleteUser(string uuid, string taskId)
        {
            loggingBL.Info($"Deleting user with UUID - {uuid}");

            var request = new RestRequest(endpointUserByUuid, Method.Delete).AddUrlSegment("uuid", uuid);

            apiClient.AddOrUpdateXTaskId(request, taskId);

            var response = apiClient.ExecuteWithoutException(request);

            AssertBL.AreEqual((int)StatusCodes.NoContent, (int)response.StatusCode);

            loggingBL.Info($"User \"{uuid}\" is deleted");
        }

        [AllureStep("Update user")]
        public UserModel UpdateUser(UserModel currentUser, Dictionary<string,string> sameUserWithChanges, string taskId)
        {
            return Step($"Updating user with UUID - {currentUser.Uuid}", () => 
            {
                var request = new RestRequest(endpointUserByUuid, Method.Patch).AddUrlSegment("uuid", currentUser.Uuid);

                apiClient.AddOrUpdateXTaskId(request, taskId);
                apiClient.AddBody(request, sameUserWithChanges);

                var response = apiClient.Execute(request);

                return JsonConvert.DeserializeObject<UserModel>(response.Content);
            });
        }

        [AllureStep("Update user")]
        public RestResponse UpdateUserWithoutException(UserModel currentUser, Dictionary<string, string> sameUserWithChanges, string taskId)
        {
            return Step($"Updating user with UUID - {currentUser.Uuid}", () =>
            {
                var request = new RestRequest(endpointUserByUuid, Method.Patch).AddUrlSegment("uuid", currentUser.Uuid);

                apiClient.AddOrUpdateXTaskId(request, taskId);
                apiClient.AddBody(request, sameUserWithChanges);

                var response = apiClient.ExecuteWithoutException(request);

                return response;
            });
        }

        [AllureStep("Get a user by password and Email")]
        public UserModel GetUserByPasswordAndEmail(User user, string taskId)
        {
            return Step($"Getting the user with Email: {user.Email} and Password: {user.Password}", () => 
            {
                var request = new RestRequest(endpointUserLogin, Method.Post);
                var data = new Dictionary<string, string>() 
                {
                    { "email", user.Email},
                    { "password", user.Password}
                };

                apiClient.AddOrUpdateXTaskId(request, taskId);
                apiClient.AddBody(request, data);

                var response = apiClient.Execute(request);

                return JsonConvert.DeserializeObject<UserModel>(response.Content);
            });
        }

        [AllureStep("Get a user by nickname and password")]
        public RestResponse GetUserByNicknameAndPassword(User user, string taskId)
        {
            return Step($"Getting the user with Nickname: {user.Nickname} and Password: {user.Password}", () => 
            {
                var request = new RestRequest(endpointUserLogin, Method.Post);
                var data = new Dictionary<string, string>()
                {
                    { "nickname", user.Nickname},
                    { "password", user.Password}
                };

                apiClient.AddOrUpdateXTaskId(request, taskId);
                apiClient.AddBody(request, data);

                var response = apiClient.ExecuteWithoutException(request);

                return response;
            });
        }
    }
}