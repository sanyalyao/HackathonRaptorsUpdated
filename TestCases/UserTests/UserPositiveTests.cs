using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using QAHackathon.BussnessObjects.Utils;
using QAHackathon.Core.BussnessLogic;
using static QAHackathon.Core.BussnessLogic.StepsBL;

namespace QAHackathon.TestCases.UserTests
{
    [TestFixture]
    public class UserPositiveTests : TestBase
    {
        [Test]
        [Description("Get list of users. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [Order(1)]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.GetAllUsersData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUsers(string taskId)
        {
            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(taskId);
            });

            Step("Checking for first response is correct", () => 
            {
                var usersCount = users.Meta.Total;
                var usersList = users.Users;

                AssertBL.IsTrue(usersList.ToList().Any());
                AssertBL.IsTrue(usersCount >= 1);
            });
        }

        [Test]
        [Description("Get list of users with limit. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [Order(2)]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.GetAllUsersData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUsersWithLimit(string taskId)
        {
            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(taskId);
            });

            var listUserWith = Step("Getting all users with limit", () =>
            {
                return userService.GetUsers(users.Meta.Total, taskId);
            });

            Step("Checking for response with limit is correct", () =>
            {
                var usersCount = listUserWith.Meta.Total;
                var usersList = listUserWith.Users;

                AssertBL.IsTrue(usersList.ToList().Any());
                AssertBL.IsTrue(usersCount >= 1);
                AssertBL.AreEqual(usersCount, usersList.Count);
            });
        }

        [Test]
        [Description("Get random user. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [Order(3)]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.GetAllUsersData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetRandomUserByUuid(string taskId)
        {
            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(taskId);
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
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.CreateUserData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void DeleteUser(string taskId)
        {
            var userForDeleting = Step("Generating a new user with random parameters", () =>
            {
                var generatedUser = UserGenerator.GetNewUser();
                var createdUser = userService.CreateNewUser(generatedUser, taskId);

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
        [Description("Delete random user")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [Order(5)]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.GetAllUsersData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void DeleteRandomUser(string taskId)
        {
            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(taskId);
            });

            var usersCount = users.Meta.Total;

            var deletedUser = Step("Deleting random user", () =>
            {
                var randomUser = users.Users.ToList()[new Random().Next(users.Users.Count)];

                userService.DeleteUser(randomUser.Uuid);

                return randomUser;
            });

            Step("Checking for the amount of users has decreased by one", () => 
            { 
                var currentUsersCount = userService.GetUsers(taskId).Meta.Total;

                AssertBL.AreEqual(usersCount - 1, currentUsersCount);

                loggingBL.Info("The amount of users has decreased by one");
            });

            Step("Checking for the non-existent user", () =>
            {
                userService.GetUserByUuidWithoutException(deletedUser.Uuid);
                loggingBL.Info("User does not exist");
            });
        }

        [Test]
        [Description("Create a mew user. Checking for response is not empty/null, success and the new user is created")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.CreateUserData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUser(string taskId)
        {
            var generatedUser = Step("Generating a new user with random parameters", () =>
            {
                var generatedUser = UserGenerator.GetNewUser();

                generatedUser.Show();

                return generatedUser;
            });

            var createdUser = Step("Creating the new user", () =>
            {
                var createdUser = userService.CreateNewUser(generatedUser, taskId);

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
        [Order(4)]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.GetAllUsersData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void UpdateUser(string taskIdGetAllUsers, string taskIdUpdateUser)
        {
            var currentUser = Step("Getting random user", () =>
            {
                var users = userService.GetUsers(taskIdGetAllUsers);
                var usersCount = users.Users.Count;
                var randomUser = users.Users.ToList()[new Random().Next(0, usersCount)];

                loggingBL.Info("Got random user");
                randomUser.Show();

                return randomUser;
            });

            var userWithChanges = new Dictionary<string, string>()
            {
                { "name", UtilsBL.GetCorrectName() },
                { "nickname", UtilsBL.GetCorrectNickname() },
                { "email", UtilsBL.GetCorrectEmail() }
            };

            var updatedUser = Step("Updating user with", () =>
            {
                loggingBL.Info($"New Name: {userWithChanges.First().Value} and Nickname: {userWithChanges.Last().Value}");

                var updatedUser = userService.UpdateUser(currentUser, userWithChanges, taskIdUpdateUser);

                loggingBL.Info("New user data");
                updatedUser.Show();

                return updatedUser;
            });

            Step("Comparing previous user data with new user data", () =>
            {
                AssertBL.AreEqual(currentUser.AvatarUrl, updatedUser.AvatarUrl);
                AssertBL.AreEqual(currentUser.Uuid, updatedUser.Uuid);
                AssertBL.AreEqual(userWithChanges.First().Value, updatedUser.Name);
                AssertBL.AreEqual(userWithChanges["nickname"], updatedUser.Nickname);
                AssertBL.AreEqual(userWithChanges.Last().Value, updatedUser.Email);
                AssertBL.AreNotEqual(currentUser.Name, updatedUser.Name);
                AssertBL.AreNotEqual(currentUser.Nickname, updatedUser.Nickname);
                AssertBL.AreNotEqual(currentUser.Email, updatedUser.Email);
                loggingBL.Info("User was updated successfuly");
            });
        }

        [Test]
        [Description("Get a user by email and password. Checking for response is not empty/null and success")]
        [Category("API")]
        [Category("Users")]
        [Category("Positive")]
        [TestCaseSource(typeof(Api.Users), nameof(Api.Users.CreateUserData))]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUserByPasswordAndEmail(string taskId)
        {
            var newUser = Step("Creating a new user", () =>
            {
                var newUser = UserGenerator.GetNewUser();

                userService.CreateNewUser(newUser, taskId);

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