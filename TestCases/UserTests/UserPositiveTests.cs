using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using QAHackathon.BussnessObjects.Utils;
using QAHackathon.Core.BussnessLogic;
using static QAHackathon.Core.BussnessLogic.StepsBL;

namespace QAHackathon.TestCases.UserTests
{
    public class UserPositiveTests : TestBase
    {
        [Test]
        [Description("Get list of users. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUsers()
        {
            Step("Getting all users", () =>
            {
                userService.GetUsers();
            });
        }

        [Test]
        [Description("Get random user. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
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
        [Category("Positive")]
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
                loggingBL.Info("User does not exist");
            });
        }

        [Test]
        [Description("Create a mew user. Checking for response is not empty/null, success and the new user is created")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUser()
        {
            var generatedUser = Step("Generating a new user with random parameters", () =>
            {
                return UserGenerator.GetNewUser();
            });

            var createdUser = Step("Creating the new user", () =>
            {
                var createdUser = userService.CreateNewUser(generatedUser);

                createdUser.Show();

                return createdUser;
            });

            Step("Comparing generated user data with user data from API", () =>
            {
                AssertBL.AreEqual(generatedUser.AvatarUrl, createdUser.AvatarUrl);
                AssertBL.AreEqual(generatedUser.Email, createdUser.Email);
                AssertBL.AreEqual(generatedUser.Name, createdUser.Name);
                AssertBL.AreEqual(generatedUser.Nickname, createdUser.Nickname);

                loggingBL.Info("The new user is correct");
            });
        }

        [Test]
        [Description("Update user. Checking for response is not empty/null, success and user is updated")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.critical)]
        public void UpdateUser()
        {
            var currentUser = Step("Getting random user", () =>
            {
                var users = userService.GetUsers();
                var usersCount = users.Users.Count;
                var randomUser = users.Users.ToList()[new Random().Next(0, usersCount)];

                loggingBL.Info("Got random user");
                randomUser.Show();

                return randomUser;
            });

            var updatedUser = Step("Updating user with", () =>
            {
                var userWithChanges = new Dictionary<string, string>()
                {
                    { "name", UtilsBL.GetCorrectName() },
                    { "nickname", UtilsBL.GetCorrectNickname() }
                };

                loggingBL.Info($"New Name: {userWithChanges.First().Value} and Nickname: {userWithChanges.Last().Value}");

                var updatedUser = userService.UpdateUser(currentUser, userWithChanges);

                loggingBL.Info("New user data");
                updatedUser.Show();

                return updatedUser;
            });

            Step("Comparing previous user data with new user data", () =>
            {
                AssertBL.AreEqual(currentUser.AvatarUrl, updatedUser.AvatarUrl);
                AssertBL.AreEqual(currentUser.Email, updatedUser.Email);
                AssertBL.AreEqual(currentUser.Uuid, updatedUser.Uuid);
                AssertBL.AreNotEqual(currentUser.Name, updatedUser.Name);
                AssertBL.AreNotEqual(currentUser.Nickname, updatedUser.Nickname);
                loggingBL.Info("User was updated successfuly");
            });
        }

        [Test]
        [Description("Get a user by email and password. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUserByPasswordAndEmail()
        {
            var newUser = Step("Creating a new user", () =>
            {
                var newUser = UserGenerator.GetNewUser();

                userService.CreateNewUser(newUser);

                return newUser;
            });

            var userFromApi = Step("Getting the user by password and email", () =>
            {
                return userService.GetUserByPasswordAndEmail(newUser);
            });

            Step("Checking for the created user equals to the user from API", () =>
            {
                AssertBL.AreEqual(newUser.AvatarUrl, userFromApi.AvatarUrl);
                AssertBL.AreEqual(newUser.Email, userFromApi.Email);
                AssertBL.AreEqual(newUser.Name, userFromApi.Name);
                AssertBL.AreEqual(newUser.Nickname, userFromApi.Nickname);
                loggingBL.Info("Successfuly got the user by email and password");
            });
        }
    }
}