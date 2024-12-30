using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using QAHackathon.BussinesObjects.Models;
using QAHackathon.BussnessObjects.Models;
using QAHackathon.BussnessObjects.Utils;
using QAHackathon.Core.BussnessLogic;
using QAHackathon.Core.RunSettings;
using RestSharp;
using System.Text.RegularExpressions;
using static QAHackathon.Core.BussnessLogic.StepsBL;

namespace QAHackathon.TestCases.UserTests
{
    [TestFixture]
    public class UserNegativeTests : TestBase
    {
        [Test]
        [Description("Update non-existent user. Checking for error in response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void UpdateNonExistentUser([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.UpdateTaskId))] string taskId)
        {
            var user = Step("Generating a user with fake UUID", () =>
            {
                var user = new UserModel()
                {
                    Uuid = UtilsBL.GetRandomUuid()
                };

                loggingBL.Info($"Fake UUID: {user.Uuid}");

                return user;
            });

            var response = Step("Updating non-existent user", () =>
            {
                var userWithChanges = new Dictionary<string, string>()
                {
                    { "name", UtilsBL.GetCorrectName() },
                    { "nickname", UtilsBL.GetCorrectNickname() }
                };

                return userService.UpdateUserWithoutException(user, userWithChanges, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($"Could not find user with \"uuid\": {user.Uuid}").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.NotFound.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Get a user NOT by email and password. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void GetUserNotByPasswordAndEmail([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string getAllTaskId,
            [ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string createTaskId)
        {
            var currentUser = Step("Creating a new user", () =>
            {
                var newUser = UserGenerator.GetNewUser();

                userService.CreateNewUser(newUser, createTaskId);

                return newUser;
            });

            var response = Step("Getting the user by nickname and password", () =>
            {
                return userService.GetUserByNicknameAndPassword(currentUser, getAllTaskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.MissedEmail}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Create a new user with incorrect email format. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUserWithIncorrectEmailFormat([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string taskId)
        {
            var userIncorrectEmail = Step("Generating fake user with incorrect email format", () =>
            {
                return UserGenerator.GetNewUserWithIncorrectEmailFormat();
            });

            var response = Step("Registering a new user with incorrect email", () =>
            {
                return userService.CreateNewUserWithoutException(userIncorrectEmail, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.NotMatchExpression}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Create a new user with incorrect max email length. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUserWithIncorrectMaxEmailLength([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string taskId)
        {
            var userIncorrectEmailMaxLength = Step("Generating fake users with incorrect max email length", () =>
            {
                return UserGenerator.GetUserIncorrectEmailLength(UserGenerator.Lengths.Max);
            });

            var response = Step("Registering a new user with incorrect max email length", () =>
            {
                return userService.CreateNewUserWithoutException(userIncorrectEmailMaxLength, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.MaximumStringLength}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Create a new user with empty email. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUserWithEmptyEmailLength([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string taskId)
        {
            var userIncorrectEmailMinLength = Step("Generating fake users with empty email", () =>
            {
                var newUser = UserGenerator.GetNewUser();
                newUser.Email = string.Empty;

                loggingBL.Info("Generated fake user with empty email");

                return newUser;
            });

            var response = Step("Registering a new user with incorrect min email length", () =>
            {
                return userService.CreateNewUserWithoutException(userIncorrectEmailMinLength, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.MinimumStringLength}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Create a new user with null email. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUserWithNullEmail([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string taskId)
        {
            var userIncorrectEmailMinLength = Step("Generating fake users with null email", () =>
            {
                var newUser = UserGenerator.GetNewUser();
                newUser.Email = null;

                loggingBL.Info("Generated fake user with empty email");

                return newUser;
            });

            var response = Step("Registering a new user with incorrect min email length", () =>
            {
                return userService.CreateNewUserWithoutException(userIncorrectEmailMinLength, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.Nullable}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Create a new user with incorrect min email length. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateUserWithIncorrectMinEmailLength([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string taskId)
        {
            var userIncorrectEmailMinLength = Step("Generating fake users with incorrect min email length", () =>
            {
                var newUser = UserGenerator.GetUserIncorrectEmailLength(UserGenerator.Lengths.Min);

                loggingBL.Info("Generated fake user with incorrect min email length");

                return newUser;
            });

            var response = Step("Registering a new user with incorrect min email length", () =>
            {
                return userService.CreateNewUserWithoutException(userIncorrectEmailMinLength, taskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.MinimumStringLength}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Update a user with incorrect null data. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void UpdateUserWithIncorrectData([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string taskId)
        {
            var responses = Step("Updating a user with incorrect null data", () =>
            {
                var currentUser = userService.GetUsers(taskId).Users.ToList().First();

                var userWithNullName = new Dictionary<string, string>()
                {
                    { "name", null }
                };

                var userWithNullNickname = new Dictionary<string, string>()
                {
                    { "nickname", null }
                };

                var userWithNullEmail = new Dictionary<string, string>()
                {
                    { "email", null }
                };

                var userWithNullPassword = new Dictionary<string, string>()
                {
                    { "password", null }
                };

                var responses = new List<RestResponse>() {
                    userService.UpdateUserWithoutException(currentUser, userWithNullName, taskId),
                    userService.UpdateUserWithoutException(currentUser, userWithNullNickname, taskId),
                    userService.UpdateUserWithoutException(currentUser, userWithNullEmail, taskId),
                    userService.UpdateUserWithoutException(currentUser, userWithNullPassword, taskId) };

                return responses;
            });

            Step("Checking for error in responses", () =>
            {
                foreach (var response in responses)
                {
                    var error = new ErrorModel().GetError(response);
                    var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.Nullable}.*").Match(error.Message);

                    AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                    AssertBL.IsFalse(response.IsSuccessful);
                    AssertBL.IsTrue(isMatch.Success);

                    loggingBL.Info($"There is error in each response: {error.Code}");
                    loggingBL.Info(error.Message);
                }
            });
        }

        [Test]
        [Description("Check users limit. Checking for error response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CheckUsersLimit([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string getAllTaskId,
            [ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.CreateTaskId))] string createTaskId)
        {
            var usersLimit = TestDataBL.InputTestData.UsersLimit;

            var usersAmount = Step("Gettings amount of users", () =>
            {
                var usersAmount = userService.GetUsers(getAllTaskId).Meta.Total.Value;

                loggingBL.Info($"Users limit is {usersAmount}");

                return usersAmount;
            });

            if (usersLimit != usersAmount)
            {
                var usersData = Step("Generating users data collection", () =>
                {
                    var usersData = new List<User>();

                    for (int i = usersAmount; i < usersLimit; i++)
                    {
                        usersData.Add(UserGenerator.GetNewUser());
                    }

                    loggingBL.Info("Users data is ready");

                    return usersData;
                });

                Step("Registering new users and checking new users amount", () =>
                {
                    foreach (var user in usersData)
                    {
                        userService.CreateNewUser(user, createTaskId);
                    }

                    var currentUsersAmount = userService.GetUsers(getAllTaskId).Meta.Total;

                    AssertBL.AreEqual(usersLimit, currentUsersAmount);

                    loggingBL.Info("Users are ready");
                });
            }

            var response = Step("Getting error from registration one more user", () => 
            {
                var currentUsersAmount = userService.GetUsers(getAllTaskId).Meta.Total;

                loggingBL.Info($"Current users amount = {currentUsersAmount}");
                loggingBL.Info("Registering a new user");

                var newUser = UserGenerator.GetNewUser();
                var response = userService.CreateNewUserWithoutException(newUser, createTaskId);

                return response;
            });

            Step("Checking for error in responses", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@".*{TestDataBL.InputTestData.ResponseErrors.Messages.UsersLimit}.*").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.UnprocessableEntity.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in each response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Check if list of users is empty, when all users was deleted. Checking for error in response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void CheckEmptyListOfUsers([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.DeleteTaskId))] string deleteTaskId, 
            [ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string getAllTaskId)
        {
            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(getAllTaskId);
            });

            var usersCount = users.Meta.Total;

            if (usersCount != 0)
            {
                loggingBL.Info("There are users");
                loggingBL.Info("Initializing the process of deleting all users");

                for (int i = 0; i < usersCount; i++)
                {
                    Step("Deleting random user", () =>
                    {
                        var randomUser = users.Users.ToList()[new Random().Next(users.Users.Count)];

                        userService.DeleteUser(randomUser.Uuid, deleteTaskId);
                    });
                }
            }

            loggingBL.Info("There are no users");
            loggingBL.Info("Initializing the process of deleting a user");

            var randomUuid = UtilsBL.GetRandomUuid();

            var response = Step("Deleting a user with random UUID", () =>
            {
                return userService.DeleteUserWithoutException(randomUuid, deleteTaskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex($@"{TestDataBL.InputTestData.ResponseErrors.Messages.NoUserWithUuid}: {randomUuid}").Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.NotFound.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Initialize deleting a user, using short uuid. Checking for error in response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void DeleteUserUuidWithShortLength([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.DeleteTaskId))] string deleteTaskId,
    [ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string getAllTaskId)
        {
            var correctUuidLength = TestDataBL.InputTestData.GeneralData.UuidLength;
            var shortUuidLength = TestDataBL.InputTestData.GeneralData.UuidLength - 1;
            var shortUuid = UtilsBL.GetRandomUuid().Remove(shortUuidLength);

            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(getAllTaskId);
            });

            var usersCount = users.Meta.Total;

            if (usersCount != 0)
            {
                loggingBL.Info("There are users");
                loggingBL.Info("Initializing the process of deleting all users");

                for (int i = 0; i < usersCount; i++)
                {
                    Step("Deleting random user", () =>
                    {
                        var randomUser = users.Users.ToList()[new Random().Next(users.Users.Count)];

                        userService.DeleteUser(randomUser.Uuid, deleteTaskId);
                    });
                }
            }

            loggingBL.Info("There are no users");
            loggingBL.Info("Initializing the process of deleting a user");

            var response = Step($"Deleting a user, using uuid legnth {shortUuidLength} instead of {correctUuidLength}", () =>
            {
                return userService.DeleteUserWithoutException(shortUuid, deleteTaskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex(TestDataBL.InputTestData.ResponseErrors.Messages.UuidMinLength).Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }

        [Test]
        [Description("Initialize deleting a user, using long uuid. Checking for error in response")]
        [Category("API")]
        [Category("Users")]
        [Category("Negative")]
        [AllureSeverity(SeverityLevel.critical)]
        public void DeleteUserUuidWithLongLength([ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.DeleteTaskId))] string deleteTaskId,
            [ValueSource(typeof(TestDataBL.Users), nameof(TestDataBL.Users.GetAllTaskId))] string getAllTaskId)
        {
            var correctUuidLength = TestDataBL.InputTestData.GeneralData.UuidLength;
            var longUuidLength = TestDataBL.InputTestData.GeneralData.UuidLength + 1;
            var longUuid = UtilsBL.GetRandomUuid() + "T";

            var users = Step("Getting all users", () =>
            {
                return userService.GetUsers(getAllTaskId);
            });

            var usersCount = users.Meta.Total;

            if (usersCount != 0)
            {
                loggingBL.Info("There are users");
                loggingBL.Info("Initializing the process of deleting all users");

                for (int i = 0; i < usersCount; i++)
                {
                    Step("Deleting random user", () =>
                    {
                        var randomUser = users.Users.ToList()[new Random().Next(users.Users.Count)];

                        userService.DeleteUser(randomUser.Uuid, deleteTaskId);
                    });
                }
            }

            loggingBL.Info("There are no users");
            loggingBL.Info("Initializing the process of deleting a user");

            var response = Step($"Deleting a user, using uuid length {longUuidLength} instead of {correctUuidLength}", () =>
            {
                return userService.DeleteUserWithoutException(longUuid, deleteTaskId);
            });

            Step("Checking for error in response", () =>
            {
                var error = new ErrorModel().GetError(response);
                var isMatch = new Regex(TestDataBL.InputTestData.ResponseErrors.Messages.UuidMaxLength).Match(error.Message);

                AssertBL.AreEqual(TestDataBL.InputTestData.ResponseErrors.BadRequest.Code, (int)response.StatusCode);
                AssertBL.IsFalse(response.IsSuccessful);
                AssertBL.IsTrue(isMatch.Success);

                loggingBL.Info($"There is error in response: {error.Code}");
                loggingBL.Info(error.Message);
            });
        }
    }
}