using Newtonsoft.Json;

namespace QAHackathon.Core.RunSettings
{
    public static class TestDataBL
    {
        public static InputTestData InputTestData { get; private set; } = GetInputTestData();

        public static class Users
        {
            public static string[] GetAllTaskId() => InputTestData.Api.Users.GetAllUsers;
            public static string[] CreateTaskId() => InputTestData.Api.Users.CreateUser;
            public static string[] UpdateTaskId() => InputTestData.Api.Users.UpdateUser;
            public static string[] GetByUuidTaskId() => new string[] { InputTestData.Api.Users.GetUserByUuid };
            public static string[] DeleteTaskId() => new string[] { InputTestData.Api.Users.DeleteUser };
            public static string[] GetUserByPassAndEmailTaskId() => new string[] { InputTestData.Api.Users.GetUserByPassAndEmail };
        }

        private static InputTestData GetInputTestData()
        {
            var config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core/Configs", "inputTestData.json"));

            return JsonConvert.DeserializeObject<InputTestData>(config);
        }
    }
}