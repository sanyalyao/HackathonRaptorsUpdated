namespace QAHackathon.Core.RunSettings
{
    public class BaseSettings
    {
        protected static string BaseEndpoint {  get; private set; }
        protected static SetUpSettings Settings { get; private set; }

        private enum Environments
        {
            Release,
            Dev
        }

        protected BaseSettings()
        {
            Settings = new();

            if (Settings.environment == Environments.Release.ToString().ToLower())
            {
                BaseEndpoint = Settings.releaseEndpoint;
            }
            else
            {
                BaseEndpoint = Settings.devEndpoint;
            }
        }
    }
}