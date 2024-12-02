using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NUnit.Framework;

namespace QAHackathon.TestCases
{
    [TestFixture]
    public class SetupTests : TestBase
    {
        [Test]
        [Description("Sets up API, cleans DB and populates with sample data")]
        [Category("API")]
        [Category("Setup")]
        [AllureSeverity(SeverityLevel.critical)]
        public void SetupEnvironment()
        {
            var setup = Settings.GetSetupSettings();

            if (baseEndpoint == Settings.releaseEndpoint && !setup.SetupEnvironmentRelease.IsSetup)
            {
                setupService.Setup();

                setup.SetupEnvironmentRelease.IsSetup = true;

                Settings.ChangeSetupConfig(setup);
                loggingBL.Info("Release environment is ready");
            }
            else if (baseEndpoint == Settings.devEndpoint && !setup.SetupEnvironmentDev.IsSetup)
            {
                setupService.Setup();

                setup.SetupEnvironmentDev.IsSetup = true;

                Settings.ChangeSetupConfig(setup);
                loggingBL.Info("Dev environment is ready");
            }
            else
            {
                loggingBL.Info("Environment is already set up. Nothing was changed");
            }
        }
    }
}