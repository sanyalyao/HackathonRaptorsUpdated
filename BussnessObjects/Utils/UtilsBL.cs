using Faker;
using Fare;
using QAHackathon.Core.RunSettings;
using System.Text;
using System.Text.RegularExpressions;

namespace QAHackathon.BussnessObjects.Utils
{
    public static class UtilsBL
    {
        private static int passwordLengthMin = TestDataBL.InputTestData.GeneralData.PasswordLengthMin;
        private static int passwordLengthMax = TestDataBL.InputTestData.GeneralData.PasswordLengthMax;

        private static int nicknameLengthMin = TestDataBL.InputTestData.GeneralData.NicknameLengthMin;
        private static int nicknameLengthMax = TestDataBL.InputTestData.GeneralData.NicknameLengthMax;

        private static int nameLengthMin = TestDataBL.InputTestData.GeneralData.NameLengthMin;
        private static int nameLengthMax = TestDataBL.InputTestData.GeneralData.NameLengthMax;

        private static int emailLengthMin = TestDataBL.InputTestData.GeneralData.EmailLengthMin;
        private static int emailLengthMax = TestDataBL.InputTestData.GeneralData.EmailLengthMax;

        private static string allCharacters = TestDataBL.InputTestData.GeneralData.AllCharacters;

        private static string correctEmailPattern = TestDataBL.InputTestData.GeneralData.CorrectEmailPattern;
        private static string incorrectEmailPattern = TestDataBL.InputTestData.GeneralData.IncorrectEmailPattern;

        private static string correctUuidPattern = TestDataBL.InputTestData.GeneralData.CorrectUuidPattern;

        public static string GetCorrectPassword() => GetPassword(passwordLengthMin, passwordLengthMax);

        public static string GetIncorrectPassword() => GetPassword(passwordLengthMax + 1);

        public static string GetCorrectEmail() => GetEmail(correctEmailPattern);

        public static string GetIncorrectEmail() => GetEmail(incorrectEmailPattern);
        public static string GetIncorrectMaxEmailLength() => GetWrongEmailLength(correctEmailPattern, emailLengthMax + 1);
        public static string GetIncorrectMinEmailLength() => GetWrongEmailLength(correctEmailPattern, emailLengthMin - 1);

        public static string GetCorrectName() => GetName(nameLengthMin, nameLengthMax);

        public static string GetIncorrectName() => GetName(nameLengthMax + 1);

        public static string GetCorrectNickname() => GetName(nicknameLengthMin, nicknameLengthMax);

        public static string GetIncorrectNickname() => GetName(nicknameLengthMax + 1);

        public static string GetRandomString(int minLength = 1, int maxLength = 101)
        {
            var lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var allCharacters = lowerCaseLetters + upperCaseLetters;
            Random random = new Random();
            var stringLength = random.Next(minLength, maxLength);
            StringBuilder randomString = new StringBuilder();

            for (int i = 0; i < stringLength; i++)
            {
                int randomIndex = random.Next(0, allCharacters.Length);
                randomString.Append(allCharacters[randomIndex]);
            }

            return randomString.ToString();
        }

        public static string GetRandomUuid()
        {
            var uuid = new Xeger(correctUuidPattern);
            return uuid.Generate().ToLower();
        }

        #region private

        private static string GetEmail(string pattern)
        {
            var i = 0;
            var randomEmail = new Xeger(pattern);
            var currentEmail = randomEmail.Generate();

            while (i < 10)
            {
                if (currentEmail.Length >= emailLengthMin && currentEmail.Length <= emailLengthMax)
                {
                    break;
                }
                else
                {
                    currentEmail = randomEmail.Generate();
                    i++;
                }
            }

            return currentEmail;
        }

        private static string GetWrongEmailLength(string pattern, int emailLength)
        {
            var randomEmail = new Xeger(pattern);
            var currentEmail = string.Empty;

            if (emailLength > emailLengthMax)
            {
                currentEmail = $"{GetRandomString(emailLength)}{randomEmail.Generate()}";
            }

            if (emailLength < emailLengthMin)
            {
                var email = randomEmail.Generate();

                if (email.Length != emailLength)
                {
                    var emailV1 = new Regex("^[a-zA-Z0-9_.+-]+").Replace(email, string.Empty);
                    var emailV2 = new Regex("@[a-zA-Z0-9-]+").Replace(emailV1, "@g");
                    currentEmail = new Regex(@"\.[a-zA-Z0-9-.]+$").Replace(emailV2, ".c");
                }
            }

            return currentEmail;
        }

        private static string GetName(int nameLengthMin = 0, int nameLengthMax = 0)
        {
            var nameLength = 0;
            var name = string.Empty;

            if (nameLengthMin > 0 && nameLengthMax > 0)
            {
                nameLength = RandomNumber.Next(nameLengthMin, nameLengthMax);
                name = GetRandomString(nameLength);
            }
            else if (nameLengthMin < 1 && nameLengthMax > 0)
            {
                name = GetRandomString(nameLengthMax);
            }
            else
            {
                name = GetRandomString(nameLengthMin);
            }

            return name;
        }

        private static string GetPassword(int passwordLengthMin, int passwordLengthMax)
        {
            Random random = new Random();
            StringBuilder password = new StringBuilder();
            var passwordLength = random.Next(passwordLengthMin, passwordLengthMax);

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = random.Next(0, allCharacters.Length);
                password.Append(allCharacters[randomIndex]);
            }

            return password.ToString();
        }

        private static string GetPassword(int passwordLength)
        {
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = new Random().Next(0, allCharacters.Length);
                password.Append(allCharacters[randomIndex]);
            }

            return password.ToString();
        }

        #endregion
    }
}