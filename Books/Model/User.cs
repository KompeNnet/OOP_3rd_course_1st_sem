namespace Books.Model
{
    public class User
    {
        public string Login { get; set; }
        public byte[] Pasword { get; set; }
        public string Role { get; set; } = "user";

        public User(string login, byte[] pasword)
        {
            Login = login;
            Pasword = pasword;
        }
    }
}
