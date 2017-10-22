using System.Collections.Generic;

namespace Books.Model
{
    public class BookShelf
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<string> Content { get; set; } = new List<string>();
        private bool Opened;

        public BookShelf() { }

        public BookShelf(string name, List<string> content = null)
        {
            Name = name;
            Content = content;
            if (Content != null) Count = Content.Count;
            Opened = false;
        }

        public bool IsOpened()
        {
            return Opened;
        }

        public void BookShelfOpened()
        {
            Opened = true;
        }

        public void BookShelfClosed()
        {
            Opened = false;
        }

        public bool AddBook(string id)
        {
            if (Content.Contains(id)) return false;
            Content.Add(id);
            Count++;
            return true;
        }

        public bool RemoveBook(string id)
        {
            if (!Content.Contains(id)) return false;
            Content.Remove(id);
            Count--;
            return true;
        }

        public void ClearBookShelfContent()
        {
            Content = new List<string>();
            Count = 0;
        }
    }
}
