using QAHackathon.BussinesObjects.Models;
using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.BussnessObjects.Utils
{
    public static class UserGenerator
    {
        private static LoggingBL loggingBL = LoggingBL.Instance;

        public static User GetNewUser()
        {
            var user = new User(UtilsBL.GenerateEmail(),
                UtilsBL.GenerateName(),
                UtilsBL.GenerateNickname(),
                UtilsBL.GeneratePassword());

            loggingBL.Info($"Generated a new user. " +
                $"Email:{user.Email}, " +
                $"Name:{user.Name}, " +
                $"Nickname:{user.Nickname}, " +
                $"Password:{user.Password}");

            return user;
        }
    }
}