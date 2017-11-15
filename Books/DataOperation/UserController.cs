using Books.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Books.DataOperation
{
    public static class UserController
    {
        private static Dictionary<string, User> users;
        private const string dataBasePath = "Base.mbb";

        static UserController()
        {
            try
            {
                if (!CheckDataBaseAccessability()) throw new ArgumentNullException("DataBase");
                string data = File.ReadAllText(dataBasePath);
                try { users = Serialiser.Deserialize<Dictionary<string, User>>(data); }
                catch { }
            }
            catch (ArgumentNullException e)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + e.ParamName + ".", "Oups!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void SaveUserData()
        {
            string content = Serialiser.Serialize(users);
            File.WriteAllText(dataBasePath, content);
        }

        public static User GetUser(string login)
        {
            if (!IsExists(login)) return null;
            return users[login];
        }

        public static bool CanLogin(string login, string pasword)
        {
            if (!IsExists(login)) return false;
            return CheckPaswordCorrectness(pasword, users[login].Pasword);
        }

        public static bool AddNewUser(string login, string pasword)
        {
            if (IsExists(login) || (login == "Login" && pasword == "password")) return false;
            AddUserData(login, pasword);
            return true;
        }

        private static void AddUserData(string login, string pasword)
        {
            if (users == null) users = new Dictionary<string, User>();
            users.Add(login, new User(login, GetHash(pasword), new Dictionary<string, BookShelf>() { { "My first bookshelf", new BookShelf("My first bookshelf") } }));
        }

        private static bool IsExists(string login)
        {
            try { return users.ContainsKey(login); }
            catch { return false; }
        }

        private static byte[] GetHash(string pasword)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            return hash;
        }

        private static bool CheckPaswordCorrectness(string pasword, byte[] paswordInBase)
        {
            return Enumerable.SequenceEqual(GetHash(pasword), paswordInBase);
        }

        private static bool CheckDataBaseAccessability()
        {
            if (!File.Exists(dataBasePath))
            {
                MessageBoxResult answer = MessageBox.Show("Smth goes wrong with our database. Sorry for that troble :(", "Ouch...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
