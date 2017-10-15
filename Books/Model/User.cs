namespace Books.Model
{
    public class User
    {
        public string Login { get; set; }
        public byte[] Pasword { get; set; }
        public BookShelfCollection ShelfCollection { get; private set; }
        public string Role { get; private set; }
        public string Info { get; set; }

        public User() { }

        public User(string login, byte[] pasword, BookShelfCollection shelfCollection, string info = "", string role = "user")
        {
            Login = login;
            Pasword = pasword;
            ShelfCollection = shelfCollection;
            Info = info;
            Role = role;
        }
    }
}
