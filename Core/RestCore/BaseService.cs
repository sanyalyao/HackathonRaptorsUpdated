using QAHackathon.Core.LoggingLogic;
using QAHackathon.Core.RunSettings;

namespace QAHackathon.Core.RestCore
{
    public class BaseService : BaseSettings
    {
        protected BaseApiClient apiClient;
        protected LoggingBL loggingBL;

        public BaseService(BaseApiClient apiClient)
        {
            this.apiClient = apiClient;
            loggingBL = LoggingBL.Instance;
        }
    }
}