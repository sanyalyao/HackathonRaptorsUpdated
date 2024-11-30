using Allure.Net.Commons;
using Allure.NUnit;
using NLog;
using NUnit.Framework;
using QAHackathon.BussinesObjects.Services;
using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.TestCases
{
    [AllureNUnit]
    public class TestBase : BaseApiTest
    {
        protected AllureLifecycle allure;
        protected UserService userService;
        protected LoggingBL loggingBL;

        [OneTimeSetUp]
        public void InitialService()
        {
            userService = new (apiClient);
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
    }
}