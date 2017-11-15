using System.Collections.Generic;

namespace Books.Model
{
    public class Author : User
    {
        public Author() { }
        public Author(string login, byte[] pasword, Dictionary<string, BookShelf> shelfCollection, string info = "", int role = RoleCollection.AUTHOR)
            : base(login, pasword, shelfCollection, info, role) { }
    }
}
