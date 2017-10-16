using Books.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace Books.View
{
    /// <summary>
    /// Interaction logic for BookReviews.xaml
    /// </summary>
    public partial class BookReviews : Window
    {
        private Book CurrentBook { get; set; }
        private string CurrentUser { get; set; }

        public BookReviews(Book book, string user)
        {
            InitializeComponent();
            CurrentBook = book;
            Title = CurrentBook.Author + " - " + CurrentBook.Name;
            CurrentUser = user;
            LoadReviews();
        }

        private void LoadReviews()
        {
            TextBoxReviews.Text = "";
            foreach (Review r in CurrentBook.BookReviewCollection)
                TextBoxReviews.Text += r.Author + " - " + r.DatePublished.ToString("mm/dd/yy") + "\n " + r.Content + "\n";
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            CurrentBook.BookReviewCollection.Add(new Review(TextBoxInput.Text, DateTime.Now, CurrentUser));
            LoadReviews();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadReviews();
        }
    }
}
