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
    public class UserController
    {
        private Dictionary<string, User> users;
        private string dataBasePath = "Base.mbb";

        public UserController()
        {
            if (!CheckDataBaseAccessability()) throw new ArgumentNullException("DataBase");
            string data = File.ReadAllText(dataBasePath);
            try { users = Serialiser.Deserialize<Dictionary<string, User>>(data); }
            catch { }
        }

        public bool CanLogin(string login, string pasword)
        {
            if (!IsExists(login)) return false;
            return CheckPaswordCorrectness(pasword, users[login].Pasword);
        }

        public bool AddNewUser(string login, string pasword)
        {
            if (IsExists(login) || (login == "Login" && pasword == "password")) return false;
            AddUserData(login, pasword);
            return true;
        }

        private void AddUserData(string login, string pasword)
        {
            if (users == null) users = new Dictionary<string, User>();
            users.Add(login, new User(login, GetHash(pasword)));
            string content = Serialiser.Serialize(users);
            File.WriteAllText(dataBasePath, content);
        }

        private bool IsExists(string login)
        {
            try { return users.ContainsKey(login); }
            catch { return false; }
        }

        private byte[] GetHash(string pasword)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            return hash;
        }

        private bool CheckPaswordCorrectness(string pasword, byte[] paswordInBase)
        {
            return Enumerable.SequenceEqual(GetHash(pasword), paswordInBase);
        }

        private bool CheckDataBaseAccessability()
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
