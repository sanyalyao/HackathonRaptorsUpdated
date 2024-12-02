using Allure.Net.Commons;
using Allure.NUnit;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using QAHackathon.BussinesObjects.Services;
using QAHackathon.BussnessObjects.Services;
using QAHackathon.Core.LoggingLogic;
using QAHackathon.Core.RunSettings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QAHackathon.TestCases
{
    [AllureNUnit]
    public class TestBase : BaseApiTest
    {
        protected AllureLifecycle allure;
        protected UserService userService;
        protected LoggingBL loggingBL;
        protected SetupService setupService;
        private static InputTestData inputTestData = GetInputTestData();

        [OneTimeSetUp]
        public void InitialService()
        {
            userService = new (apiClient);
            setupService = new (apiClient);
            loggingBL = LoggingBL.Instance;

            LogManager.Setup().LoadConfigurationFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/Configs", "NLog.config"));
        }

        [SetUp]
        public void SetUp()
        {
            allure = AllureLifecycle.Instance;
            loggingBL.GenerateGuid();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            LogManager.Flush();
            LogManager.Shutdown();            
        }

        public enum ApiTaskId
        {
            GetAll,
            GetByUuid,
            Delete,
            Create,
            Update,
            GetByPassEmail
        }

        public static class Api
        {
            public static class Users
            {
                public static IEnumerable<TestCaseData> GetTestData(IEnumerable<ApiTaskId> apiTaskIds)
                {
                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.GetAll , ApiTaskId.Update }))
                    {
                        foreach (var getAll in inputTestData.Api.Users.GetAllUsers)
                        {
                            foreach (var update in inputTestData.Api.Users.UpdateUser)
                            {
                                yield return new TestCaseData(getAll, update);
                            }
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.GetAll, ApiTaskId.GetByUuid }))
                    {
                        foreach (var getAll in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(getAll, inputTestData.Api.Users.GetUserByUuid);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.GetAll, ApiTaskId.Create }))
                    {
                        foreach (var getAll in inputTestData.Api.Users.GetAllUsers)
                        {
                            foreach (var create in inputTestData.Api.Users.CreateUser)
                            {
                                yield return new TestCaseData(getAll, create);
                            }
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.Create, ApiTaskId.GetByPassEmail }))
                    {
                        foreach (var createUser in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(createUser, inputTestData.Api.Users.GetUserByPassAndEmail);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.Create, ApiTaskId.Delete, ApiTaskId.GetByUuid }))
                    {
                        foreach (var createUser in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(createUser, inputTestData.Api.Users.DeleteUser, inputTestData.Api.Users.GetUserByUuid);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.GetAll, ApiTaskId.GetByUuid, ApiTaskId.Delete }))

                    {
                        foreach (var getUsers in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(getUsers, inputTestData.Api.Users.GetUserByUuid, inputTestData.Api.Users.DeleteUser);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.GetAll }))
                    {
                        foreach (var data in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(data);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.Create }))
                    {
                        foreach (var data in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(data);
                        }
                    }

                    if (apiTaskIds.SequenceEqual(new ApiTaskId[] { ApiTaskId.Update }))
                    {
                        foreach (var data in inputTestData.Api.Users.UpdateUser)
                        {
                            yield return new TestCaseData(data);
                        }
                    }
                }
            }
        }

        private static InputTestData GetInputTestData()
        {
            var config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/Configs", "inputTestData.json"));

            return JsonConvert.DeserializeObject<InputTestData>(config);
        }
    }
}