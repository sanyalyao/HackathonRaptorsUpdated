using QAHackathon.BussinesObjects.Models;

namespace QAHackathon.BussnessObjects.Utils
{
    public static class UserGenerator
    {
        public enum Lengths
        {
            Min,
            Max
        }

        public enum UserData
        {
            Email,
            Name,
            Nickname,
            Password
        }

        public static User GetNewUser()
        {
            var user = new User(UtilsBL.GetCorrectEmail(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        public static User GetNewUserWithIncorrectEmailFormat()
        {
            var user = new User(UtilsBL.GetIncorrectEmail(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        public static User GetUserIncorrectEmailLength(Lengths incorrectLength)
        {
            switch (incorrectLength)
            { 
                case Lengths.Max:
                    {
                        return GetUserIncorrectMaxEmailLength();
                    }
                default:
                    {
                        return GetUserIncorrectMinEmailLength();
                    }
            }
        }

        public static User GetNewUserWithIncorrectName()
        {
            var user = new User(UtilsBL.GetCorrectEmail(),
                UtilsBL.GetIncorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        public static User GetNewUserWithIncorrectNickname()
        {
            var user = new User(UtilsBL.GetCorrectEmail(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetIncorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        public static User GetNewUserWithIncorrectPassword()
        {
            var user = new User(UtilsBL.GetCorrectEmail(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetIncorrectPassword());

            return user;
        }

        #region Private methods

        private static User GetUserIncorrectMaxEmailLength()
        {
            var user = new User(UtilsBL.GetIncorrectMaxEmailLength(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        private static User GetUserIncorrectMinEmailLength()
        {
            var user = new User(UtilsBL.GetIncorrectMinEmailLength(),
                UtilsBL.GetCorrectName(),
                UtilsBL.GetCorrectNickname(),
                UtilsBL.GetCorrectPassword());

            return user;
        }

        #endregion
    }
}