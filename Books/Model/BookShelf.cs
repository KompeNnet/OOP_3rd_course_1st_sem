using System.Collections.Generic;

namespace Books.Model
{
    public class BookShelf
    {
        public string Name { get; set; }
        public int Count { get; private set; }
        private List<Book> Content { get; set; }

        public BookShelf() { }

        public BookShelf(string name, List<Book> content = null)
        {
            Name = name;
            Content = content;
            if (Content != null) Count = Content.Count;
        }

        public bool AddBook(Book book)
        {
            if (Content.Contains(book)) return false;
            Content.Add(book);
            Count++;
            return true;
        }

        public bool RemoveBook(Book book)
        {
            if (!Content.Contains(book)) return false;
            Content.Remove(book);
            Count++;
            return true;
        }

        public void ClearBookShelfContent()
        {
            Content = new List<Book>();
            Count = 0;
        }

        public List<Book> GetBookList()
        {
            return Content;
        }
    }
}
