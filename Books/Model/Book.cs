namespace Books.Model
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public ReviewCollection BookReviewCollection { get; private set; }
        public int? PublishingYear { get; set; }

        public Book() { }

        public Book(string name, string author, ReviewCollection reviewCollection = null, int? publishingYear = null)
        {
            Name = name;
            Author = author;
            PublishingYear = publishingYear;
            BookReviewCollection = reviewCollection;
        }
    }
}
