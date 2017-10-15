using System;

namespace Books.Model
{
    public class Review
    {
        public string Content { get; set; }
        public DateTime DatePublished { get; private set; }
        public string Author { get; private set; }

        public Review() { }

        public Review(string content, DateTime datePublished, string author)
        {
            Content = content;
            DatePublished = datePublished;
            Author = author;
        }
    }
}
