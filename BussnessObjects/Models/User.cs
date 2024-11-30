namespace QAHackathon.BussinesObjects.Models
{
    public partial class User
    {
        public int AvatarUrl { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

        public User(string email, string name, string nickname, string password) 
        {
            Email = email;
            Name = name;
            Nickname = nickname;
            Password = password;
        }

        public User(int avatarUrl, string email, string name, string nickname, string password)
        {
            AvatarUrl = avatarUrl;
            Email = email;
            Name = name;
            Nickname = nickname;
            Password = password;
        }
    }
}