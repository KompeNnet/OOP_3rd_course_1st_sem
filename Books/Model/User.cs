using System.Collections.Generic;

namespace Books.Model
{
    public class User
    {
        public string Login { get; set; }
        public byte[] Pasword { get; set; }
        public Dictionary<string, BookShelf> ShelfCollection { get; set; }
        public int Role { get; set; }
        public string Info { get; set; }

        public User() { }

        public User(string login, byte[] pasword, Dictionary<string, BookShelf> shelfCollection, string info = "", int role = RoleCollection.USER)
        {
            Login = login;
            Pasword = pasword;
            ShelfCollection = shelfCollection;
            Info = info;
            Role = role;
        }
    }
}
