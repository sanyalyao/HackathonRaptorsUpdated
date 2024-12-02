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
                        foreach (var data in inputTestData.Api.Users.UpdateUser)
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
            }
        }

        private static InputTestData GetInputTestData()
        {
            var config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/Configs", "inputTestData.json"));

            return JsonConvert.DeserializeObject<InputTestData>(config);
        }
    }
}