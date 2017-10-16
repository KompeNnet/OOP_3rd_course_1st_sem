using System;
using System.Collections.Generic;

namespace Books.Model
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public List<Review> BookReviewCollection { get; set; } = new List<Review>();
        public string Info { get; set; }

        public Book() { }

        public Book(string name, string author, List<Review> reviewCollection = null, string info = "")
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Author = author;
            Info = info;
            BookReviewCollection = reviewCollection;
        }
    }
}
