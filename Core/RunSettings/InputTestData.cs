using Newtonsoft.Json;

namespace QAHackathon.Core.RunSettings
{
    public class InputTestData
    {
        [JsonProperty("api")]
        public required Api Api { get; set; }

        [JsonProperty("users_limit")]
        public required int UsersLimit { get; set; }

        [JsonProperty("response_errors")]
        public required ResponseErrors ResponseErrors { get; set; }

        [JsonProperty("response_success")]
        public required ResponseSuccess ResponseSuccess { get; set; }

        [JsonProperty("general_data")]
        public required GeneralData GeneralData { get; set; }
    }

    public class Api
    {
        [JsonProperty("users")]
        public required Users Users { get; set; }
    }

    public class Users
    {
        [JsonProperty("get_all_users")]
        public required string[] GetAllUsers { get; set; }

        [JsonProperty("create_user")]
        public required string[] CreateUser { get; set; }

        [JsonProperty("update_user")]
        public required string[] UpdateUser { get; set; }

        [JsonProperty("get_user_by_uuid")]
        public required string GetUserByUuid { get; set; }

        [JsonProperty("delete_user")]
        public required string DeleteUser { get; set; }

        [JsonProperty("get_user_by_pass_and_email")]
        public required string GetUserByPassAndEmail { get; set; }
    }

    public class ResponseErrors
    {
        [JsonProperty("unprocessable_entity")]
        public required Description UnprocessableEntity { get; set; }

        [JsonProperty("bad_request")]
        public required Description BadRequest { get; set; }

        [JsonProperty("not_found")]
        public required Description NotFound { get; set; }

        [JsonProperty("messages")]
        public required Messages Messages { get; set; }
    }

    public class ResponseSuccess 
    {
        [JsonProperty("ok")]
        public required Description Ok { get; set; }

        [JsonProperty("no_content")]
        public required Description NoContent { get; set; }
    }

    public class GeneralData
    {
        [JsonProperty("password_length_min")]
        public required int PasswordLengthMin { get; set; }

        [JsonProperty("password_length_max")]
        public required int PasswordLengthMax { get; set; }

        [JsonProperty("nickname_length_min")]
        public required int NicknameLengthMin { get; set; }

        [JsonProperty("nickname_length_max")]
        public required int NicknameLengthMax { get; set; }

        [JsonProperty("name_length_min")]
        public required int NameLengthMin { get; set; }

        [JsonProperty("name_length_max")]
        public required int NameLengthMax { get; set; }

        [JsonProperty("email_length_min")]
        public required int EmailLengthMin { get; set; }

        [JsonProperty("email_length_max")]
        public required int EmailLengthMax { get; set; }

        [JsonProperty("uuid_length")]
        public required int UuidLength { get; set; }

        [JsonProperty("all_characters")]
        public required string AllCharacters { get; set; }

        [JsonProperty("correct_email_pattern")]
        public required string CorrectEmailPattern { get; set; }

        [JsonProperty("incorrect_email_pattern")]
        public required string IncorrectEmailPattern { get; set; }

        [JsonProperty("correct_uuid_pattern")]
        public required string CorrectUuidPattern { get; set; }
    }

    public class Description
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("code")]
        public required int Code { get; set; }
    }

    public class Messages
    {
        [JsonProperty("nullable")]
        public required string Nullable { get; set; }

        [JsonProperty("minimum_string_length")]
        public required string MinimumStringLength { get; set; }

        [JsonProperty("maximum_string_length")]
        public required string MaximumStringLength { get; set; }

        [JsonProperty("users_limit")]
        public required string UsersLimit { get; set; }

        [JsonProperty("not_match_expression")]
        public required string NotMatchExpression { get; set; }

        [JsonProperty("missed_email")]
        public required string MissedEmail { get; set; }

        [JsonProperty("no_user_with_uuid")]
        public required string NoUserWithUuid { get; set; }

        [JsonProperty("uuid_min_length")]
        public required string UuidMinLength { get; set; }

        [JsonProperty("uuid_max_length")]
        public required string UuidMaxLength { get; set; }
    }
}