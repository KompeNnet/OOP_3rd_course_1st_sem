using Books.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Books.DataOperation
{
    public static class BookController
    {
        private static Dictionary<string, Book> bookBase;
        private const string dataBasePath = "BookBase.mbb";

        static BookController()
        {
            try
            {
                if (!CheckDataBaseAccessability()) throw new ArgumentNullException("DataBase");
                string data = File.ReadAllText(dataBasePath);
                try { bookBase = Serialiser.Deserialize<Dictionary<string, Book>>(data); }
                catch { }
            }
            catch (ArgumentNullException e)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + e.ParamName + ".", "Oups!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void SaveBookData()
        {
            string content = Serialiser.Serialize(bookBase);
            File.WriteAllText(dataBasePath, content);
        }

        public static List<Book> GetBookBase()
        {
            return bookBase.Values.ToList();
        }

        public static Book GetBook(string id)
        {
            if (!IsExists(id)) return null;
            return bookBase[id];
        }

        //public static bool AddNewBook(Book book, int accessState)
        public static bool AddNewBook(Book book)
        {
            if (IsExists(book.Id)) return false;
            AddBookData(book);
            return true;
        }

        private static void AddBookData(Book book)
        {
            if (bookBase == null) bookBase = new Dictionary<string, Book>();
            bookBase.Add(book.Id, book);
        }

        private static bool IsExists(string id)
        {
            try { return bookBase.ContainsKey(id); }
            catch { return false; }
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
