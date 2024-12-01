using Faker;
using System.Text;

namespace QAHackathon.BussnessObjects.Utils
{
    public static class UtilsBL
    {
        private static int passwordLengthMin = 6;
        private static int passwordLengthMax = 100;

        private static int nicknameLengthMin = 2;
        private static int nicknameLengthMax = 100;

        private static int nameLengthMin = 1;
        private static int nameLengthMax = 100;

        private static int emailLengthMin = 5;
        private static int emailLengthMax = 100;

        public static string GeneratePassword()
        {
            var lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";
            var specialCharacters = "!@#$%^&*()_+-=";
            string allCharacters = lowerCaseLetters + upperCaseLetters + numbers + specialCharacters;
            Random random = new Random();
            var passwordLength = random.Next(passwordLengthMin, passwordLengthMax);
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = random.Next(0, allCharacters.Length);
                password.Append(allCharacters[randomIndex]);
            }

            return password.ToString();
        }

        public static string GenerateEmail()
        {
            var emailLength = RandomNumber.Next(emailLengthMin, emailLengthMax);
            var email = Internet.Email(GetRandomString(emailLength));

            return email;
        }

        public static string GenerateName()
        {
            var nameLength = RandomNumber.Next(nameLengthMin, nameLengthMax);
            var name = GetRandomString(nameLength);

            return name;
        }

        public static string GenerateNickname()
        {
            var nickNameLength = RandomNumber.Next(nicknameLengthMin, nicknameLengthMax);
            var nickname = GetRandomString(nickNameLength);

            return nickname;
        }

        public static string GetRandomString(int minLength = 1, int maxLength = 101)
        {
            var lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string allCharacters = lowerCaseLetters + upperCaseLetters;
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
    }
}