namespace QAHackathon.Core.RunSettings
{
    public class BaseSettings
    {
        private enum Environments
        {
            Release,
            Dev
        }

        protected static SetUpSettings Settings;
        protected static string baseEndpoint;

        protected BaseSettings()
        {
            Settings = new();

            if (Settings.environment == Environments.Release.ToString().ToLower())
            {
                baseEndpoint = Settings.releaseEndpoint;
            }
            else
            {
                baseEndpoint = Settings.devEndpoint;
            }
        }
    }
}
