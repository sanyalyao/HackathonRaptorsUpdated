using QAHackathon.BussinesObjects.Models;
using Faker;
using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.BussnessObjects.Utils
{
    public static class UserGenerator
    {
        private static LoggingBL loggingBL = LoggingBL.Instance;

        public static User GetNewUser()
        {
            var email = Internet.Email();
            var name = Name.First();
            var nickname = Internet.UserName();
            var password = string.Join("",Lorem.Words(RandomNumber.Next(20)));
            var user = new User(email, name, nickname, password);

            loggingBL.Info($"Generated a new user. " +
                $"Email:{user.Email}," +
                $"Name:{user.Name}," +
                $"Nickname:{user.Nickname}," +
                $"Password:{user.Password}");

            return user;
        }
    }
}