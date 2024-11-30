using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using QAHackathon.BussnessObjects.Utils;
using static QAHackathon.Core.BussnessLogic.StepsBL;

namespace QAHackathon.TestCases
{
    public class UserTests : TestBase
    {
        [Test]
        [Description("Get list of users. Checking for response is not empty and success")]
        [Category("API")]
        [Category("Users")]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUsers()
        {
            Step("Getting all users", () => 
            {
                userService.GetUsers();
            });
        }

        [Test]
        [Description("Get random user. Checking for response is not empty and success")]
        [Category("API")]
        [Category("Users")]
        [Category("User")]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetRandomUserByUuid()
        {
            var users = Step("Getting all users", () => 
            {
                return userService.GetUsers();
            });

            Step("Getting random user", () => 
            {
                var usersCount = users.Users.Count;
                var randomUser = users.Users.ToList()[new Random().Next(0, usersCount)];
                var user = userService.GetUserByUuid(randomUser.Uuid);

                loggingBL.Info("Got random user");
                user.Show();
            });
        }

        [Test]
        [Description("Delete a user")]
        [Category("API")]
        [Category("Users")]
        [Category("User")]
        [AllureSeverity(SeverityLevel.critical)]
        public void DeleteUser()
        {
            var userForDeleting = Step("Generating a new user with random parameters", () =>
            {
                var generatedUser = UserGenerator.GetNewUser();
                var createdUser = userService.CreateNewUser(generatedUser);

                createdUser.Show();

                return createdUser;
            });

            Step("Deleting the new user", () => 
            {
                userService.DeleteUser(userForDeleting.Uuid);
            });

            Step("Checking for the non-existent user", () => 
            {
                userService.GetUserByUuidWithoutException(userForDeleting.Uuid);
            });
        }

        [Test]
        [Description("")]
        [Category("API")]
        [Category("Users")]
        [Category("User")]
        [AllureSeverity(SeverityLevel.critical)]
        public void Test()
        {
            // 1 DONE
            /*
             * delete user
             * 
             * создаём user
             * проверяем что он создан
             * 
             * удаляем ранее созданного user
             * проверяем что его нет в БД
             */

            // 2
            /*
             * create user
             * 
             * создаём user
             * проверяем что он создан
             */

            // 3
            /*
             * update user
             * 
             * get user
             * generate new data
             * do patch user
             * check user
             */

            // 4
            /*
             * проверка обновления несуществующего user
             * 
             * без некорректных символов
             * 
             * проверка наличия ошибки
             */

            // 5
            /*
             * Get a user by email and password
             * 
             * получаем нужного user by email and password
             * 
             */

            // 6
            /*
             * Get a user by email and password
             * 
             * получаем user not by email and password
             * 
             * проверка наличия ошибки
             */

            // 7
            /*
             * create user
             * 
             * создать user с неверной почтой
             * 
             * проверка наличия ошибки
             */

            // 8
            /*
             * проверка обновления существующего user
             * 
             * с некорректными символами
             * 
             * проверка наличия ошибки
             */

            // 9
            /*
             * create user
             * 
             * с некорректными символами
             * 
             * проверка наличия ошибки
             */
        }
    }
}
