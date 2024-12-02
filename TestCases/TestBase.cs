using Allure.Net.Commons;
using Allure.NUnit;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using QAHackathon.BussinesObjects.Services;
using QAHackathon.BussnessObjects.Services;
using QAHackathon.Core.LoggingLogic;
using QAHackathon.Core.RunSettings;

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

        public static class Api
        {
            public static class Users
            {
                public static IEnumerable<TestCaseData> GetAllUsersData
                {
                    get
                    {
                        foreach (var data in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(data);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> CreateUserData
                {
                    get
                    {
                        foreach (var data in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(data);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> UpdateUserData
                {
                    get
                    {
                        foreach (var data in inputTestData.Api.Users.UpdateUser)
                        {
                            yield return new TestCaseData(data);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> GetUpdateUsers
                {
                    get
                    {
                        foreach (var getAllUsers in inputTestData.Api.Users.GetAllUsers)
                        {
                            foreach (var updateUser in inputTestData.Api.Users.UpdateUser)
                            {
                                yield return new TestCaseData(getAllUsers, updateUser);
                            }
                        }
                    }
                }

                public static IEnumerable<TestCaseData> GetCreateUsers
                {
                    get
                    {
                        foreach (var getAllUsers in inputTestData.Api.Users.GetAllUsers)
                        {
                            foreach (var createUser in inputTestData.Api.Users.CreateUser)
                            {
                                yield return new TestCaseData(getAllUsers, createUser);
                            }
                        }
                    }
                }

                public static IEnumerable<TestCaseData> GetUsersUuidData
                {
                    get
                    {
                        foreach (var getAllUsers in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(getAllUsers, inputTestData.Api.Users.GetUserByUuid);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> CreateDeleteGetByUuidData
                {
                    get
                    {
                        foreach (var createUser in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(createUser, inputTestData.Api.Users.DeleteUser, inputTestData.Api.Users.GetUserByUuid);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> GetAllGetByUuidDeleteData
                {
                    get
                    {
                        foreach (var getUsers in inputTestData.Api.Users.GetAllUsers)
                        {
                            yield return new TestCaseData(getUsers, inputTestData.Api.Users.GetUserByUuid, inputTestData.Api.Users.DeleteUser);
                        }
                    }
                }

                public static IEnumerable<TestCaseData> CreateGetByPassEmailData
                {
                    get
                    {
                        foreach (var createUser in inputTestData.Api.Users.CreateUser)
                        {
                            yield return new TestCaseData(createUser, inputTestData.Api.Users.GetUserByPassAndEmail);
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