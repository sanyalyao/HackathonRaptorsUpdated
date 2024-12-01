using Faker;
using Fare;
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

        private static int uuidLength = 36;

        public static string GeneratePassword()
        {
            var lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";
            var specialCharacters = "!@#$%^&*()_+-=";
            var allCharacters = lowerCaseLetters + upperCaseLetters + numbers + specialCharacters;
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
            var i = 0;
            var randomEmail = new Xeger(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            var currentEmail = randomEmail.Generate();

            while(i < 10)
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
            var uuid = new Xeger("[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-4[a-fA-F0-9]{3}-[89abAB][a-fA-F0-9]{3}-[a-fA-F0-9]{12}");

            return uuid.Generate();
        }
    }
}