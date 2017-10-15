using System.Collections.Generic;
using System.Linq;

namespace Books.Model
{
    public class BookShelfCollection
    {
        private List<BookShelf> BookShelfList;

        public BookShelfCollection()
        {
            BookShelfList = new List<BookShelf>() { new BookShelf("My first bookshelf") };
        }

        public BookShelfCollection(IEnumerable<BookShelf> shelfCollection)
        {
            BookShelfList = shelfCollection.ToList();
        }

        public bool AddBookShelf(BookShelf bookShelf)
        {
            if (BookShelfList.Contains(bookShelf)) return false;
            BookShelfList.Add(bookShelf);
            return true;
        }

        public bool RemoveBookShelf(BookShelf bookShelf)
        {
            if (!BookShelfList.Contains(bookShelf)) return false;
            BookShelfList.Remove(bookShelf);
            return true;
        }

        public bool RemoveBookShelf(int index)
        {
            if (BookShelfList.Count >= index && index <= 0) return false;
            BookShelfList.RemoveAt(index);
            return true;
        }

        public List<BookShelf> GetBookShelfList()
        {
            return BookShelfList;
        }
    }
}
